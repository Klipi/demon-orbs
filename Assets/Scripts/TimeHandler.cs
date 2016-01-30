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

	[SerializeField]
	private RectTransform timeBarFill;

	[SerializeField]
	private float endTopValue = 620f;

	public float InitialTimeLeft = -1f;

	public void SetRemainingTime(float timeLeft)
	{
		if (Mathf.Approximately(InitialTimeLeft, -1f))
		{
			InitialTimeLeft = timeLeft;
		}

		float topValue = Mathf.Lerp(-620f, 0f, timeLeft/InitialTimeLeft);


		timeBarFill.offsetMax = new Vector2(0f, topValue);
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

