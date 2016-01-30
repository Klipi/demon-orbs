using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class OrbSequenceController : MonoBehaviour {
	
	private List<Orb> currentSequence;

	public delegate void SequenceEndHandler(object sender, EventArgs e);

	public event SequenceEndHandler OnSequenceEnd;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Adds the given orb to the current sequence if possible.
	/// </summary>
	/// <returns><c>true</c>, if orb could be added, <c>false</c> otherwise.</returns>
	/// <param name="type">Type.</param>
	public bool SelectOrb(OrbType type, OrbPosition position)
	{

		if (currentSequence != null)
		{
			Orb selected = new Orb(type, position);
			currentSequence.Add(selected);
			return true;
		}

		return false;
	}

	public void StartSequence()
	{
		currentSequence = new List<Orb>();
	}

	public void EndSequence()
	{
		CheckSequence(currentSequence);
		currentSequence = null;

		if (OnSequenceEnd != null)
		{
			OnSequenceEnd.Invoke(this, EventArgs.Empty);
		}
	}

	private void CheckSequence(List<Orb> sequence)
	{
		Debug.Log(string.Format("Got {0} orbs in sequence", sequence.Count));
		LevelLogic.Instance.VerifySequence(sequence);
	}
}
