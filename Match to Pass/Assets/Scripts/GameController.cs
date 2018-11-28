using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject resultsPanel;
    public GameObject pausePanel;

    public Text levelNameText;
    public Text timeDisplayText;
    public Text guessDisplayText;

    public Image BombStrapsImage;
    public RawImage HeadRawImage;
    public Image repeatImage;
    public RawImage resultRawImage;
    public RawImage finalResultRawImage;

    public Transform colorSlotButtonsParent;
    public Transform colorHintImagesParent;
    public Transform colorOptionsButtonsParent;

    public SimpleObjectPool colorSlotObjectPool;
    public SimpleObjectPool colorHintsObjectPool;
    public SimpleObjectPool colorOptionsObjectPool;

    public Button backButton;
    public Button clearButton;
    public Button checkButton;
    public Button returnToMainMenuButton;

    private DataController dataController;
    private LevelData currentLevelData;

    private List<GameObject> colorSlotGameObjects = new List<GameObject>();
    private List<GameObject> colorHintsGameObjects = new List<GameObject>();
    private List<GameObject> colorOptionsGameObjects = new List<GameObject>();

    private List<Color> colorCombination = new List<Color>();

    private Texture2D texture2D;

    private float timeInSecondsFromLevel;
    private bool isTimeActive;
    private int timeInMinutes;
    private int timeInSeconds;
    private int timeInMiliseconds;
    private int numColorsSelected;
    private int numGuesses;
    private int currentColorRow;
    private int currentLevel;

    // Use this for initialization
    void Start ()
    {
        dataController = FindObjectOfType<DataController>();
        currentLevel = dataController.GetLevel() - 1;
        pausePanel.SetActive(false);
        resultsPanel.SetActive(false);
        isTimeActive = true;
        ShowNewLevel();
    }

    private void ShowNewCombination()
    {
        currentColorRow++;
        numGuesses = currentLevelData.colorSlotRows[currentColorRow].numberOfGuesses;
        guessDisplayText.text = numGuesses.ToString();

        backButton.interactable = false;
        clearButton.interactable = false;
        checkButton.interactable = false;

        RemoveColorSlotsandColorHints();

        ShowColorSlotsAndHints();

        DetermineIfColorsCanRepeat();

        RandomizeColorCombination();

        numColorsSelected = 0;
    }

    private void ShowNewLevel()
    {
        currentColorRow = -1;
        currentLevel++;
        currentLevelData = dataController.GetCurrentLevelData(currentLevel);
        levelNameText.text = currentLevelData.levelName;
        timeInSecondsFromLevel = currentLevelData.timeLimitInSeconds;

        texture2D = Resources.Load(currentLevelData.headSpriteLocation) as Texture2D;
        HeadRawImage.texture = texture2D;

        BombStrapsImage.color = currentLevelData.bombColor;

        RemoveColorOptions();

        ShowNewCombination();

        ShowColorOptions();
    }

    private void DetermineIfColorsCanRepeat()
    {
        if (currentLevelData.colorSlotRows[currentColorRow].canColorsRepeatInRow)
            repeatImage.color = Color.green;
        else
            repeatImage.color = Color.red;
    }

    private void ShowColorSlotsAndHints()
    {
        for (int i = 0; i < currentLevelData.colorSlotRows[currentColorRow].numberOfColorSlotColumns; i++)
        {
            GameObject colorSlotGameObject = colorSlotObjectPool.GetObject();
            colorSlotGameObject.GetComponent<Button>().image.color = Color.white;
            colorSlotGameObjects.Add(colorSlotGameObject);
            colorSlotGameObject.transform.SetParent(colorSlotButtonsParent.transform, false);

            GameObject colorHintsGameObject = colorHintsObjectPool.GetObject();
            colorHintsGameObject.GetComponent<Image>().color = Color.white;
            colorHintsGameObjects.Add(colorHintsGameObject);
            colorHintsGameObject.transform.SetParent(colorHintImagesParent.transform, false);
        }
    }

    private void ShowColorOptions()
    {
        for (int i = 0; i < currentLevelData.userColorOptions.Length; i++)
        {
            GameObject colorOptionsGameObject = colorOptionsObjectPool.GetObject();
            colorOptionsGameObject.GetComponent<Button>().image.color = currentLevelData.userColorOptions[i];
            colorOptionsGameObjects.Add(colorOptionsGameObject);
            colorOptionsGameObject.transform.SetParent(colorOptionsButtonsParent.transform, false);

            UserColors userColor = colorOptionsGameObject.GetComponent<UserColors>();
            userColor.Setup(currentLevelData.userColorOptions[i]);
        }
    }

    private void RemoveColorSlotsandColorHints()
    {
        while (colorSlotGameObjects.Count > 0)
        {
            colorSlotObjectPool.ReturnObject(colorSlotGameObjects[0]);
            colorSlotGameObjects.RemoveAt(0);
        }

        while (colorHintsGameObjects.Count > 0)
        {
            colorHintsObjectPool.ReturnObject(colorHintsGameObjects[0]);
            colorHintsGameObjects.RemoveAt(0);
        }
    }

    private void RemoveColorOptions()
    {
        while (colorOptionsGameObjects.Count > 0)
        {
            colorOptionsObjectPool.ReturnObject(colorOptionsGameObjects[0]);
            colorOptionsGameObjects.RemoveAt(0);
        }
    }

    public void UserColorChoiceClicked(Color color)
    {
        if (numColorsSelected != currentLevelData.colorSlotRows[currentColorRow].numberOfColorSlotColumns)
        {
            colorSlotGameObjects[numColorsSelected].GetComponent<Button>().image.color = color;
            numColorsSelected++;
        }
    }

    public void Undo()
    {
        checkButton.interactable = false;

        numColorsSelected--;
        colorSlotGameObjects[numColorsSelected].GetComponent<Button>().image.color = Color.white;
        colorHintsGameObjects[numColorsSelected].GetComponent<Image>().color = Color.white;

        if (numColorsSelected <= 0)
        {
            backButton.interactable = false;
            clearButton.interactable = false;
        }
    }


    public void Clear()
    {
        backButton.interactable = false;
        clearButton.interactable = false;
        checkButton.interactable = false;

        for (int i = 0; i < currentLevelData.colorSlotRows[currentColorRow].numberOfColorSlotColumns; i++)
        {
            colorSlotGameObjects[i].GetComponent<Button>().image.color = Color.white;
            colorHintsGameObjects[i].GetComponent<Image>().color = Color.white;
        }

        numColorsSelected = 0;
    }

    public void ChangeButtonState()
    {
        if (numColorsSelected > 0)
        {
            backButton.interactable = true;
            clearButton.interactable = true;
        }

        if (numColorsSelected == currentLevelData.colorSlotRows[currentColorRow].numberOfColorSlotColumns)
            checkButton.interactable = true;
    }

    private void RandomizeColorCombination()
    {
        colorCombination.Clear();

        if (!currentLevelData.colorSlotRows[currentColorRow].canColorsRepeatInRow && currentLevelData.colorSlotRows[currentColorRow].numberOfColorSlotColumns <= currentLevelData.userColorOptions.Length)
        {
            for (int i = 0; i < currentLevelData.userColorOptions.Length; i++)
                colorCombination.Add(Color.clear);

            for (int i = 0, j; i < colorCombination.Count; i++)
            {
                j = Random.Range(0, i + 1);

                if (j != i)
                    colorCombination[i] = colorCombination[j];

                colorCombination[j] = currentLevelData.userColorOptions[i];
            }

            colorCombination.RemoveRange(currentLevelData.colorSlotRows[currentColorRow].numberOfColorSlotColumns - 1, colorCombination.Count - currentLevelData.colorSlotRows[currentColorRow].numberOfColorSlotColumns);
        }

        else
        {
            for (int i = 0; i < currentLevelData.colorSlotRows[currentColorRow].numberOfColorSlotColumns; i++)
                colorCombination.Add(currentLevelData.userColorOptions[Random.Range(0, currentLevelData.userColorOptions.Length)]);
        }

        for (int i = 0; i < colorCombination.Count; i++)
            Debug.Log(colorCombination[i].ToString());
    }

    public void CheckColorCombination()
    {
        int correctGuesses;     // Keeps track of the number of correct guesses in the user's color combination
        int guess;              // Determines if the color guess is one of the right colors but not in the right position, or if the color is is not in the color combination

        correctGuesses = 0; // Reset the number of correct guesses to zero

        checkButton.interactable = false;

        // Check to see if the user's color combination guess is correct
        for (int i = 0; i < currentLevelData.colorSlotRows[currentColorRow].numberOfColorSlotColumns; i++)
        {
            // If one color of the user's color combination guess doesn't match up with the correct color combination, don't check the rest
            if (colorSlotGameObjects[i].GetComponent<Button>().image.color != colorCombination[i])
                break;
            correctGuesses++;
        }

        // If the user's color combination guess is correct, stop the game and output winner message
        if (correctGuesses == currentLevelData.colorSlotRows[currentColorRow].numberOfColorSlotColumns)
        {
            texture2D = Resources.Load("Green Check") as Texture2D;
            resultRawImage.texture = texture2D;

            if (currentColorRow == currentLevelData.colorSlotRows.Length - 1)
            {
                if (dataController.GetPlayOneLevelChoice() || currentLevel == dataController.GetNumberOfLevels() - 1)
                {
                    isTimeActive = false;
                    texture2D = Resources.Load("Winning Image") as Texture2D;
                    finalResultRawImage.texture = texture2D;
                    resultsPanel.SetActive(true);
                }

                else
                    ShowNewLevel();
            }
            
            else
                ShowNewCombination();

            return;
        }

        texture2D = Resources.Load("Red X") as Texture2D;
        resultRawImage.texture = texture2D;

        // If the user's color combination guess is not correct, output one of three possible outcomes for each color in the user's color combination guess
        for (int i = 0; i < currentLevelData.colorSlotRows[currentColorRow].numberOfColorSlotColumns; i++)
        {
            // If one of the colors in the user's color combination matches the same position as the correct color combination, let the user know and immediately check the next color
            if (colorSlotGameObjects[i].GetComponent<Button>().image.color == colorCombination[i])
            {
                colorHintsGameObjects[i].GetComponent<Image>().color = Color.green;
                continue;
            }

            guess = 0;  // Reset guess to zero

            // Compare one of the user's color combinaion guesses with each position in the correct color combination until a match is found
            for (int j = 0; j < currentLevelData.colorSlotRows[currentColorRow].numberOfColorSlotColumns; j++)
            {
                // If one of the colors in the user's color combination matches some color in the correct color combination but not in the right position, let the user know
                if (colorSlotGameObjects[i].GetComponent<Button>().image.color == colorCombination[j])
                {
                    colorHintsGameObjects[i].GetComponent<Image>().color = Color.yellow;
                    break;
                }
                guess++;
            }

            // If one of the colors in the user's color combination isn't any of the colors in the correct color combination, let the user know
            if (guess == currentLevelData.colorSlotRows[currentColorRow].numberOfColorSlotColumns)
                colorHintsGameObjects[i].GetComponent<Image>().color = Color.red;
        }

        numGuesses--;
        guessDisplayText.text = numGuesses.ToString();

        // If the user's number of guesses are all used up, stop the game and output loser message
        if (numGuesses == 0)
        {
            isTimeActive = false;
            texture2D = Resources.Load("Losing Image") as Texture2D;
            finalResultRawImage.texture = texture2D;
            resultsPanel.SetActive(true);
        }
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene("MenuScreens");
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown("escape"))
            SceneManager.LoadScene("MenuScreens");

        if (Input.GetKeyDown(KeyCode.P) && !resultsPanel.activeSelf && timeInSecondsFromLevel > 0.0f)
        {
            isTimeActive = !isTimeActive;
            pausePanel.SetActive(!pausePanel.activeSelf);
        }

        if (isTimeActive)
        {
            timeInSecondsFromLevel -= Time.deltaTime;
            timeInMinutes = (int) timeInSecondsFromLevel / 60;
            timeInSeconds = (int) timeInSecondsFromLevel % 60;
            timeInMiliseconds = (int) (timeInSecondsFromLevel * 100) % 100;
            
            timeDisplayText.text = timeInMinutes.ToString("00") + ":" + timeInSeconds.ToString("00") + ":" + timeInMiliseconds.ToString("00");

            if (timeInSecondsFromLevel < 0.0f)
            {
                isTimeActive = false;
                texture2D = Resources.Load("Losing Image") as Texture2D;
                finalResultRawImage.texture = texture2D;
                resultsPanel.SetActive(true);
            }
        }
    }
}