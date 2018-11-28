using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{
    private int level;
    private DataController dataController;

	// Use this for initialization
	void Start ()
    {
        dataController = FindObjectOfType<DataController>();
    }

    public void Setup(int levelChoice)
    {
        level = levelChoice;
    }

    public void HandleClick()
    {
        dataController.SetupLevel(level, true);
        SceneManager.LoadScene("Game");
    }
}