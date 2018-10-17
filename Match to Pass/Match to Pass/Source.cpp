#include <iostream>
#include <ctime>
#include <cstdlib>
#include <string>
using namespace std;

int main()
{
	string g1, g2, g3;

	int a;
	const int ARRAY_SIZE = 5;
	srand(time(0));
	string m1[ARRAY_SIZE] = { "Green","Blue", "Yellow" ,"Orange", "Pink" };
	string m2[ARRAY_SIZE] = { "Green","Blue", "Yellow", "Orange", "Pink" };
	string m3[ARRAY_SIZE] = { "Green","Blue", "Yellow", "Orange", "Pink" };

	cout << "1. Play the Game!" << endl;
	cout << "2. Read the Rules!" << endl;
	cout << "3. Exit." << endl;

	cin >> a;

	if (a == 2)
	{
		cout << "Rules" << endl << "_________________________________" << endl << "1. The only colors you can guess are Blue, Yellow, Orange, Green, and Pink." << endl;
		cout << "2. Type 3 colors in succession that you think is the correct combination. Ex: Blue Green Yellow. " << endl;
		cout << "3. You get 10 guesses. After the 10th incorrect guess, you lose!" << endl;
		cout << "4. Enter the correct combination and you win." << endl;

		cout << "Press 1 to play the game ";


		cin >> a;
	}
	else if (a == 3)
	{
		exit(0);
	}

	do {
		if (a == 1)
		{
			for (int i = 0; i < 1; i++)
			{
				int index = rand() % ARRAY_SIZE;
				m1[i] = m1[index];
				int index1 = rand() % ARRAY_SIZE;
				m2[i] = m2[index1];
				int index2 = rand() % ARRAY_SIZE;
				m3[i] = m3[index2];
			}

			for (int i = 0; i < 1; i++)
			{
				cout << m1[i] << " ";
				cout << " " << m2[i] << " ";
				cout << " " << m3[i] << " ";
			}
			cout << endl;

			cout << "Enter your guess (Pick between Green, Orange, Pink, Blue, and Yellow)" << endl;
			for (int n = 0; n < 3; n++)
			{
				cin >> g1 >> g2 >> g3;

				if (g1 == m1[0] && g2 == m2[0] && g3 == m3[0])
				{
					cout << "You got the right combination!!" << endl;
					cout << "Winner Winner Chicken Dinner!!" << endl;

					cout << "Do you want to play again? Press 1. Press 2 to Exit." << endl;

					n = 3;
					cin >> a;

				}

				else if (g1 == m1[0] && g2 == m2[0])
				{
					cout << "Color 1" << " and " << "Color 2" << " are in the right position and the right color!" << endl;
				}
				else if (g1 == m1[0] && g3 == m3[0])
				{
					cout << "Color 1" << " and " << "Color 3" << " are in the right position and the right color!" << endl;
				}
				else if (g2 == m2[0] && g3 == m3[0])
				{
					cout << "Color 2" << " and " << "Color 3" << " are in the right position and the right color!" << endl;
				}

				else if ((g1 == m2[0] || g1 == m3[0]) && (g2 == m1[0] || g2 == m3[0]) && (g3 == m1[0] || g3 == m2[0]))
				{
					if (g1 == m1[0])
					{
						cout << "Color 1 is correct, the other 2 are the right colors but wrong position" << endl;
					}
					else if (g2 == m2[0])
					{
						cout << "Color 2 is correct, the other 2 are the right colors but wrong position" << endl;
					}
					else if (g3 == m3[0])
					{
						cout << "Color 3 is correct, the other 2 are the right colors but wrong position" << endl;
					}
					else
					{
						cout << "Colors are correct but in the wrong position!" << endl;
					}
				}
				else if (g1 == m2[0] && g2 == m3[0])
				{
					cout << "Color 1" << " and " << "Color 2" << " are the right color but in the wrong position" << endl;
				}
				else if (g1 == m3[0] && g2 == m1[0])
				{
					cout << "Color 1" << " and " << "Color 2" << " are the right color but in the wrong position" << endl;
				}

				else if (g1 == m2[0] && g3 == m1[0])
				{
					cout << "Color 1" << " and " << "Color 3" << " are the right color but in the wrong position" << endl;
				}
				else if (g1 == m3[0] && g3 == m2[0])
				{
					cout << "Color 1" << " and " << "Color 3" << " are the right color but in the wrong position" << endl;
				}

				else if (g1 == m1[0] || g2 == m2[0] || g3 == m3[0])
				{
					cout << "1 Is the right color and position" << endl;
				}
				else if (g1 == m2[0] || g1 == m3[0] || g2 == m1[0] || g2 == m3[0] || g3 == m1[0] || g3 == m2[0])
				{
					cout << "1 is the right color, wrong position." << endl;
				}

				else if (g1 != m1[0] && g1 != m2[0] && g1 != m3[0] && g2 != m1[0] && g2 != m2[0] && g3 != m3[0] && g3 != m1[0] && g3 != m2[0] && g3 != m3[0])
				{
					cout << "None of the colors are the right color or the right position :(" << endl;
				}
			}
			cout << endl;

			cout << "You lose!" << endl;
			cout << "Do you want to play again? Press 1. Press 2 to Exit." << endl;

			cin >> a;
		}
	} while (a == 1);

	if (a == 2)
	{
		exit(0);
	}

	cout << endl;
	system("pause");
	return 0;
}