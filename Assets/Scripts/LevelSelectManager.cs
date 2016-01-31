using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour {

    public Transform[] levels;
    public float cameraMinY = 8.0f;
    public float cameraMaxY = 48.0f;

    private PersistentData m_persistentData;
    private Camera m_camera;

    private int m_maxLevel
	{
		get {
			return m_persistentData.MaxLevel;
		}
		set {
			m_persistentData.MaxLevel = value;
		}
	}

	// Use this for initialization
	void Start ()
    {
        DOTween.Init();

        m_persistentData = GameObject.Find("SceneEssentials").GetComponent<PersistentData>();
        m_persistentData.Save();
        m_maxLevel = m_persistentData.MaxLevel;
        Debug.Log("Max level at scene start: " + m_maxLevel);

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
            m_maxLevel = levels.Length - 1;
            UnlockAllLevels();
            m_persistentData.Unlock = false;
            m_persistentData.CurrentLevel = m_maxLevel;
        }

        if(m_persistentData.Reset)
        {
            m_maxLevel = 0;
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
                    int level = hit.collider.gameObject.GetComponent<LevelNode>().Number;
                    Debug.Log("Hit level " + level + ", max level " + m_persistentData.MaxLevel);
                    if (m_persistentData.MaxLevel >= level)
                    {
                        m_persistentData.CurrentLevel = level;

                        float waitTime = 2.0f;
                        StartCoroutine(ExitScene(waitTime));
                        
                    }
                }
                else
                {
                    Debug.Log("no hit!");
                }
            }
        }
	}

    IEnumerator ExitScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); 
        SceneManager.LoadScene("main");
    }

    void ColorizeLevelNodes()
    {
        DOTween.KillAll(true);
        for (int i=0; i < levels.Length; i++)
        {
            if(i <= m_maxLevel)
            {
                levels[i].GetChild(0).gameObject.SetActive(true);
                levels[i].GetChild(1).gameObject.SetActive(false);
                levels[i].GetChild(2).gameObject.SetActive(false);
                levels[i].DOScale(new Vector3(1, 1, 1), 0);    
            }
            else
            {
                levels[i].GetChild(0).gameObject.SetActive(false);
                levels[i].GetChild(1).gameObject.SetActive(true);
                levels[i].GetChild(2).gameObject.SetActive(false);

                Color tmp = levels[i].GetChild(1).gameObject.GetComponent<SpriteRenderer>().color;
                tmp.a = 30f;
                levels[i].GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = tmp;
                levels[i].DOScale(new Vector3(1, 1, 1), 0);
            }
        }

        Debug.Log("Current max level is: " + m_maxLevel);
        levels[m_maxLevel].DOScale(new Vector3(1.5f, 1.5f, 0), 1).SetLoops(-1, LoopType.Yoyo);
    }

    public void UnlockAllLevels()
    {
        m_maxLevel = levels.Length - 1;
        SetCamera();
        ColorizeLevelNodes();
    }

    public void Reset()
    {
        m_maxLevel = 0;
        SetCamera();
        ColorizeLevelNodes();
    }

    public void Settings()
    {
        SceneManager.LoadScene("settings", LoadSceneMode.Additive);
    }
}
