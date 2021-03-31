using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using IdleEngine.Sessions;
using IdleEngine;
using System;

namespace IdleEngineUI
{

    public class IdleEngineUI : MonoBehaviour
    {


        [SerializeField] public TextMeshProUGUI moneyTMP; // Produce: calculatedSum / ProductionTimeInSecond
        [SerializeField] public Session Session;

        private double money;

        public float maxHealth = 0;
        public double currentHealth;

        public Bar healthBar;

        // Start is called before the first frame update
        void Start()
        {
           Session = GameObject.Find("Session").GetComponent<Session>();

            maxHealth = 500000;
            healthBar.SetMaxHealth(maxHealth);


        }

        // Update is called once per frame
        void Update()
        {
            money = Math.Floor(Session.Money);
            moneyTMP.text = MoneyFormat.Default(money);

            currentHealth = money;
            healthBar.SetHealth((float)currentHealth);

           

        }

    }
}