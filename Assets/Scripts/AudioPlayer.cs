using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoundType
{
	DRAG,

	SELECT
}

public class AudioPlayer : MonoBehaviour {

	private static AudioPlayer _instance;

	public static AudioPlayer Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayer>();
			}

			return _instance;
		}
	}

	[SerializeField]
	private AudioClip[]	selectSounds;

	[SerializeField]
	private AudioClip	dragSound;

	// Use this for initialization
	void Start () {
		if (_instance == null)
		{
			_instance = this;
		}
		else
		{
			Debug.LogWarning("Multiple AudioPlayers present!");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private AudioClip GetSoundForType(SoundType type)
	{
		switch (type)
		{
			case SoundType.SELECT:
				int index = Random.Range(0, selectSounds.Length);
				return selectSounds[index];
				break;
			case SoundType.DRAG:
				return dragSound;
			default:
				Debug.LogWarning(string.Format("No sound set for type {0}", type));
				return new AudioClip();
		}
	}

	public void PlaySound(SoundType type)
	{
		AudioClip clip = GetSoundForType(type);
		AudioSource audio = gameObject.AddComponent<AudioSource>();
		audio.PlayOneShot(clip);
	}

	public AudioSource GetAudioSource(SoundType type)
	{
		AudioSource audio = gameObject.AddComponent<AudioSource>();
		AudioClip clip = GetSoundForType(type);

		audio.clip = clip;

		return audio;
	}
}
