using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemyType
{
	DRAGON,
	LIZARD,
	IMP
}

public class Round
{
	public float TimeBoost;
	public int SequenceLength;

	public Round(float time, int sequenceLength)
	{
		TimeBoost = time;
		SequenceLength = sequenceLength;
	}
}

public class EnemyConfig
{
	public EnemyType 	Type;
	public List<Round>	Rounds;
	public float		InitialTime;

	public static EnemyConfig DefaultEnemy
	{
		get
		{	
			List<Round> rounds = new List<Round>();
			rounds.Add(new Round(4f, 3));
			rounds.Add(new Round(3f, 4));
			rounds.Add(new Round(2f, 5));

			EnemyConfig result = new EnemyConfig();
			result.Type = EnemyType.DRAGON;
			result.Rounds = rounds;

			result.InitialTime = 10f;

			return result;
		}
	}
}


