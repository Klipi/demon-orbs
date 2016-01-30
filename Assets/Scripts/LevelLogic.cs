using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelLogic : MonoBehaviour 
{


	[SerializeField]
	private GameObject								enemyPrefab;

	private LevelState								currentState;

	public 	LevelConfig 							LevelConfig 	= LevelConfig.DefaultConfig;

	// Use this for initialization
	void Start () {
		Initialize();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnEnemy()
	{
		int enemyIndex = UnityEngine.Random.Range(0, currentState.EnemiesInLevel.Count);
		EnemyConfig enemyToSpawn = currentState.EnemiesInLevel[enemyIndex];

		EnemyController enemyController = GameObject.Instantiate(enemyPrefab).GetComponent<EnemyController>();

		switch (enemyToSpawn.Type)
		{
			case EnemyType.IMP:
				Debug.Log("Spawning Imp");
				// TODO: Set sprite
				break;
			default:
				Debug.LogWarning(string.Format("Couldn't find enemy type {0}", enemyToSpawn.Type));
				break;
		}

		enemyController.SequenceLength = enemyToSpawn.SequenceLength;
		enemyController.OnSequenceChanged += EnemyController_OnSequenceChanged;
		enemyController.EnterGameArea();
	}

	void EnemyController_OnSequenceChanged (object sender, SequenceEventArgs e)
	{
		OrbInfoController.Instance.DrawNewOrbs(e.Sequence);
	}

	void Initialize()
	{
		currentState = new LevelState(this.LevelConfig);

		this.SpawnEnemy();
	}
}
