using UnityEngine;
using System.Collections;

public class LevelSelectManager : MonoBehaviour {

    private PersistentData m_persistentData;
    private Camera m_camera;

	// Use this for initialization
	void Start ()
    {
        m_persistentData = GameObject.Find("SceneEssentials").GetComponent<PersistentData>();

        m_camera = Camera.main;
        LevelSelectCamera cam = m_camera.GetComponent<LevelSelectCamera>();

        //Debug
        cam.MinY = 0.0f;
        cam.MaxY = 20.0f;

	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
