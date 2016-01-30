using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class TimeHandler : MonoBehaviour
{
	private static TimeHandler _instance;

	public static TimeHandler Instance
	{
		get
		{
			if (_instance == null)
			{
				Debug.LogWarning("No TimeHandler instance in scene!");
			}

			return _instance;
		}
	}


	public void SetRemainingTime(float timeLeft)
	{
		GetComponent<Text>().text = timeLeft.ToString();
	}

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
		}
		else
		{
			Debug.LogWarning("Several TimeHandler objects in scene!");
		}
	}	
}

