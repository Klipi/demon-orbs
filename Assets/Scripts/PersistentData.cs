using UnityEngine;
using System.Collections;

public class PersistentData : MonoBehaviour {

    private static bool s_created = false;

    // Put here the stats which must be transferred between scenes
    
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
}