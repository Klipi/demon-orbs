using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectManager : MonoBehaviour {

    public Transform[] levels;

    private PersistentData m_persistentData;
    private Camera m_camera;

	// Use this for initialization
	void Start ()
    { 
        m_persistentData = GameObject.Find("SceneEssentials").GetComponent<PersistentData>();
        int currentLevel = m_persistentData.CurrentLevel;

        SetCamera(currentLevel);
	}

    void SetCamera(int currentLevel)
    {
        m_camera = Camera.main;
        LevelSelectCamera cam = m_camera.GetComponent<LevelSelectCamera>();

        //Debug
        cam.MinY = 0.0f;
        if (currentLevel + 1 <= levels.Length-1)
        {
            cam.MaxY = levels[currentLevel + 1].transform.position.y;
        }
        else
        {
            cam.MaxY = levels[levels.Length-1].transform.position.y + 1;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void UnlockAllLevels()
    {
        SetCamera(levels.Length-1);
        //TODO: colorize all levels
    }

    public void Reset()
    {
        SetCamera(0);
        //TODO: grey out all levels except first
    }
}
