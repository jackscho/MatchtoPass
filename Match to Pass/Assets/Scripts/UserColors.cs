using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserColors : MonoBehaviour
{
    private Color color;
    private GameController gameController;

    // Use this for initialization
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void Setup(Color userColorChoice)
    {
        color = userColorChoice;
    }


    public void HandleClick()
    {
        gameController.UserColorChoiceClicked(color);
        gameController.ChangeButtonState();
    }
}