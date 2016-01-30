using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelState
{
	public int EnemiesDefeated;
	public int EnemiesToDefeat;

	public List<EnemyConfig> EnemiesInLevel;

	public LevelState(LevelConfig config)
	{
		EnemiesDefeated = 0;
		EnemiesToDefeat = config.EnemiesToDefeat;
		EnemiesInLevel = config.Enemies;
	}
}

