﻿using UnityEngine;
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

	public List<OrbType> 	Sequence;

	public int				SequenceLength;

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

	public SequenceResult CompareSequence(List<Orb> sequence)
	{
		List<OrbType> types = sequence.Select(s => s.Type).ToList();

		PrintSequence(types);
		PrintSequence(this.Sequence);

		if (types.SequenceEqual(this.Sequence))
		{
			return SequenceResult.HIT;
		}
		else
		{
			return SequenceResult.MISS;
		}
	} 

	OrbType GetRandomOrb()
	{
		OrbType result = new OrbType();
		result.Color = OrbColorEnum.BLUE;
//		result.Symbol = OrbSymbolEnum.SQUARE;

		return result;
	}

	void GenerateNewSequence()
	{
		Sequence = new List<OrbType>();
		for (int i = 0; i < SequenceLength; i++)
		{
			Sequence.Add(this.GetRandomOrb());
		}


		if (OnSequenceChanged != null)
		{
			OnSequenceChanged.Invoke(this, new SequenceEventArgs(this.Sequence));
		}
	}

	public void EnterGameArea()
	{
		this.gameObject.SetActive(true);

		//TODO: Animate entrance

		this.GenerateNewSequence();

		Debug.Log(string.Format("Generated a {0} orb long sequence", this.Sequence.Count));
	}

}
