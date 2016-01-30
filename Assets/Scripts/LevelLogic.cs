using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelLogic : MonoBehaviour 
{
	private static 	LevelLogic 						_instance;

	public static	LevelLogic 						Instance
	{
		get
		{
			if (_instance == null)
			{
				Debug.LogWarning("No LevelLogic object in scene!");
			}

			return _instance;
		}
	}

	[SerializeField]
	private GameObject								enemyPrefab;

	private LevelState								currentState;

	public 	LevelConfig 							LevelConfig 	= LevelConfig.DefaultConfig;

	private EnemyController							currentEnemy;

	void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
		}
		else
		{
			Debug.LogWarning("Multiple LevelLogic objects in scene!");
		}
	}

	// Use this for initialization
	void Start () {
		Initialize();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnEnemy()
	{
		if (currentEnemy != null)
		{
			Debug.LogWarning("Trying to spawn a second enemy to scene!");
		}

		int enemyIndex = UnityEngine.Random.Range(0, currentState.EnemiesInLevel.Count);
		EnemyConfig enemyToSpawn = currentState.EnemiesInLevel[enemyIndex];

		currentEnemy = GameObject.Instantiate(enemyPrefab).GetComponent<EnemyController>();

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

		currentEnemy.SequenceLength = enemyToSpawn.SequenceLength;
		currentEnemy.OnSequenceChanged += EnemyController_OnSequenceChanged;
		currentEnemy.EnterGameArea();
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

	public void VerifySequence(List<Orb> sequence)
	{
		Debug.Log (string.Format("Verification result: {0}", currentEnemy.CompareSequence(sequence)));
	}
}
