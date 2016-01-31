using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsButtonController : MonoBehaviour {

	public Text soundOnText;
	public Button menuButton;
	public Button levelsButton;

    private PersistentData persistentData;


	void Start () {
        persistentData = GameObject.Find("SceneEssentials").GetComponent<PersistentData>();

        if (AudioListener.volume == 0) {
			soundOnText.text = "Sound: off";
		} else {
			soundOnText.text = "Sound: on";
		}

		Scene currentScene = SceneManager.GetActiveScene ();
		if (currentScene == SceneManager.GetSceneByName ("levelselect")) {
			levelsButton.gameObject.SetActive (false);
		} else if (currentScene == SceneManager.GetSceneByName ("menu")) {
			levelsButton.gameObject.SetActive (false);
			menuButton.gameObject.SetActive (false);
		} else if (currentScene == SceneManager.GetSceneByName ("main")){
			LevelLogic.Instance.paused = true;
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
        persistentData.Reset = true;
	}

	public void ClickUnlock () {
		Debug.Log ("Unlock clicked");
        persistentData.Unlock = true;
	}

	public void ClickLevels () {
		SceneManager.LoadScene ("levelselect");
	}

	public void ClickMenu () {
		SceneManager.LoadScene ("menu");
	}

	public void ClickClose () {
		Scene currentScene = SceneManager.GetActiveScene ();
		if (currentScene == SceneManager.GetSceneByName ("main")) {
			LevelLogic.Instance.paused = false;
		}
		SceneManager.UnloadScene ("settings");
	}
}	
