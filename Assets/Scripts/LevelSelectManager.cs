﻿using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour {

    public Transform[] levels;
    public float cameraMinY = 8.0f;
    public float cameraMaxY = 48.0f;

    private PersistentData m_persistentData;
    private Camera m_camera;
    private int m_currentLevel;

	// Use this for initialization
	void Start ()
    {
        DOTween.Init();

        m_persistentData = GameObject.Find("SceneEssentials").GetComponent<PersistentData>();
        m_currentLevel = m_persistentData.CurrentLevel;
        Debug.Log("Current level at scene start: " + m_currentLevel);

        SetCamera();
        ColorizeLevelNodes();
	}

    void SetCamera()
    {
        m_camera = Camera.main;
        LevelSelectCamera cam = m_camera.GetComponent<LevelSelectCamera>();

        cam.setMinMaxY(cameraMinY, cameraMaxY);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(m_persistentData.Unlock)
        {
            m_currentLevel = levels.Length - 1;
            UnlockAllLevels();
            m_persistentData.Unlock = false;
            m_persistentData.CurrentLevel = m_currentLevel;
        }

        if(m_persistentData.Reset)
        {
            m_currentLevel = 0;
            Reset();
            m_persistentData.Reset = false;
            m_persistentData.CurrentLevel = 0;
        }

	    if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.tag == "Level")
                {
                    SceneManager.LoadScene("main");
                }
            }
        }
	}

    void ColorizeLevelNodes()
    {
        DOTween.KillAll(true);
        for (int i=0; i<levels.Length-1; i++)
        {
            if(i <= m_currentLevel)
            {
                levels[i].GetChild(0).gameObject.SetActive(true);
                levels[i].GetChild(1).gameObject.SetActive(false);
                levels[i].DOScale(new Vector3(1, 1, 1), 0);    
            }
            else
            {
                levels[i].GetChild(0).gameObject.SetActive(false);
                levels[i].GetChild(1).gameObject.SetActive(true);

                Color tmp = levels[i].GetChild(1).gameObject.GetComponent<SpriteRenderer>().color;
                tmp.a = 30f;
                levels[i].GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = tmp;
                levels[i].DOScale(new Vector3(1, 1, 1), 0);
            }
        }

        Debug.Log("Current level is: " + m_currentLevel);
        levels[m_currentLevel].DOScale(new Vector3(1.5f, 1.5f, 0), 1).SetLoops(-1, LoopType.Yoyo);
    }

    public void UnlockAllLevels()
    {
        m_currentLevel = levels.Length - 1;
        SetCamera();
        ColorizeLevelNodes();
    }

    public void Reset()
    {
        m_currentLevel = 0;
        SetCamera();
        ColorizeLevelNodes();
    }

    public void Settings()
    {
        SceneManager.LoadScene("settings", LoadSceneMode.Additive);
    }
}
