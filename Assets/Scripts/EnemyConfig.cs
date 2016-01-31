using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemyType
{
	DRAGON,
    DRAGON2,
	LIZARD,
    LIZARD2,
    BOSS,
    BOSS2,
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

			EnemyConfig result = new EnemyConfig();
			result.Type = EnemyType.DRAGON;
			result.Rounds = rounds;

			result.InitialTime = 10f;

			return result;
		}
	}

    public static EnemyConfig Level0
    {
        get
        {
            List<Round> rounds = new List<Round>();
            rounds.Add(new Round(5f, 3));
            rounds.Add(new Round(8f, 3));
            rounds.Add(new Round(10f, 3));

            EnemyConfig result = new EnemyConfig();
            result.Type = EnemyType.DRAGON;
            result.Rounds = rounds;

            result.InitialTime = 10f;

            return result;
        }
    }

    public static EnemyConfig Level1
    {
        get
        {
            List<Round> rounds = new List<Round>();
            rounds.Add(new Round(8f, 3));
            rounds.Add(new Round(6f, 3));
            rounds.Add(new Round(8f, 4));

            EnemyConfig result = new EnemyConfig();
            result.Type = EnemyType.LIZARD;
            result.Rounds = rounds;

            result.InitialTime = 10f;

            return result;
        }
    }

    public static EnemyConfig Level2
    {
        get
        {
            List<Round> rounds = new List<Round>();
            rounds.Add(new Round(4f, 3));
            rounds.Add(new Round(3f, 4));
            rounds.Add(new Round(2f, 5));

            EnemyConfig result = new EnemyConfig();
            result.Type = EnemyType.BOSS;
            result.Rounds = rounds;

            result.InitialTime = 10f;

            return result;
        }
    }

    public static EnemyConfig Level3
    {
        get
        {
            List<Round> rounds = new List<Round>();
            rounds.Add(new Round(4f, 3));
            rounds.Add(new Round(3f, 4));
            rounds.Add(new Round(2f, 5));

            EnemyConfig result = new EnemyConfig();
            result.Type = EnemyType.DRAGON2;
            result.Rounds = rounds;

            result.InitialTime = 10f;

            return result;
        }
    }

    public static EnemyConfig Level4
    {
        get
        {
            List<Round> rounds = new List<Round>();
            rounds.Add(new Round(4f, 3));
            rounds.Add(new Round(3f, 4));
            rounds.Add(new Round(2f, 5));

            EnemyConfig result = new EnemyConfig();
            result.Type = EnemyType.LIZARD2;
            result.Rounds = rounds;

            result.InitialTime = 10f;

            return result;
        }
    }

    public static EnemyConfig Level5
    {
        get
        {
            List<Round> rounds = new List<Round>();
            rounds.Add(new Round(4f, 3));
            rounds.Add(new Round(3f, 4));
            rounds.Add(new Round(2f, 5));

            EnemyConfig result = new EnemyConfig();
            result.Type = EnemyType.BOSS2;
            result.Rounds = rounds;

            result.InitialTime = 10f;

            return result;
        }
    }

    public static EnemyConfig Level6
    {
        get
        {
            List<Round> rounds = new List<Round>();
            rounds.Add(new Round(4f, 3));
            rounds.Add(new Round(3f, 4));
            rounds.Add(new Round(2f, 5));

            EnemyConfig result = new EnemyConfig();
            result.Type = EnemyType.IMP;
            result.Rounds = rounds;

            result.InitialTime = 10f;

            return result;
        }
    }

}


