using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IdleEngine.Generators;
using IdleEngine.Sessions;

public class UpgradeButton : MonoBehaviour
{
	public Button yourButton;
	[SerializeField] public Generator Generator;
	[SerializeField] public Session session;

	void Start()
	{
		session = GameObject.Find("Session").GetComponent<Session>();
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		
		Generator.Build(session);
	}
}
