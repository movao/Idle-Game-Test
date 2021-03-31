/*
 * using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class Save : MonoBehaviour
{
    string json;
    Generator generator;
    public List<Generator> generatorList;

    void Awake()
    {
        generatorList = new List<Generator>();
    }

    void Start()
    {
         SaveJson();
     }
    

    void SaveJson()
    {

        foreach (GameObject gObject in GameObject.FindObjectsOfType<Generator>()) 
        {

            generator = new Generator(gObject.owned, gObject.transform.position, gObject.multipliers);
            generatorList.Add(generator);

        }
        json = JsonUtility.ToJson(generatorList);
        File.WriteAllText(Application.UserAppDataPath + "\\save.txt", json);

    }

}

[Serializable]
public class Generator
{
    [serilaizeField]
    public int Owned;

    [serilaizeField]
    public Multiplier[] Multipliers;

    [serilaizeField]
    public Vector3 Position;


    public Generator(int owned, Vector3 position, Multiplier[] multipliers)
    {
        this.Owned = owned;
        this.Multipliers = multipliers;
        this.Position = position;
    }
}


// Dann Datei lesen
// for-Schleife json-Datei durchgehen, GameoObject instanziieren und Werte zuweisen.

*/