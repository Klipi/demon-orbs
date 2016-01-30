using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemyType
{
	IMP
}

public class EnemyConfig
{
	public EnemyType 	Type;
	public int			SequenceLength;

	public static EnemyConfig DefaultEnemy
	{
		get
		{
			EnemyConfig result = new EnemyConfig();
			result.Type = EnemyType.IMP;
			result.SequenceLength = 4;

			return result;
		}
	}
}


