using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour {

	public void ClickPlay () {
		AudioPlayer.Instance.PlaySound(SoundType.CLICK);
		SceneManager.LoadScene ("levelselect");
	}

	public void ClickSettings () {
		AudioPlayer.Instance.PlaySound(SoundType.CLICK);
		SceneManager.LoadScene ("settings", LoadSceneMode.Additive);
	}
}	
