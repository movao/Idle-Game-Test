using System;
using System.Linq;
using IdleEngine.Sessions;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace IdleEngine.Generators
{
    [Serializable]
    public class Generator : MonoBehaviour, ISerializationCallbackReceiver
  {
    private double _multiplier;
    public int Owned;
    public double BaseCost;
    public double BaseRevenue;
    public float BaseProductionTimeInSeconds;
    public double CostFactor;

    public float pos;
        

    public Multiplier[] Multipliers;
    public float ProductionCycleInSeconds;

    [NonSerialized]
    public float ProductionTimeInSeconds;

    [NonSerialized]
    public double NextBuildingCostsForOne;

      
    void Start()
        {
            pos = transform.position.y;
        } 

    public void OnBeforeSerialize()
    {

     }


    public void OnAfterDeserialize()
    {
      Precalculate();
    }


    public bool CanBeBuild(Session session)
    {
      return session.Money >= NextBuildingCostsForOne;

    }


    public void Build(Session session)
    {
      if (!CanBeBuild(session))
      {
        return;
      }

      Owned++;
      session.Money -= NextBuildingCostsForOne;
      Precalculate();
    }


    public double Produce(float deltaTimeInSeconds)
    {
      if (Owned == 0)
      {
        return 0;
      }

      ProductionCycleInSeconds += deltaTimeInSeconds;

      double calculatedSum = 0;

      while (ProductionCycleInSeconds >= ProductionTimeInSeconds)
      {
        calculatedSum += BaseRevenue * Owned * _multiplier;
        ProductionCycleInSeconds -= ProductionTimeInSeconds;
      }

      return calculatedSum;
    }


    private void Precalculate()
    {
      UpdateModifiers();
      UpdateMultiplier();
      UpdateNextBuildingCosts();
    }


    private void UpdateNextBuildingCosts()
    {
      var kOverR = Math.Pow(CostFactor, Owned);
      var kPlusNOverR = Math.Pow(CostFactor, Owned + 1);

      NextBuildingCostsForOne = BaseCost *
                                (
                                  (kOverR - kPlusNOverR)
                                  /
                                  (1 - CostFactor)
                                );
    }


    private void UpdateModifiers()
    {
      ProductionTimeInSeconds = BaseProductionTimeInSeconds;

      if (Owned > 10)
      {
        ProductionTimeInSeconds /= 2;
      }
            if (Owned > 50)
            {
                ProductionTimeInSeconds /= 2;
            }
            if (Owned > 100)
            {
                ProductionTimeInSeconds /= 2;
            }
            if (Owned > 250)
            {
                ProductionTimeInSeconds /= 2;
            }
        }


    private void UpdateMultiplier()
    {
      if (Multipliers == null)
      {
        _multiplier = 1;
        return;
      }

      _multiplier = Multipliers.Aggregate(1d, (acc, multiplier) => acc * (multiplier.Level <= Owned ? multiplier.Value : 1));
    }


    private void OnValidate()
    {
      Precalculate();
    }
  }
}
