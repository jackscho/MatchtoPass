#include <iostream>
#include <string>
#include <cstdlib>
#include <ctime>
using namespace std;

const int NUM_DEFAULT_TOTAL_COLORS = 13;
const string DEFAULT_COLOR_OPTIONS[NUM_DEFAULT_TOTAL_COLORS] = { "Black", "Blue", "Brown", "Cyan", "Green", "Grey", "Magenta", "Orange", "Pink", "Purple", "Red", "White", "Yellow" };
const int numColorCombinationS_EASY_DIFFICULTY = 3;
const int numGuesses_EASY_DIFFICULTY = 10;
const int numColorCombinationS_NORMAL_DIFFICULTY = 7;
const int numGuesses_NORMAL_DIFFICULTY = 20;
const int numColorCombinationS_HARD_DIFFICULTY = 10;
const int numGuesses_HARD_DIFFICULTY = 30;
const int numColorCombinationS_EXPERT_DIFFICULTY = 13;
const int numGuesses_EXPERT_DIFFICULTY = 40;
const int NUM_WAIT_SECONDS = 4;

void chooseDifficulty(int&, int&, int&, string&, string*&);
void playGame(int, int, int, string*);
void displayRules();

int main()
{
	int totalColors;
	int numColorCombinations;
	int numGuess;
	string optionMainMenu, optionDifficulty;
	string *customUserColors;
	clock_t startClockExitProgram;

	cout << "Color Match Game" << endl << endl;

	do
	{
		cout << "1. Play the Game!" << endl;
		cout << "2. Read the Rules!" << endl;
		cout << "Press anything else to exit the game" << endl;

		getline(cin, optionMainMenu);

		cout << endl;

		if (optionMainMenu == "1")
		{
			customUserColors = nullptr;
			chooseDifficulty(totalColors, numColorCombinations, numGuess, optionDifficulty, customUserColors);
			if (optionDifficulty == "1" || optionDifficulty == "2" || optionDifficulty == "3" || optionDifficulty == "4" || optionDifficulty == "5")
				playGame(totalColors, numColorCombinations, numGuess, customUserColors);

			delete[] customUserColors;
		}

		else if (optionMainMenu == "2")
			displayRules();

	} while (optionMainMenu == "1" || optionMainMenu == "2");

	cout << "Thank you for playing!" << endl << endl;
	cout << "Exiting...";

	startClockExitProgram = clock();
	while ((clock() - startClockExitProgram) / CLOCKS_PER_SEC < NUM_WAIT_SECONDS) {}

	return 0;
}

