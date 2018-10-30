using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject ModeSelect;
    public GameObject Rules;
    
    public void PlayGame()
    {
        ModeSelect.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void ReadRules()
    {
        Rules.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void ReturnToMainMenuFromModeSelect()
    {
        MainMenu.SetActive(true);
        ModeSelect.SetActive(false);
    }

    public void ReturnToMainMenuFromRules()
    {
        MainMenu.SetActive(true);
        Rules.SetActive(false);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }

	// Update is called once per frame
	void Update ()
    {
		if (MainMenu.activeSelf == true && Input.GetKey("escape"))
            Application.Quit();
    }
}