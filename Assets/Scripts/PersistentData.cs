using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistentData : MonoBehaviour {

    private static bool s_created = false;

    // Game Definitions
    public static int LEVEL_AMOUNT = 7;

    // Put here the stats which must be transferred between scenes
    private int[] m_levelScores = new int[LEVEL_AMOUNT];

    void Awake()
    {
        if (!s_created)
        {
            Debug.Log("Persistent Data created!");
            DontDestroyOnLoad(this.gameObject);
            Load();
            s_created = true;    
        }
        else
        {
            Debug.Log("Persistent Data already initialized...");
            Destroy(this.gameObject);
        }
    }

    void OnApplicationQuit()
    {
        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Lives", this.Lives);
        PlayerPrefs.SetInt("Score", this.Score);
        PlayerPrefs.SetInt("Level", this.CurrentLevel);
        PlayerPrefsX.SetIntArray("LevelScores", m_levelScores);
   
    }

    public void Load()
    {
        this.Lives = PlayerPrefs.GetInt("Lives", 3);
        this.Score = PlayerPrefs.GetInt("Score", 0);
        this.CurrentLevel = PlayerPrefs.GetInt("Level", 0);
        m_levelScores = PlayerPrefsX.GetIntArray("LevelScores");
    }

    public int Score
    {
        get;
        set; 
    }

    public int CurrentLevel
    {
        get;
        set;
    }

    public int Lives
    {
        get;
        set; 
    }

    public bool Reset
    {
        get;
        set;
    }

    public bool Unlock
    {
        get;
        set;
    }

    public void SetScore(int level, int score)
    {
        if(level < LEVEL_AMOUNT)
        {
            m_levelScores[level] = score;
        }
    }

    public int GetScore(int level)
    {
        if(level < LEVEL_AMOUNT)
        {
            return m_levelScores[level];
        }
        return 0;
    }
}