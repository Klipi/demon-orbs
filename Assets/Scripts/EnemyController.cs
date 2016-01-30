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

	public List<OrbType> 	Sequence;

	public int				SequenceLength;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public SequenceResult CompareSequence(List<OrbType> sequence)
	{
		if (sequence.SequenceEqual(this.Sequence))
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
		result.Color = OrbColor.BLUE;
		result.Symbol = OrbSymbol.TRIANGLE;

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
