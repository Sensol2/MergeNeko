using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    static public LevelManager instance;
    public int currLevel = 0;
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void UpdateLevel(int newLevel)
    {
        if (currLevel < newLevel)
        {
            currLevel = newLevel;
        }
    }

    public int GetLevel()
    {
        return this.currLevel;
    }
}
