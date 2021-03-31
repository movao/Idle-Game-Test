using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IdleEngine.Sessions;
using IdleEngine;
using IdleEngine.Generators;


    public class ShaftManager : MonoBehaviour
    {
        public static ShaftManager Instance;
        [SerializeField] public Generator shaftPrefab;
        [SerializeField] private float newShaftYPosition;
        [SerializeField] private int newShaftCost = 500;

        public Session session;

        [SerializeField] public List<Generator> shafts;

       
        public int NewShaftCost => newShaftCost;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            session = GameObject.Find("Session").GetComponent<Session>();
            shafts = session.Generators;
        }

        private int _currentShaftIndex;

        public void AddShaft()
        {
            Transform lastShaft = shafts[_currentShaftIndex].transform;
            Generator newShaft = Instantiate(shaftPrefab, lastShaft.position, Quaternion.identity);
            newShaft.transform.localPosition = new Vector3(lastShaft.position.x, lastShaft.position.y - newShaftYPosition, lastShaft.position.z);
        
            newShaft.transform.parent = GameObject.Find("Content").transform;

            _currentShaftIndex++;
            shafts.Add(newShaft);

            newShaftCost *= 2;
         }
    /*
         public void SaveShaftCost()
        {
            SaveData.Save(newShaftCost, "newShaftCost");
        }

         public void LoadShaftCost()
        {
            newShaftCost = SaveData.Load<int>("newShaftCost");
        }
    */

}
