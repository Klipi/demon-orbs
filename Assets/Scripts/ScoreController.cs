using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	[SerializeField]
	private Text scoreText;

	private static ScoreController _instance;

	public static ScoreController Instance
	{
		get
		{
			if (_instance == null)
			{
				Debug.LogWarning("No ScoreController instance in scene!");
			}

			return _instance;
		}
	}

	// Use this for initialization
	void Awake () {
		if (_instance == null)
		{
			_instance = this;
		}
		else
		{
			Debug.LogWarning("Several ScoreControllers in scene!");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetScore(int current, int maximum)
	{
		scoreText.text = string.Format("{0}/{1}", current, maximum);
	}
}
