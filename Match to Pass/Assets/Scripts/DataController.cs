using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
    public LevelData[] allLevelData;
    private int levelChoice;
    private bool onlyPlayOneLevel;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("TitleScreen");
	}

    public LevelData GetCurrentLevelData(int level)
    {
        return allLevelData[level];
    }

    public int GetNumberOfLevels()
    {
        return allLevelData.Length;
    }

    public int GetLevel()
    {
        return levelChoice;
    }

    public bool GetPlayOneLevelChoice()
    {
        return onlyPlayOneLevel;
    }

    public void SetupLevel(int level, bool playOneLevel)
    {
        levelChoice = level;
        onlyPlayOneLevel = playOneLevel;
    }
}