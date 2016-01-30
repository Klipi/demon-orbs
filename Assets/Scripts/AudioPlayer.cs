using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoundType
{
	HIT,

	DRAG,

	SELECT,

	MISS
}

public class AudioPlayer : MonoBehaviour {

	private static AudioPlayer _instance;

	public static AudioPlayer Instance
	{
		get
		{
			if (_instance == null)
			{
				Debug.LogWarning("No AudioPlayer in scene!");
			}

			return _instance;
		}
	}

	[SerializeField]
	private AudioClip[]	selectSounds;

	[SerializeField]
	private AudioClip	dragSound;

	[SerializeField]
	private AudioClip[] 	hitSounds;

	[SerializeField]
	private AudioClip[] missSounds;

	// Use this for initialization
	void Awake () {
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

	private AudioClip SelectRandom(AudioClip[] clipArray)
	{
		int index = Random.Range(0, clipArray.Length);
		return clipArray[index];		
	}

	private AudioClip GetSoundForType(SoundType type)
	{
		int index;
		switch (type)
		{
			case SoundType.SELECT:
				return SelectRandom(selectSounds);
			case SoundType.DRAG:
				return dragSound;
			case SoundType.HIT:
				return SelectRandom(hitSounds);
			case SoundType.MISS:
				return SelectRandom(missSounds);
		
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
