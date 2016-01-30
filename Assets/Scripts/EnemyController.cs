using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class SequenceEventArgs : EventArgs
{
	public List<OrbType> Sequence;

	public SequenceEventArgs(List<OrbType> seq)
	{
		Sequence = seq;
	}
}

public class EnemyController : MonoBehaviour {

	public delegate void SequenceChangeHandler(object sender, SequenceEventArgs e);
	public event SequenceChangeHandler OnSequenceChanged;

	public delegate void EnemyDefeatedHandler(object sender, EventArgs e);
	public event EnemyDefeatedHandler OnEnemyDefeated;

	private int 			_currentRound = -1;

	public List<OrbType> 	CurrentSequence;

	public int				CurrentSequenceLength
	{
		get
		{
			return Config.Rounds[_currentRound].SequenceLength;
		}
	}

	public EnemyConfig Config
	{
		get;
		set;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void PrintSequence(List<OrbType> seq)
	{
		Debug.Log("Start sequence");
		foreach (OrbType type in seq)
		{
			Debug.Log(string.Format("Orb type: {0}", type.Color)); //, type.Symbol));
		}
		Debug.Log("End sequence");
	}

	public SequenceResult CompareSequence(List<OrbController> sequence)
	{
		List<OrbType> types = sequence.Select(s => s.Type).ToList();

		if (types.Count != CurrentSequence.Count)
		{
		 	return SequenceResult.MISS;
		}

		for (int i = 0; i < types.Count; i++)
		{
			if (types[i].Color != CurrentSequence[i].Color)
			{
				return SequenceResult.MISS;
			}
		}

		GenerateNewSequence();
		return SequenceResult.HIT;
	} 

	OrbType GetRandomOrb()
	{
		OrbType result = OrbType.GetRandomOrbType();

		return result;
	}

	void GenerateNewSequence()
	{
		_currentRound++;

		if (Config.Rounds.Count() <= _currentRound)
		{
			if (OnEnemyDefeated != null)
			{
				OnEnemyDefeated.Invoke(this, EventArgs.Empty);
			}
			else
			{
				Debug.LogWarning("No listener to enemy defeated event");
			}

			return;
		}

		CurrentSequence = new List<OrbType>();
		for (int i = 0; i < CurrentSequenceLength; i++)
		{
			CurrentSequence.Add(this.GetRandomOrb());
			Debug.Log(CurrentSequence[i].Color);
		}


		if (OnSequenceChanged != null)
		{
			OnSequenceChanged.Invoke(this, new SequenceEventArgs(this.CurrentSequence));
		}
	}

	public void EnterGameArea()
	{
		this.gameObject.SetActive(true);

		//TODO: Animate entrance

		this.GenerateNewSequence();

		Debug.Log(string.Format("Generated a {0} orb long sequence", this.CurrentSequence.Count));
	}

}
