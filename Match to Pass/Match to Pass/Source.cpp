#include <iostream>
#include <string>
#include <cstdlib>
#include <ctime>
using namespace std;

const int NUM_TOTAL_COLORS = 5;
const int NUM_COLOR_COMBINATION = 3;
const int NUM_GUESSES = 10;
const int NUM_WAIT_SECONDS = 4;

int main()
{
	int correctGuesses;
	int guess;
	string option;
	string buffer;
	string colorOptions[NUM_TOTAL_COLORS] = { "Red", "Green", "Blue", "Yellow", "Purple" };
	string colorCombination[NUM_COLOR_COMBINATION];
	string userColorGuess[NUM_COLOR_COMBINATION];
	clock_t startClock;

	cout << "Match to Pass" << endl << endl;

	do
	{
		cout << "1. Play the Game!" << endl;
		cout << "2. Read the Rules!" << endl;
		cout << "Press anything else to exit the game" << endl;

		getline(cin, option);

		if (option == "1")
		{
			srand(time(0));
			for (int i = 0; i < NUM_COLOR_COMBINATION; i++)
				colorCombination[i] = colorOptions[rand() % NUM_TOTAL_COLORS];

			/* TESTING PURPOSES ONLY. PRINTS THE CORRECT COLOR COMBINATION
			
			for (int i = 0; i < NUM_COLOR_COMBINATION; i++)
				cout << colorCombination[i] << " ";
			cout << endl;
			*/

			for (int i = 0; i < NUM_GUESSES; i++)
			{
				cout << endl << "Enter your guess: ";

				for (int j = 0; j < NUM_COLOR_COMBINATION; j++)
					cin >> userColorGuess[j];

				getline(cin, buffer);

				correctGuesses = 0;
				for (int j = 0; j < NUM_COLOR_COMBINATION; j++)
				{
					if (userColorGuess[j] != colorCombination[j])
						break;
					correctGuesses++;
				}

				if (correctGuesses == NUM_COLOR_COMBINATION)
					break;

				cout << endl;

				for (int j = 0; j < NUM_COLOR_COMBINATION; j++)
				{
					if (userColorGuess[j] == colorCombination[j])
					{
						cout << "Color " << j << " is one of the right colors and in the right position" << endl;
						continue;
					}

					guess = 0;
					for (int k = 0; k < NUM_COLOR_COMBINATION; k++)
					{
						if (userColorGuess[j] == colorCombination[k])
						{
							cout << "Color " << j << " is one of the right colors, but its not in the right position" << endl;
							break;
						}
						guess++;
					}

					if (guess == NUM_COLOR_COMBINATION)
						cout << "Color " << j << " is not one of the right colors" << endl;
				}
			}

			cout << endl;

			if (correctGuesses == NUM_COLOR_COMBINATION)
				cout << "You got the right combination!!" << endl << "Winner Winner Chicken Dinner!!" << endl;

			else
				cout << "You lose..." << endl << "Try again next time!" << endl;

			cout << endl << "Returning to Main Menu..." << endl << endl;

			startClock = clock();
			while ((clock() - startClock) / CLOCKS_PER_SEC < NUM_WAIT_SECONDS) {}
		}

		else if (option == "2")
		{
			cout << endl << "---------------------------------- Rules ----------------------------------" << endl << endl;
			cout << "1. Enter the " << NUM_COLOR_COMBINATION << " color combination that you think is correct and you win!" << endl;
			cout << "2. The only colors you can guess are ";
			for (int i = 0; i < NUM_TOTAL_COLORS; i++)
				cout << colorOptions[i] << ", ";
			cout << "\b\b." << endl;
			cout << "3. Type the colors in succession. Ex: Blue Green Yellow." << endl;
			cout << "4. You get " << NUM_GUESSES << " guesses. After the " << NUM_GUESSES << "th incorrect guess, you lose!" << endl << endl;
		}

	} while (option == "1" || option == "2");

	cout << endl << "Thank you for playing!" << endl << endl;
	cout << "Exiting...";

	startClock = clock();
	while ((clock() - startClock) / CLOCKS_PER_SEC < NUM_WAIT_SECONDS) {}

	return 0;
}