void chooseDifficulty(int& totalColors, int& numColorCombinations, int& numGuesses, string& option, string *&customUserColors)
{
	string tempInput;

	cout << "Choose your difficulty level:" << endl << endl;
	cout << "1. Easy" << endl;
	cout << "2. Normal" << endl;
	cout << "3. Hard" << endl;
	cout << "4. Expert" << endl;
	cout << "5. Custom" << endl;
	cout << "Press anything else to return to the main menu" << endl;

	getline(cin, option);

	cout << endl;

	if (option == "1")
	{
		numColorCombinations = numColorCombinationS_EASY_DIFFICULTY;
		numGuesses = numGuesses_EASY_DIFFICULTY;
	}

	else if (option == "2")
	{
		numColorCombinations = numColorCombinationS_NORMAL_DIFFICULTY;
		numGuesses = numGuesses_NORMAL_DIFFICULTY;
	}

	else if (option == "3")
	{
		numColorCombinations = numColorCombinationS_HARD_DIFFICULTY;
		numGuesses = numGuesses_HARD_DIFFICULTY;
	}

	else if (option == "4")
	{
		numColorCombinations = numColorCombinationS_EXPERT_DIFFICULTY;
		numGuesses = numGuesses_EXPERT_DIFFICULTY;
	}

	else if (option == "5")
	{
		cout << "Enter the total number of colors: ";
		getline(cin, tempInput);
		while (atoi(tempInput.c_str()) <= 0)
		{
			cout << "Input must be a positive integer: ";
			getline(cin, tempInput);
		}
		totalColors = atoi(tempInput.c_str());

		customUserColors = new string[totalColors];

		cout << "Enter the number of color combinations you want to pick: ";
		getline(cin, tempInput);
		while (atoi(tempInput.c_str()) <= 0 || atoi(tempInput.c_str()) > totalColors)
		{
			cout << "Input must be a positive integer and less than or equal to the total number of colors: ";
			getline(cin, tempInput);
		}
		numColorCombinations = atoi(tempInput.c_str());

		cout << "Enter the number of guesses you can use: ";
		getline(cin, tempInput);
		while (atoi(tempInput.c_str()) <= 0)
		{
			cout << "Input must be a positive integer: ";
			getline(cin, tempInput);
		}
		numGuesses = atoi(tempInput.c_str());

		cout << "Enter the " << totalColors << " colors in succession: ";
		for (int i = 0; i < totalColors; i++)
			cin >> customUserColors[i];

		getline(cin, tempInput);

		cout << endl;
	}
}
void playGame(int totalColors, int numColorCombination, int numGuesses, string* customUserColors)
{
	int correctGuesses;
	int guess;
	string buffer;
	string *colorCombination = new string[numColorCombination];
	string *userColorGuess = new string[numColorCombination];
	clock_t startClockExitGame;

	cout << "All the possible colors are ";

	srand(time(0));

	if (!customUserColors)
	{
		for (int i = 0; i < numColorCombination; i++)
			colorCombination[i] = DEFAULT_COLOR_OPTIONS[rand() % NUM_DEFAULT_TOTAL_COLORS];

		for (int i = 0; i < NUM_DEFAULT_TOTAL_COLORS; i++)
			cout << DEFAULT_COLOR_OPTIONS[i] << ", ";
	}

	else
	{
		for (int i = 0; i < numColorCombination; i++)
			colorCombination[i] = customUserColors[rand() % totalColors];

		for (int i = 0; i < totalColors; i++)
			cout << customUserColors[i] << ", ";
	}

	cout << "\b\b\b" << endl;

	cout << "In this difficulty, there are " << numColorCombination << " colors in the ordered combination and you are given " << numGuesses << " guesses." << endl;

	/* TESTING PURPOSES ONLY. PRINTS THE CORRECT COLOR COMBINATION
	
	for (int i = 0; i < numColorCombination; i++)
		cout << colorCombination[i] << " ";
	cout << endl;
	*/

	for (int i = 0; i < numGuesses; i++)
	{
		cout << endl << "Enter your guess: ";

		for (int j = 0; j < numColorCombination; j++)
			cin >> userColorGuess[j];

		getline(cin, buffer);

		correctGuesses = 0;
		for (int j = 0; j < numColorCombination; j++)
		{
			if (userColorGuess[j] != colorCombination[j])
				break;
			correctGuesses++;
		}

		if (correctGuesses == numColorCombination)
			break;

		cout << endl;

		for (int j = 0; j < numColorCombination; j++)
		{
			if (userColorGuess[j] == colorCombination[j])
			{
				cout << "Color " << j + 1 << " is in the right position" << endl;
				continue;
			}

			guess = 0;
			for (int k = 0; k < numColorCombination; k++)
			{
				if (userColorGuess[j] == colorCombination[k])
				{
					cout << "Color " << j + 1 << " is one of the right colors, but its not in the right position" << endl;
					break;
				}
				guess++;
			}

			if (guess == numColorCombination)
				cout << "Color " << j + 1 << " is not one of the right colors" << endl;
		}
	}

	cout << endl;

	if (correctGuesses == numColorCombination)
		cout << "You got the right combination!!" << endl << "Winner Winner Chicken Dinner!!" << endl;

	else
		cout << "You lose..." << endl << "Try again next time!" << endl;

	delete[] colorCombination;
	delete[] userColorGuess;

	cout << endl << "Returning to Main Menu..." << endl << endl;

	startClockExitGame = clock();
	while ((clock() - startClockExitGame) / CLOCKS_PER_SEC < NUM_WAIT_SECONDS) {}
}

void displayRules()
{
	cout << "---------------------------------- Rules ----------------------------------" << endl << endl;
	cout << "1. Enter the color combination in the right order and you win!" << endl;
	cout << "2. You will be given a list of all possible color choices when you select a difficulty" << endl;
	cout << "3. Type the colors in succession, seperating each color with one whitespace. Ex: Blue Green Yellow Red." << endl;
	cout << "4. You will be given a number of guesses you can use. If you use up all your guesses before finding the correct color combination, you lose!" << endl << endl;
}