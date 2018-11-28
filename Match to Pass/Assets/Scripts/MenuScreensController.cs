using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScreensController : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject LevelSelectPanel;
    public GameObject RulesPanel;

    public Button[] MainMenuButtons;
    public Button[] LevelSelectButtons;
    public Button RulesBackButton;

    private DataController dataController;

    private int buttonIndex;

    void Start ()
    {
        dataController = FindObjectOfType<DataController>();
        SetupLevelSelectButtons();
        buttonIndex = 0;
        MainMenuButtons[buttonIndex].Select();
    }

    public void ReadRules()
    {
        RulesBackButton.Select();
        RulesPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void SelectLevel()
    {
        buttonIndex = 0;
        LevelSelectButtons[buttonIndex].Select();
        LevelSelectPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void ReturnToMainMenuFromRules()
    {
        buttonIndex = 0;
        MainMenuButtons[buttonIndex].Select();
        MainMenuPanel.SetActive(true);
        RulesPanel.SetActive(false);
    }

    public void ReturnToMainMenuFromLevelSelect()
    {
        buttonIndex = 0;
        MainMenuButtons[buttonIndex].Select();
        MainMenuPanel.SetActive(true);
        LevelSelectPanel.SetActive(false);
    }

    public void PlayGame()
    {
        dataController.SetupLevel(0, false);
        SceneManager.LoadScene("Game");
    }

    private void SetupLevelSelectButtons()
    {
        for (int i = 0; i < LevelSelectButtons.Length; i++)
            LevelSelectButtons[i].GetComponent<LevelSelectController>().Setup(i);
    }

    public void Exit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (MainMenuPanel.activeSelf)
            {
                buttonIndex--;

                if (buttonIndex == -1)
                    buttonIndex = MainMenuButtons.Length - 1;

                MainMenuButtons[buttonIndex].Select();
            }

            else if (LevelSelectPanel.activeSelf)
                LevelSelectButtons[buttonIndex].Select();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (MainMenuPanel.activeSelf)
            {
                buttonIndex++;

                if (buttonIndex == MainMenuButtons.Length)
                    buttonIndex = 0;

                MainMenuButtons[buttonIndex].Select();
            }

            else if (LevelSelectPanel.activeSelf)
                LevelSelectButtons[buttonIndex].Select();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && LevelSelectPanel.activeSelf)
        {
            buttonIndex--;

            if (buttonIndex == -1)
                buttonIndex = LevelSelectButtons.Length - 1;

            LevelSelectButtons[buttonIndex].Select();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && LevelSelectPanel.activeSelf)
        {
            buttonIndex++;

            if (buttonIndex == LevelSelectButtons.Length)
                buttonIndex = 0;

            LevelSelectButtons[buttonIndex].Select();
        }

        if (Input.GetKeyDown("escape"))
        {
            if (MainMenuPanel.activeSelf)
                Application.Quit();
            else
            {
                buttonIndex = 0;
                MainMenuButtons[buttonIndex].Select();
                MainMenuPanel.SetActive(true);
                RulesPanel.SetActive(false);
                LevelSelectPanel.SetActive(false);
            }
        }
    }
}