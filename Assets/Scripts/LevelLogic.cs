﻿using UnityEngine;
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
		currentState.TimeLeft -= Time.deltaTime;

		if (currentState.TimeLeft < 0f)
		{
			Debug.Log("TODO: Trigger game over");
		}
	}

	void SpawnEnemy()
	{
		if (currentEnemy != null)
		{
			Debug.LogWarning("Trying to spawn a second enemy to scene!");
		}

		EnemyConfig enemyToSpawn = currentState.Enemy;

		currentEnemy = GameObject.Instantiate(enemyPrefab).GetComponent<EnemyController>();

		switch (enemyToSpawn.Type)
		{
			case EnemyType.DRAGON:
				Debug.Log("Spawning Dragon");
				currentEnemy.GetComponent<SpriteRenderer>().sprite = Utils.LoadSprite("Enemies/monster_01");
				break;
			case EnemyType.LIZARD:
				Debug.Log("Spawning Lizard");
				currentEnemy.GetComponent<SpriteRenderer>().sprite = Utils.LoadSprite("Enemies/monster_02");
				break;
			case EnemyType.IMP:
				Debug.Log("Spawning Imp Boss");
				currentEnemy.GetComponent<SpriteRenderer>().sprite = Utils.LoadSprite("Enemies/DemonBlob");
				break;
			default:
				Debug.LogWarning(string.Format("Couldn't find enemy type {0}", enemyToSpawn.Type));
				break;
		}

		currentEnemy.Config = enemyToSpawn;
		currentEnemy.OnSequenceChanged += EnemyController_OnSequenceChanged;
		currentEnemy.EnterGameArea();
	}

	void EnemyController_OnSequenceChanged (object sender, SequenceEventArgs e)
	{
		OrbInfoController.Instance.DrawNewOrbs(e.Sequence);
		OrbSequenceController.Instance.GenerateNewColors(e.Sequence);
	}

	void Initialize()
	{
		currentState = new LevelState(this.LevelConfig);

		this.SpawnEnemy();
	}

	void AddTimeToTimer()
	{
		currentState.TimeLeft += currentState.CurrentRound.TimeBoost;
	}

	public void VerifySequence(List<OrbController> sequence)
	{
		SequenceResult res = currentEnemy.CompareSequence(sequence);
		Debug.Log (string.Format("Verification result: {0}", res));

		if (res == SequenceResult.HIT)
		{
			AddTimeToTimer();	
		}
		AudioPlayer.Instance.PlaySound(res == SequenceResult.HIT ? SoundType.HIT : SoundType.MISS);

	}
}
