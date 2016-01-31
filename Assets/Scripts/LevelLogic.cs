using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

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
	private GameObject								defaultEnemyPrefab;

	[SerializeField]
	private GameObject								dragonPrefab1;

	[SerializeField]
	private GameObject								dragonPrefab2;

	[SerializeField]
	private GameObject 								lizardPrefab1;

	[SerializeField]
	private GameObject								lizardPrefab2;

	private LevelState								currentState;

	public 	LevelConfig 							LevelConfig 	= LevelConfig.DefaultConfig;

	private EnemyController							currentEnemy;

	private bool 									playing			= true;

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
		if (!playing)
			return;

		currentState.TimeLeft -= Time.deltaTime;

		TimeHandler.Instance.SetRemainingTime(currentState.TimeLeft);
		if (currentState.TimeLeft < 0f)
		{
			GameOver (false);
		}
	}

	void GameOver (bool won)
	{
		playing = false;
		OrbSequenceController.Instance.StopRound ();


		if (won) {
			AudioPlayer.Instance.PlaySound(SoundType.WIN);
			PersistentData data = GameObject.Find ("SceneEssentials").GetComponent<PersistentData> ();
			if (data.CurrentLevel == data.MaxLevel) {
				data.MaxLevel++;
			}


		} else {
			AudioPlayer.Instance.PlaySound (SoundType.GAME_OVER);

		}

		SceneManager.LoadScene ("levelselect", LoadSceneMode.Single);

	}

	void SpawnEnemy()
	{
		if (currentEnemy != null)
		{
			Debug.LogWarning("Trying to spawn a second enemy to scene!");
		}

		EnemyConfig enemyToSpawn = currentState.Enemy;


		switch (enemyToSpawn.Type)
		{
			case EnemyType.DRAGON:
				Debug.Log("Spawning Dragon");
				currentEnemy = GameObject.Instantiate(dragonPrefab1).GetComponent<EnemyController>();
//				currentEnemy.GetComponent<SpriteRenderer>().sprite = Utils.LoadSprite("Enemies/monster_01");
				break;
			case EnemyType.LIZARD:
				Debug.Log("Spawning Lizard");
				currentEnemy = GameObject.Instantiate(lizardPrefab1).GetComponent<EnemyController>();

//				currentEnemy.GetComponent<SpriteRenderer>().sprite = Utils.LoadSprite("Enemies/monster_02");
				break;
			case EnemyType.IMP:
				Debug.Log("Spawning Imp Boss");
				currentEnemy = GameObject.Instantiate(defaultEnemyPrefab).GetComponent<EnemyController>();

//				currentEnemy.GetComponent<SpriteRenderer>().sprite = Utils.LoadSprite("Enemies/DemonBlob");
				break;
			default:
				Debug.LogWarning(string.Format("Couldn't find enemy type {0}", enemyToSpawn.Type));
				break;
		}

		currentEnemy.Config = enemyToSpawn;
		currentEnemy.OnSequenceChanged += EnemyController_OnSequenceChanged;
		currentEnemy.OnEnemyDefeated += CurrentEnemy_OnEnemyDefeated;
		currentEnemy.EnterGameArea();
	}

	void EnemyController_OnSequenceChanged (object sender, SequenceEventArgs e)
	{
		currentState.AdvanceRound();
		OrbInfoController.Instance.DrawNewOrbs(e.Sequence);
		OrbSequenceController.Instance.GenerateNewColors(e.Sequence);
		ScoreController.Instance.SetScore(currentState.Score, currentState.Enemy.Rounds.Count);
	}

	void CurrentEnemy_OnEnemyDefeated (object sender, EventArgs e)
	{
		currentState.AdvanceRound();
		ScoreController.Instance.SetScore(currentState.Score, currentState.Enemy.Rounds.Count);
		GameOver (true);
	}

	void Initialize()
	{
		int levelToLoad = GameObject.Find("SceneEssentials").GetComponent<PersistentData>().CurrentLevel;

		LevelConfig newLevel;
		switch (levelToLoad) {
		case 0:
			newLevel = LevelConfig.Level0;
			break;
		case 1:
			newLevel = LevelConfig.Level1;
			break;
		case 2:
			newLevel = LevelConfig.Level2;
			break;
		case 3:
			newLevel = LevelConfig.Level3;
			break;
		case 4:
			newLevel = LevelConfig.Level4;
			break;
		case 5:
			newLevel = LevelConfig.Level5;
			break;
		case 6:
			newLevel = LevelConfig.Level6;
			break;
		default:
			newLevel = LevelConfig.DefaultConfig;
			break;
		

		}
		currentState = new LevelState(this.LevelConfig);
		ScoreController.Instance.SetScore(currentState.Score, currentState.Enemy.Rounds.Count);

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
