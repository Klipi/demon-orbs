using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour {

	public void ClickPlay () {
		SceneManager.LoadScene ("levelselect");
	}

	public void ClickSettings () {
		SceneManager.LoadScene ("settings");
	}
}	
