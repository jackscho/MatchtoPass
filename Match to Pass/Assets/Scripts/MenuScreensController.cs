using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScreensController : MonoBehaviour
{
    public GameObject TitleScreenPanel;
    public GameObject MainMenuPanel;
    public GameObject RulesPanel;

    public Button[] MainMenuButtons;
    public Button[] RulesButtons;

    private int buttonIndex;

    public void ReadRules()
    {
        buttonIndex = 0;
        RulesButtons[buttonIndex].Select();
        RulesPanel.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        buttonIndex = 0;
        MainMenuButtons[buttonIndex].Select();
        RulesPanel.SetActive(false);
    }

    public void PlayGame()
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
        if (TitleScreenPanel.activeSelf && Input.anyKey)
        {
            buttonIndex = 0;
            MainMenuPanel.SetActive(true);
            TitleScreenPanel.SetActive(false);
            MainMenuButtons[buttonIndex].Select();
        }

        if (MainMenuPanel.activeSelf && Input.GetKeyDown(KeyCode.UpArrow))
        {
            buttonIndex--;

            if (buttonIndex == -1)
                buttonIndex = MainMenuButtons.Length - 1;

            MainMenuButtons[buttonIndex].Select();
        }

        if (MainMenuPanel.activeSelf && Input.GetKeyDown(KeyCode.DownArrow))
        {
            buttonIndex++;

            if (buttonIndex == MainMenuButtons.Length)
                buttonIndex = 0;

            MainMenuButtons[buttonIndex].Select();
        }

        if (RulesPanel.activeSelf && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            buttonIndex--;

            if (buttonIndex == -1)
                buttonIndex = RulesButtons.Length - 1;

            RulesButtons[buttonIndex].Select();
        }

        if (RulesPanel.activeSelf && Input.GetKeyDown(KeyCode.RightArrow))
        {
            buttonIndex++;

            if (buttonIndex == RulesButtons.Length)
                buttonIndex = 0;

            RulesButtons[buttonIndex].Select();
        }

        if (!TitleScreenPanel.activeSelf && Input.GetKeyDown("escape"))
            Application.Quit();
    }
}