using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyController : MonoBehaviour {

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

	void ShowSequence()
	{
		OrbInfoController.Instance.DrawNewOrbs(this.Sequence);
	}

	void GenerateNewSequence()
	{
		Sequence = new List<OrbType>();
		for (int i = 0; i < SequenceLength; i++)
		{
			Sequence.Add(this.GetRandomOrb());
		}

		this.ShowSequence();
	}

	public void EnterGameArea()
	{
		this.gameObject.SetActive(true);

		//TODO: Animate entrance

		this.GenerateNewSequence();

		Debug.Log(string.Format("Generated a {0} orb long sequence", this.Sequence.Count));
	}

}
