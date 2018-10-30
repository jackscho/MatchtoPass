using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private readonly Color[] COLORS = { Color.black, Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.yellow };
    public GameObject resultPanel;
    public Button[] userColors;
    public Button backButton;
    public Button clearButton;
    public Button checkButton;
    public Text timeDisplay;
    public Text guessDisplay;
    public Text resultDisplay;
    public Text hintDisplay;
    private float timeInSeconds;
    private int numColorCombinations;   // Number of colors in the color combination.
    private int numGuess;               // Number of guesses the user gets.
    private int numColorsSelected;
    private bool repeatColors;          // Determines whether colors in the color combination will repeat.
    private int guess;                  // Determines if the color guess is one of the right colors but not in the right position, or if the color is is not in the color combination
    private Color[] colorCombination;   // String array with the correct colors in the color combination

    // Use this for initialization
    void Start ()
    {
        resultPanel.SetActive(false);
        backButton.interactable = false;
        clearButton.interactable = false;
        checkButton.interactable = false;
        timeInSeconds = 120f;
        numColorCombinations = 4;
        numGuess = (int)(Mathf.Log(Mathf.Pow(COLORS.Length, numColorCombinations)) / Mathf.Log(2));
        repeatColors = false;
        colorCombination = new Color[numColorCombinations];
        randomizeColorCombination();
        timeDisplay.text = "Time: " + timeInSeconds.ToString();
        guessDisplay.text = "Guesses: " + numGuess.ToString();
        numColorsSelected = 0;

        DEBUG();
    }

    private void DEBUG()
    {
        for (int i = 0; i < numColorCombinations; i++)
            Debug.Log(colorCombination[i].ToString());
    }

    // The randomizeColorCombination method randomizes the correct color combination. Colors can or can not repeat
    private void randomizeColorCombination()
    {
        // Colors in the color combination can repeat
        if (repeatColors)
        {
            // Randomly select colors from all the total possibe colors and put them in the color combination
            for (int i = 0; i < numColorCombinations; i++)
                colorCombination[i] = COLORS[Random.Range(0, COLORS.Length)];
        }

        // Otherwise, colors in the color combination will not repeat
        else
        {
            for (int i = 0; i < numColorCombinations;)
            {
                // Randomly select a color from all the total possibe colors and put it in the corresponding index in the color combination
                colorCombination[i] = COLORS[Random.Range(0 , COLORS.Length)];

                // The first index in the color combination can be skipped after putting in a color because this can be any random color
                if (i == 0)
                    i++;

                // If its not the first index, check to see if the next index has a repeated color. If so, another random color will be selected for this index
                else
                {
                    // Check if the last filled index has the same color as some other index
                    for (int j = i - 1; j >= 0; j--)
                    {
                        // If the last filled index has the same color as the previous index, don't check the next index and repeat the first for loop with the same index
                        if (colorCombination[i] == colorCombination[j])
                            break;

                        // If the last filled index doesn't have the same color as some other index, repeat the first for loop with the next index in the color combination
                        if (j == 0)
                            i++;
                    }
                }
            }
        }
    }
    
    public void Black()
    {
        if(numColorsSelected != numColorCombinations)
        {
            userColors[numColorsSelected].image.color = Color.black;
            numColorsSelected++;
        }
    }

    public void Blue()
    {
        if (numColorsSelected != numColorCombinations)
        {
            userColors[numColorsSelected].image.color = Color.blue;
            numColorsSelected++;
        }
    }

    public void Cyan()
    {
        if (numColorsSelected != numColorCombinations)
        {
            userColors[numColorsSelected].image.color = Color.cyan;
            numColorsSelected++;
        }
    }

    public void Gray()
    {
        if (numColorsSelected != numColorCombinations)
        {
            userColors[numColorsSelected].image.color = Color.gray;
            numColorsSelected++;
        }
    }

    public void Green()
    {
        if (numColorsSelected != numColorCombinations)
        {
            userColors[numColorsSelected].image.color = Color.green;
            numColorsSelected++;
        }
    }

    public void Magenta()
    {
        if (numColorsSelected != numColorCombinations)
        {
            userColors[numColorsSelected].image.color = Color.magenta;
            numColorsSelected++;
        }
    }

    public void Red()
    {
        if (numColorsSelected != numColorCombinations)
        {
            userColors[numColorsSelected].image.color = Color.red;
            numColorsSelected++;
        }
    }

    public void Yellow()
    {
        if (numColorsSelected != numColorCombinations)
        {
            userColors[numColorsSelected].image.color = Color.yellow;
            numColorsSelected++;
        }
    }

    public void Back()
    {
        numColorsSelected--;
        userColors[numColorsSelected].image.color = Color.white;
    }

    public void Clear()
    {
        for (int i = 0; i < numColorCombinations; i++)
            userColors[i].image.color = Color.white;

        numColorsSelected = 0;
    }

    public void CheckColorCombination()
    {
        hintDisplay.text = "";
        int correctGuesses;     // Keeps track of the number of correct guesses in the user's color combination
        int guess;                  // Determines if the color guess is one of the right colors but not in the right position, or if the color is is not in the color combination

        correctGuesses = 0; // Reset the number of correct guesses to zero

        // Check to see if the user's color combination guess is correct
        for (int j = 0; j < numColorCombinations; j++)
        {
            // If one color of the user's color combination guess doesn't match up with the correct color combination, don't check the rest
            if (userColors[j].image.color != colorCombination[j])
                break;
            correctGuesses++;
        }

        // If the user's color combination guess is correct, stop the game and output winner message
        if (correctGuesses == numColorCombinations)
        {
            resultDisplay.text = "You got the right combination!!\nWinner Winner Chicken Dinner!!";
            resultPanel.SetActive(true);
            return;
        }

        // If the user's color combination guess is not correct, output one of three possible outcomes for each color in the user's color combination guess
        for (int j = 0; j < numColorCombinations; j++)
        {
            // If one of the colors in the user's color combination matches the same position as the correct color combination, let the user know and immediately check the next color
            if (userColors[j].image.color == colorCombination[j])
            {
                hintDisplay.text += "Color " + (j + 1) + " is in the right position\n";
                continue;
            }

            guess = 0;  // Reset guess to zero

            // Compare one of the user's color combinaion guesses with each position in the correct color combination until a match is found
            for (int k = 0; k < numColorCombinations; k++)
            {
                // If one of the colors in the user's color combination matches some color in the correct color combination but not in the right position, let the user know
                if (userColors[j].image.color == colorCombination[k])
                {
                    hintDisplay.text += "Color " + (j + 1) + " is one of the right colors, but its not in the right position\n";
                    break;
                }
                guess++;
            }

            // If one of the colors in the user's color combination isn't any of the colors in the correct color combination, let the user know
            if (guess == numColorCombinations)
                hintDisplay.text += "Color " + (j + 1) + " is not one of the right colors\n";
        }

        numGuess--;
        guessDisplay.text = "Guesses: " + numGuess.ToString();

        // If the user's number of guesses are all used up, stop the game and output loser message
        if (numGuess == 0)
        {
            resultDisplay.text = "You lose...\nTry again next time!";
            resultPanel.SetActive(true);
        }
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKey("escape"))
            SceneManager.LoadScene("MenuScreen");

        if (timeInSeconds > 0.0f)
        {
            timeInSeconds -= Time.deltaTime;
            timeDisplay.text = "Time: " + (Mathf.Round(timeInSeconds * 10) / 10).ToString();
        }
        else
        {
            resultDisplay.text = "You lose...\nTry again next time!";
            resultPanel.SetActive(true);
        }

        if (numColorsSelected > 0)
        {
            backButton.interactable = true;
            clearButton.interactable = true;
        }
        else
        {
            backButton.interactable = false;
            clearButton.interactable = false;
        }

        if (numColorsSelected == numColorCombinations)
            checkButton.interactable = true;
        else
            checkButton.interactable = false;
        
	}
}