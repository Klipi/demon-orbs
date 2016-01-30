using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelConfig
{
	public int EnemiesToDefeat;
	public List<EnemyConfig> Enemies;

	public static LevelConfig DefaultConfig
	{
		get
		{
			List<EnemyConfig> enemies = new List<EnemyConfig>();
			enemies.Add(EnemyConfig.DefaultEnemy);

			LevelConfig result = new LevelConfig();
			result.Enemies = enemies;
			result.EnemiesToDefeat = 1;

			return result;
		}
		
	}
}

