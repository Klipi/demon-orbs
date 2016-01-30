using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class OrbSequenceController : MonoBehaviour {
	
	private List<OrbController> currentSequence;

	public delegate void SequenceEndHandler(object sender, EventArgs e);

	public event SequenceEndHandler OnSequenceEnd;

	private bool playing = true;

	private static OrbSequenceController _instance;

	public static OrbSequenceController Instance
	{
		get
		{
			if (_instance == null)
			{
				Debug.LogWarning("No instance found for OrbSequenceController");
			}

			return _instance;
		}
	}

	void Awake () 
	{
		if (_instance == null)
		{
			_instance = this;
		}
		else
		{
			Debug.LogWarning("Several OrbSequenceControllers in scene!");
		}
	}
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
	public bool SelectOrb(OrbController controller)
	{
		if (currentSequence == null || !playing)
			return false;
		
		if (currentSequence.Count() == 0 || currentSequence.Last().GetNeighbors().Contains(controller))
		{
			currentSequence.Add(controller);
			return true;
		}

		return false;
	}

	public void StopRound()
	{
		playing = false;
	}

	public void StartSequence()
	{
		currentSequence = new List<OrbController>();
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

	private void CheckSequence(List<OrbController> sequence)
	{
		Debug.Log(string.Format("Got {0} orbs in sequence", sequence.Count));
		LevelLogic.Instance.VerifySequence(sequence);
	}

	private List<OrbController> GetOrbs()
	{
		List<OrbController> orbs = new List<OrbController>();
		for (int i = 0; i < transform.childCount; i++)
		{
			OrbController orb = transform.GetChild(i).GetComponent<OrbController>();
			if (orb != null)
				orbs.Add(orb);
		}

		return orbs;
	}

	public void GenerateNewColors(List<OrbType> sequence)
	{
		List<OrbController> orbs = GetOrbs();

		int initialIndex = UnityEngine.Random.Range(0, orbs.Count);

		OrbController currentOrb = orbs[initialIndex];
		List<OrbController> usedOrbs = new List<OrbController>();

		for (int i = 0; i < sequence.Count; i++)
		{
			currentOrb.Type = sequence[i];
			Debug.Log(string.Format("Set orb at {1} / {2} type to {0}", currentOrb.Type.Color, currentOrb.Position.Direction, currentOrb.Position.Ring));
			usedOrbs.Add(currentOrb);
			IEnumerable<OrbController> unusedNeighbors = currentOrb.GetNeighbors().Except(usedOrbs);

			if (unusedNeighbors.Count() == 0 && i < sequence.Count() -1)
			{
				Debug.LogError("Greedy pattern search failed! Rethink algorithm!");
			}
			else
			{
				int index = UnityEngine.Random.Range(0, unusedNeighbors.Count());
				currentOrb = unusedNeighbors.ToList()[index];
			}

		}

		IEnumerable<OrbController> unusedOrbs = orbs.Except(usedOrbs);

		foreach (OrbController orb in unusedOrbs)
		{
			orb.Type = OrbType.GetRandomOrbType();
			Debug.Log(string.Format("Set orb at {1} / {2} type to {0}", orb.Type.Color, orb.Position.Direction, orb.Position.Ring));
		}

	}

}
