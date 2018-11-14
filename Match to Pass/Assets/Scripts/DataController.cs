using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
    public LevelData[] allLevelData;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("MenuScreens");
	}

    public LevelData GetCurrentLevelData(int level)
    {
        return allLevelData[level];
    }

    public int GetNumberOfLevels()
    {
        return allLevelData.Length;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}