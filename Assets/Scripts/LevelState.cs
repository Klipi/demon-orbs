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

	private int _currentRound = 0;

	public Round CurrentRound
	{
		get
		{
			return Enemy.Rounds[_currentRound];
		}
	}

	public LevelState(LevelConfig config)
	{
		TimeLeft = config.Enemy.InitialTime;
		Enemy = config.Enemy;
	}
}

