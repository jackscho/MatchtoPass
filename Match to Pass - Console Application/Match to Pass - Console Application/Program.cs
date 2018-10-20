using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_to_Pass___Console_Application
{
    class Program
    {
        // All the possible colors that the user can choose from. Array must be static so that any function can access it. Array is read only so that it acts as a constant.
        static readonly string[] COLOR_OPTIONS = { "black", "blue", "brown", "cyan", "green", "grey", "magenta", "orange", "pink", "purple", "red", "white", "yellow" };
        const int NUM_COLOR_COMBINATIONS_EASY_DIFFICULTY = 4;   // Number of colors in the combination in easy difficulty
        const int NUM_COLOR_COMBINATION_NORMAL_DIFFICULTY = 8;  // Number of colors in the combination in normal difficulty
        const int NUM_COLOR_COMBINATION_HARD_DIFFICULTY = 12;   // Number of colors in the combination in hard difficulty
        const int NUM_COLOR_COMBINATION_EXPERT_DIFFICULTY = 16; // Number of colors in the combination in expert difficulty
        const bool DEBUG = false;                               // Debug mode

        static void Main(string[] args)
        {
            int numColorCombinations = 0;   // Number of colors in the color combination.
            int numGuess = 0;               // Number of guesses the user gets.
            bool repeatColors = true;       // Determines whether colors in the color combination will repeat.
            char optionMainMenu;            // Main Menu choice. Determines if the user will play the game, read the rules, or exit the program
            char optionDifficulty = ' ';    // Difficulty choice. Determines if the difficulty will be easy, normal, hard, expert, or custom, or return to main menu

            do
            {
                // Main Menu
                Console.Out.WriteLine("Match to Pass\n");
                Console.Out.WriteLine("1. Play the Game!");
                Console.Out.WriteLine("2. Read the Rules!");
                Console.Out.WriteLine("Enter anything else to exit the game");

                optionMainMenu = Console.ReadKey().KeyChar; // Read the user's character input

                Console.Out.WriteLine(Environment.NewLine); // Format menu with a new line

                // If the user inputs a '1', the user will be able to select a difficulty. If the user selects a valid difficulty, the game will start. Otherwise, the user will be returned to the main menu
                if (optionMainMenu == '1')
                {
                    chooseDifficulty(ref numColorCombinations, ref numGuess, ref repeatColors, ref optionDifficulty);
                    if (optionDifficulty == '1' || optionDifficulty == '2' || optionDifficulty == '3' || optionDifficulty == '4' || optionDifficulty == '5')    // Only easy, normal, hard, expert, or custom can be selected
                        playGame(numColorCombinations, numGuess, repeatColors);
                }

                // If the user inputs '2', the rules will be displayed on the screen
                else if (optionMainMenu == '2')
                    displayRules();

            } while (optionMainMenu == '1' || optionMainMenu == '2');   // If the user doesn't input a '1' or '2', the program will terminate

            Console.Out.WriteLine("Thank you for playing!\n");  // Exit message
        }

        // The printArray function outputs all the elements in an array. Some formatting is also applied
        private static string printArray(string[] array)
        {
            string temp = "";    // String with each element in the array

            for (int i = 0; i < array.Length; i++)
                temp += array[i] + ", ";
            temp += "\b\b "; // Formatting. Gets rid of the extra ','

            return temp;
        }

        // The chooseDifficulty method will determine the difficulty. This will determine the number of colors in the combination, the number of guesses the user gets, and whether the colors in the combination can repeat
        private static void chooseDifficulty(ref int numColorCombinations, ref int numGuesses, ref bool repeatColors, ref char option)
        {
            char customRepeatColorsChoice;  // If the user chooses a custom difficulty, this determines whether the user wants colors in the color combinaiton to repeat or not.

            // Difficulty Menu
            Console.Out.WriteLine("Choose your difficulty level:");
            Console.Out.WriteLine("1. Easy");
            Console.Out.WriteLine("2. Normal");
            Console.Out.WriteLine("3. Hard");
            Console.Out.WriteLine("4. Expert");
            Console.Out.WriteLine("5. Custom");
            Console.Out.WriteLine("Enter anything else to return to the main menu");

            option = Console.ReadKey().KeyChar; // Read the user's character input

            Console.Out.WriteLine(Environment.NewLine); // Format menu with a new line

            // If the user inputs a '1', the easy difficulty will be used. Colors will not repeat in this difficulty
            if (option == '1')
            {
                numColorCombinations = NUM_COLOR_COMBINATIONS_EASY_DIFFICULTY;
                repeatColors = false;
            }

            // If the user inputs a '2', the normal difficulty will be used. Colors will not repeat in this difficulty
            else if (option == '2')
            {
                numColorCombinations = NUM_COLOR_COMBINATION_NORMAL_DIFFICULTY;
                repeatColors = false;
            }

            // If the user inputs a '3', the hard difficulty will be used. Colors can repeat in this difficulty
            else if (option == '3')
            {
                numColorCombinations = NUM_COLOR_COMBINATION_HARD_DIFFICULTY;
                repeatColors = true;
            }

            // If the user inputs a '4', the expert difficulty will be used. Colors will repeat in this difficulty
            else if (option == '4')
            {
                numColorCombinations = NUM_COLOR_COMBINATION_EXPERT_DIFFICULTY;
                repeatColors = true;
            }

            // If the user inputs a '5', the custom difficulty will be used. The user can choose the number of colors in the color combination and whether colors can repeat or not.
            else if (option == '5')
            {
                Console.Out.Write("Enter the number of colors in the color combination: ");

                // While the user doesn't enter an integer or if that integer is zero or negative, continue to ask the user to enter the number of colors in the color combination
                while (!int.TryParse(Console.In.ReadLine(), out numColorCombinations) || numColorCombinations <= 0)
                    Console.Out.Write("Input must be a positive integer: ");

                // If the number of colors in the color combination is less than or equal to the total number of colors, ask the user if they want colors to repeat
                if (numColorCombinations <= COLOR_OPTIONS.Length)
                {
                    Console.Out.Write("Do you want colors to repeat in the combination [y/n]?: ");

                    customRepeatColorsChoice = Console.ReadKey().KeyChar;   // Read the user's character input

                    Console.Out.Write(Environment.NewLine);

                    // While the user doesn't enter 'y' or 'n', continue to ask the user to input 'y' or 'n'
                    while (customRepeatColorsChoice != 'y' && customRepeatColorsChoice != 'n')
                    {
                        Console.Out.Write("Input must be 'y' (yes) or 'n' (no): ");
                        customRepeatColorsChoice = Console.ReadKey().KeyChar;       // Read the user's character input
                        Console.Out.Write(Environment.NewLine);
                    }

                    // If the user inputted 'y', colors can repeat in the color combination
                    if (customRepeatColorsChoice == 'y')
                        repeatColors = true;

                    // If the user inputted 'n', colors will not repeat in the color combination
                    else
                        repeatColors = false;
                }

                // If the number of colors in the color combination is greater than the total number of colors, colors will have to repeat.
                else
                    repeatColors = true;

                Console.Out.Write(Environment.NewLine);
            }

            // Formula to determine the number of guesses the user gets
            numGuesses = (int)(Math.Log(Math.Pow(COLOR_OPTIONS.Length, numColorCombinations)) / Math.Log(2));
        }

        // The playGame() method has the user guess the colors in the color combination
        private static void playGame(int numColorCombination, int numGuesses, bool repeatColors)
        {
            int correctGuesses = 0;                                         // Keeps track of the number of correct guesses in the user's color combination
            int guess;                                                      // Determines if the color guess is one of the right colors but not in the right position, or if the color is is not in the color combination
            string[] colorCombination = new string[numColorCombination];    // String array with the correct colors in the color combination
            string[] userColorGuess = new string[numColorCombination];      // String array with the user's guesses of colors

            // Prints all the total possible colors
            Console.Out.WriteLine("All the possible colors are " + printArray(COLOR_OPTIONS));

            Console.Out.Write("In this difficulty, there are " + numColorCombination + " colors in the ordered color combination and you are given " + numGuesses + " guesses. ");

            // Fill the color combination with random colors
            randomizeColorCombination(numColorCombination, repeatColors, colorCombination);

            // Debug mode. If set to true, the correct color combination will be printed. Otherwise, it won't be printed.
            if (DEBUG)
                Console.Out.WriteLine(printArray(colorCombination));

            // User will guess the color combination until the correct one is guessed or the number of guesses are all used up
            for (int i = 0; i < numGuesses; i++)
            {
                Console.Out.Write("\nEnter your guess: ");

                // Take each color in the user's guesses of colors and put as many as needed in the user's color combination guess.
                userColorsToArray(numColorCombination, userColorGuess);

                correctGuesses = 0; // Reset the number of correct guesses to zero

                // Check to see if the user's color combination guess is correct
                for (int j = 0; j < numColorCombination; j++)
                {
                    // If one color of the user's color combination guess doesn't match up with the correct color combination, don't check the rest
                    if (userColorGuess[j] != colorCombination[j])
                        break;
                    correctGuesses++;
                }

                // If the user's color combination guess is correct, stop the game and output winner message
                if (correctGuesses == numColorCombination)
                {
                    Console.Out.WriteLine("\nYou got the right combination!!" + Environment.NewLine + "Winner Winner Chicken Dinner!!");    // Winner message
                    break;
                }

                Console.Out.Write(Environment.NewLine); // Format with new line

                // If the user's color combination guess is not correct, output one of three possible outcomes for each color in the user's color combination guess
                for (int j = 0; j < numColorCombination; j++)
                {
                    // If one of the colors in the user's color combination matches the same position as the correct color combination, let the user know and immediately check the next color
                    if (userColorGuess[j] == colorCombination[j])
                    {
                        Console.Out.WriteLine("Color " + (j + 1) + " (" + userColorGuess[j] + ") is in the right position");
                        continue;
                    }

                    guess = 0;  // Reset guess to zero

                    // Compare one of the user's color combinaion guesses with each position in the correct color combination until a match is found
                    for (int k = 0; k < numColorCombination; k++)
                    {
                        // If one of the colors in the user's color combination matches some color in the correct color combination but not in the right position, let the user know
                        if (userColorGuess[j] == colorCombination[k])
                        {
                            Console.Out.WriteLine("Color " + (j + 1) + " (" + userColorGuess[j] + ") is one of the right colors, but its not in the right position");
                            break;
                        }
                        guess++;
                    }

                    // If one of the colors in the user's color combination isn't any of the colors in the correct color combination, let the user know
                    if (guess == numColorCombination)
                        Console.Out.WriteLine("Color " + (j + 1) + " (" + userColorGuess[j] + ") is not one of the right colors");
                }
            }

            Console.Out.Write(Environment.NewLine); // Format with a new line

            // If the user's number of guesses are all used up, stop the game and output loser message
            if (correctGuesses != numColorCombination)
                Console.Out.WriteLine("You lose..." + Environment.NewLine + "Try again next time!");
        }

        // The displayRules() method outputs the rules on the screen.
        private static void displayRules()
        {
            Console.Out.WriteLine("-------------------------------------------------------------------------- Rules ---------------------------------------------------------------------------\n");
            Console.Out.WriteLine("1. Enter the color combination in the right order and you win!");
            Console.Out.WriteLine("2. The only colors you can guess are " + printArray(COLOR_OPTIONS));
            Console.Out.WriteLine("3. Depending on the difficulty, colors can or can't repeat in the combination");
            Console.Out.WriteLine("4. Type the colors in the combination in succession, seperating each color with one whitespace. Ex: Blue Green Yellow Red");
            Console.Out.WriteLine("5. You will be given a number of guesses depending on the difficulty. If you use up all your guesses before finding the correct color combination, you lose!\n");
        }

        // The randomizeColorCombination method randomizes the correct color combination. Colors can or can not repeat
        private static void randomizeColorCombination(int numColorCombination, bool repeatColors, string[] colorCombination)
        {
            Random rand = new Random(); // Random Number Generator. Used to randomly select a color

            // Colors in the color combination can repeat
            if (repeatColors)
            {
                Console.Out.WriteLine("Colors can repeat.");

                // Randomly select colors from all the total possibe colors and put them in the color combination
                for (int i = 0; i < numColorCombination; i++)
                    colorCombination[i] = COLOR_OPTIONS[rand.Next(COLOR_OPTIONS.Length)];
            }

            // Otherwise, colors in the color combination will not repeat
            else
            {
                Console.Out.WriteLine("Colors will not repeat.");

                for (int i = 0; i < numColorCombination;)
                {
                    // Randomly select a color from all the total possibe colors and put it in the corresponding index in the color combination
                    colorCombination[i] = COLOR_OPTIONS[rand.Next(COLOR_OPTIONS.Length)];

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

        // The userColorsToArray method takes each color in the string of user's guesses of colors and puts as many needed in the user's color combination guess array.
        private static void userColorsToArray(int numColorCombination, string[] userColorGuess)
        {
            int missingColors = numColorCombination;    // Keeps track of the number of missing colors if the user didn't correctly input the number of colors in the color combination
            string colors;                              // User's guesses of colors

            // Resets user's guesses of colors to an empty array
            for (int i = 0; i < numColorCombination; i++)
                userColorGuess[i] = "";

            // If the user forgot to enter some number of colors in the color combination, continue to ask the user to enter the number colors needed until the user's color combination guess is filled up
            do
            {
                colors = Console.In.ReadLine(); // Read input

                // If the user hits 'enter' with no colors entered in the guess, don't do anything
                if (colors != "")
                {
                    // Enter colors in each empty index of the user's color combination guess
                    for (int i = 0, j = numColorCombination - missingColors; i < colors.Length && j < numColorCombination; i++)
                    {
                        // If the character in the user's guess of colors isn't a white space, add that character to the index
                        if (colors[i] != ' ')
                            userColorGuess[j] += colors[i];

                        // If the next character in the user's guess of colors isn't the end of the string and if the next character isn't a whitespace, reduce missing colors by 1 and go to the next index in the user's color combination guess
                        else if (i + 1 < colors.Length && colors[i + 1] != ' ')
                        {
                            missingColors--;
                            j++;
                        }
                    }

                    missingColors--;    // If only one color entered in the user's guess of colors, reduce missing colors by 1
                }

                // If there are any colors missing in the user's guesses of colors, tell the user to input that many more colors
                if (missingColors > 0)
                    Console.Out.Write("Enter " + missingColors + " more color(s): ");

            } while (missingColors > 0);    // If there are any colors missing in the user's color combination, repeat until there aren't any colors missing
        }
    }
}