using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using IdleEngine.Generators;
using IdleEngine.Sessions;


    public class SaveSystem : MonoBehaviour
    {
        string json;
        string json2;
        Generatordata generator;
        Generaldata general;
        public List<Generatordata> generatorList;

        public Session session;

        void Awake()
        {

            generatorList = new List<Generatordata>();
        }

        void Start()
        {
            // DIESEN TEIL IN IDLEENGINE:
            // SaveGeneratorsJson();
            // SaveGeneralJson();
        }


        public void SaveGeneratorsJson()
        {

            Debug.Log("yessgenerator");
            // Alle Generatoren durch TYP finden
            foreach (Generator gObject in GameObject.FindObjectsOfType<Generator>())
            {
                // Daten in neue Liste schreiben
                Debug.Log(gObject);
                return;
                // generator = new Generatordata(gObject.Owned, gObject.transform.position, gObject.multipliers);
                //generatorList.Add(generator);
            }

            //  Save: Score, Avg. Performance, Level

            // generator = new Generaldata(gObject.owned, gObject.transform.position, gObject.multipliers);
            // generatorList.Add(generator);


            json = JsonUtility.ToJson(generatorList);
            File.WriteAllText(Application.persistentDataPath + "\\save.txt", json);


        }

        public void SaveGeneralJson()
        {
            Debug.Log("yessgeneral");
            general = new Generaldata(session.Money, session.Level, session.LastTicks);

            json2 = JsonUtility.ToJson(general);
            File.WriteAllText(Application.persistentDataPath + "\\savegeneral.txt", json2);


        }
    public void reee()
    {
        Debug.Log("reee");
       
    }
}
/*

[Serializable]
public class Generatordata
{
    public int Owned;

    public Multiplier[] Multipliers;

    public Vector3 Position;

    public Generatordata(int owned, Vector3 position, Multiplier[] multipliers)
    {
        this.Owned = owned;
        this.Multipliers = multipliers;
        this.Position = position;
    }
}



[Serializable]
public class Generaldata
{
    public double Money;

    public double Level;

    public long LastTicks;


    public Generaldata(double money, double level, long lastTicks)
    {
        this.Money = money;
        this.Level = level;
        this.LastTicks = lastTicks;
    }
}
*/

