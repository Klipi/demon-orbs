using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsButtonController : MonoBehaviour {

	public Text soundOnText;

	void Start () {
		if (AudioListener.volume == 0) {
			soundOnText.text = "Sound: off";
		} else {
			soundOnText.text = "Sound: on";
		}
	}
		
	public void ClickSound () {
		if (AudioListener.volume == 0) {
			AudioListener.volume = 1;
			soundOnText.text = "Sound: on";
		} else {
			AudioListener.volume = 0;
			soundOnText.text = "Sound: off";
		}
	}

	public void ClickReset () {
		Debug.Log ("Reset clicked");
	}

	public void ClickUnlock () {
		Debug.Log ("Unlock clicked");
	}

	public void ClickBack () {
		SceneManager.LoadScene ("menu");
	}
}	
