using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelState
{
	public float TimeLeft
	{
		get;
		set;
	}

	public EnemyConfig Enemy;

	private int _currentRound = -1;

	public int Score
	{
		get
		{
			return _currentRound;
		}
	}

	public Round CurrentRound
	{
		get
		{
			return Enemy.Rounds[_currentRound];
		}
	}

	public void AdvanceRound()
	{
		Debug.Log("Advancing");
		_currentRound++;
	}

	public LevelState(LevelConfig config)
	{
		TimeLeft = config.Enemy.InitialTime;
		Enemy = config.Enemy;
	}
}

