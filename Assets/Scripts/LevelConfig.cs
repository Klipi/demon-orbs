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

	public static LevelConfig Level0
	{
		get
		{
			return new LevelConfig(EnemyConfig.DefaultEnemy);
		}

	}

	public static LevelConfig Level1
	{
		get
		{
			return new LevelConfig(EnemyConfig.DefaultEnemy);
		}

	}

	public static LevelConfig Level2
	{
		get
		{
			return new LevelConfig(EnemyConfig.DefaultEnemy);
		}

	}

	public static LevelConfig Level3
	{
		get
		{
			return new LevelConfig(EnemyConfig.DefaultEnemy);
		}

	}

	public static LevelConfig Level4
	{
		get
		{
			return new LevelConfig(EnemyConfig.DefaultEnemy);
		}

	}

	public static LevelConfig Level5
	{
		get
		{
			return new LevelConfig(EnemyConfig.DefaultEnemy);
		}

	}

	public static LevelConfig Level6
	{
		get
		{
			return new LevelConfig(EnemyConfig.DefaultEnemy);
		}

	}

}

