using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour {

    public Transform[] levels;

    private PersistentData m_persistentData;
    private Camera m_camera;
    private int m_currentLevel;

	// Use this for initialization
	void Start ()
    {
        DOTween.Init();

        m_persistentData = GameObject.Find("SceneEssentials").GetComponent<PersistentData>();
        m_currentLevel = m_persistentData.CurrentLevel;

        SetCamera();
        ColorizeLevelNodes();
	}

    void SetCamera()
    {
        m_camera = Camera.main;
        LevelSelectCamera cam = m_camera.GetComponent<LevelSelectCamera>();

        //Debug
        cam.MinY = 8.0f;
        if (m_currentLevel + 1 <= levels.Length-1)
        {
            cam.MaxY = levels[m_currentLevel + 1].transform.position.y;
        }
        else
        {
            cam.MaxY = levels[levels.Length-1].transform.position.y + 1;
        }

        Vector3 pos = m_camera.transform.position;
        pos.y = levels[m_currentLevel].transform.position.y+8;
        m_camera.transform.position = pos;
    }
	
	// Update is called once per frame
	void Update ()
    {
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
