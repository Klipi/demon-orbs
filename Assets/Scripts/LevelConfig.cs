using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelConfig
{
	public EnemyConfig 	Enemy;

	public LevelConfig(EnemyConfig enemy)
	{
		Enemy = enemy;
	}


	public static LevelConfig DefaultConfig
	{
		get
		{
			return new LevelConfig(EnemyConfig.DefaultEnemy);
		}
		
	}
}

