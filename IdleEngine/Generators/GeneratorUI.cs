using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using IdleEngine.Generators;
using IdleEngine.Sessions;


public class GeneratorUI : MonoBehaviour
    {

    [Header("General")]
    [SerializeField] public Generator Generator;
    [SerializeField] public Session session;
    //[SerializeField] private TextMeshProUGUI moneyTMP; // Produce: calculatedSum / ProductionTimeInSecond

    [Header("Prouction")]
    [SerializeField] private TextMeshProUGUI productionTimeInSecondsTMP;
    [SerializeField] private TextMeshProUGUI calculatedSumTMP;

    [Header("Upgrade")]
    [SerializeField] private TextMeshProUGUI upgradeLevelTMP; // UpgradeCount
    private double upgradeLevel;
    [SerializeField] private TextMeshProUGUI nextUpgradeCostTMP; // NextUpgradeCostForOne
    private double nextUpgradeCost;
    public Button upgradeButton;

    [Header("Bar")]
    public Bar healthBar;
    public float maxHealth = 0;
    public float currentHealth;

    [Header("New Generator")]
    [SerializeField] private TextMeshProUGUI nextGeneratorCostTMP;
    public Button buyNewGeneratorButton;
    [SerializeField] private GameObject buyNewShaftButton;
    // [SerializeField] private TextMeshProUGUI nextPerformanceProfitTMP; // Erstmal weglassen




    // Start is called before the first frame update
    void Start()
        {
           Generator = GetComponent<Generator>();
           session = GameObject.Find("Session").GetComponent<Session>();

    }

     // Update is called once per frame
     void Update()
        {
            upgradeLevel = Generator.Owned;
            upgradeLevelTMP.text = upgradeLevel.ToString();

            nextUpgradeCost = Math.Floor(Generator.NextBuildingCostsForOne);
            nextUpgradeCostTMP.text = MoneyFormat.Default(nextUpgradeCost);


            double BaseRevenue = Math.Floor(Generator.BaseRevenue);
            // double _multiplier = Generator.Multipliers;

            double calculatedSum = BaseRevenue * upgradeLevel; // * _multiplier;
        
            calculatedSumTMP.text = MoneyFormat.Default(calculatedSum);

            nextGeneratorCostTMP.text = MoneyFormat.Default(ShaftManager.Instance.NewShaftCost);

            float countdownf = Generator.ProductionTimeInSeconds - Generator.ProductionCycleInSeconds;
      
            int countdown = (int)Math.Round(countdownf);


            TimeSpan result = TimeSpan.FromHours(countdown);
            string fromTimeString = result.ToString("ss':'mm':'hh");
            productionTimeInSecondsTMP.text = fromTimeString;


            maxHealth = Generator.ProductionTimeInSeconds;
            healthBar.SetMaxHealth(maxHealth);

            if (maxHealth <= 1)
                {
                    currentHealth = maxHealth;
                    healthBar.SetHealth(currentHealth);
                }
                else
                {

                    currentHealth = Generator.ProductionCycleInSeconds;
                    healthBar.SetHealth(currentHealth);

                }

        
                if (session.Money < ShaftManager.Instance.NewShaftCost)
                {
                    buyNewGeneratorButton.interactable = false;

                }
                else
                {
                    buyNewGeneratorButton.interactable = true;
                }

                


            if (!Generator.CanBeBuild(session) == true)
                {
                    upgradeButton.interactable = false;

                }
                else
                {
                    upgradeButton.interactable = true;
                }


        }

    public void BuyNewShaft()
    {
        if(session.Money > ShaftManager.Instance.NewShaftCost)
        {
            ShaftManager.Instance.AddShaft();
            Debug.Log("Geld: " + session.Money + " und Next Cost:" + ShaftManager.Instance.NewShaftCost);
            session.Money -= ShaftManager.Instance.NewShaftCost;
           buyNewShaftButton.SetActive(false);
            
        }
    } 

       
    }





