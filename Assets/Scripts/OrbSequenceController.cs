using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class OrbSequenceController : MonoBehaviour {
	
	private List<OrbType> currentSequence;

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
	public bool SelectOrb(OrbType type)
	{
		if (currentSequence != null)
		{
			currentSequence.Add(type);
			return true;
		}

		return false;
	}

	public void StartSequence()
	{
		currentSequence = new List<OrbType>();
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

	private void CheckSequence(List<OrbType> sequence)
	{
		Debug.Log(string.Format("Got {0} orbs in sequence", sequence.Count));
		// TODO: Check if sequence has some effect on demon
	}
}
