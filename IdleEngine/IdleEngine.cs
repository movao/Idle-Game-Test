using System;
using IdleEngine.Sessions;
using UnityEngine;
using IdleEngine.Generators;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace IdleEngine
{
    public class IdleEngine : MonoBehaviour
    {
        public Session Session;

        string json;
        string json2;
        Generatordata generator;
        Generaldata general;
        public List<Generatordata> generatorList;


        private void Awake()
        {
            generatorList = new List<Generatordata>();
            Session = GameObject.Find("Session").GetComponent<Session>();
        }

        private void Update()
        {
            Session.Tick(Time.deltaTime);
        }

        private void OnEnable()
        {
            // ShaftManager.Instance.LoadShaftCost();
            Session.CalculateOfflineProgression();
            Debug.Log("Load");
        }

        private void OnDisable()
        {


            Session.LastTicks = DateTime.UtcNow.Ticks;
            SaveGeneralJson();
            SaveGeneratorsJson();
            //  ShaftManager.Instance.SaveShaftCost();
            Debug.Log("Save!");
        }




        public void SaveGeneratorsJson()
        {
            foreach (Generator gObject in Session.Generators)
            {   
            generator = new Generatordata(gObject.Owned, gObject.pos, gObject.Multipliers);
            generatorList.Add(generator);
            }

            Debug.Log("Generators: " + generatorList);

            json = JsonUtility.ToJson(generatorList);
            File.WriteAllText(Application.persistentDataPath + "\\save.txt", json);

            Debug.Log("Json: " + json);

        }


        public void SaveGeneralJson()
        {
            general = new Generaldata(Session.Money, Session.Level, Session.LastTicks);

            json2 = JsonUtility.ToJson(general);
            File.WriteAllText(Application.persistentDataPath + "\\savegeneral.txt", json2);
            Debug.Log("General: " + json2);

        }
    }
}


[Serializable]
public class Generatordata
{
    [SerializeField] public int Owned;

    [SerializeField] public Multiplier[] Multipliers;

    [SerializeField] public float PositionY;

    public Generatordata(int owned, float position, Multiplier[] multipliers)
    {
        this.Owned = owned;
        this.Multipliers = multipliers;
        this.PositionY = position;
    }
}



[Serializable]
public class Generaldata
{
    [SerializeField] public double Money;

    [SerializeField] public double Level;

    [SerializeField] public long LastTicks;


    public Generaldata(double money, double level, long lastTicks)
    {
        this.Money = money;
        this.Level = level;
        this.LastTicks = lastTicks;
    }
}

