using UnityEngine;
using System.Collections;

public class PersistentData : MonoBehaviour {

    private static bool s_created = false;

    // Put here the stats which must be transferred between scenes
    private int m_score = 0;
    private int m_currentLevel = 0;
    private int m_lives = 0;

    void Awake()
    {
        if (!s_created)
        {
            DontDestroyOnLoad(this.gameObject);
            s_created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public int Score
    {
        get { return m_score; }
        set { m_score = value; }
    }

    public int CurrentLevel
    {
        get { return m_currentLevel; }
        set { m_currentLevel = value; }
    }

    public int Lives
    {
        get { return m_lives; }
        set { m_lives = Lives; }
    }
}