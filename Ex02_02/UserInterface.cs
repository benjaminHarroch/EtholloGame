using System;

namespace Ex02_02
{
    internal class UserInterface
    {
        public const int k_LittleBoardSize = 6;
        public const int k_HighBoardSize = 8;

        public static string GetUserName()
        {
            Console.WriteLine("Please enter your name:");
            string userName = Console.ReadLine();
            return userName;
        }

        public static int GetDesiredBoardSize()
        {
            bool isValidSize = false;
            int desiredSize = 0;
            bool isValidInput;

            while (!isValidSize)
            {
                Console.WriteLine("Please enter the desired board size: 1. 8x8 or 2. 6x6");
                isValidInput = int.TryParse(Console.ReadLine(), out desiredSize);

                if (isValidInput && (desiredSize == 1 || desiredSize == 2))
                {
                    desiredSize = (desiredSize == 2) ? k_LittleBoardSize : k_HighBoardSize;
                    isValidSize = true;
                }
                else
                {
                    PrintMessage("Invalid size selection. Please choose again.");
                }
            }

            return desiredSize;
        }

        public static int ChooseSecondPlayer()
        {
            Console.WriteLine("Please choose against whom you want to play:");
            Console.WriteLine("1. Computer");
            Console.WriteLine("2. Player");

            bool isValidInput = false;
            int chosenOption = 0;

            while (!isValidInput)
            {
                string secondPlayer = Console.ReadLine();

                if (int.TryParse(secondPlayer, out chosenOption) && (chosenOption == 1 || chosenOption == 2))
                {
                    isValidInput = true;
                }
                else
                {
                    PrintMessage("Invalid input. Please choose 1 or 2.");
                }
            }

            return chosenOption;
        }

        public static void PrintMessage(string i_Message)
        {
            Console.WriteLine(i_Message);
        }

        public static void PrintBoard(string[,] i_BoardGame)
        {
            int boardSize = i_BoardGame.GetLength(0);

            // Print column headers (A, B, C, etc.)
            Console.Write("   ");
            for (int i = 0; i < boardSize; i++)
            {
                Console.Write($" {Convert.ToChar('A' + i)}  ");
            }
            Console.WriteLine();

            // Print board rows with row numbers
            for (int i = 0; i < boardSize; i++)
            {
                Console.WriteLine("  " + new string('=', boardSize * 4)); // Row divider
                Console.Write($"{i + 1} "); // Row number

                for (int j = 0; j < boardSize; j++)
                {
                    Console.Write($"| {i_BoardGame[i, j]} ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("  " + new string('=', boardSize * 4)); // Final row divider
        }

        public static (int, int) GetMoveInput()
        {
            bool isValidInput = false;
            int row = -1, col = -1;

            while (!isValidInput)
            {
                Console.WriteLine("Enter your move (e.g., E3):");
                string input = Console.ReadLine();

                if (input == "Q")
                {
                    Environment.Exit(0);
                }

                if (input.Length == 2 && char.IsLetter(input[0]) && char.IsDigit(input[1]))
                {
                    col = char.ToUpper(input[0]) - 'A';
                    row = int.Parse(input[1].ToString()) - 1;

                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please use the format of a letter and a number, e.g., E3.");
                }
            }

            return (row, col);
        }

        public static bool AskToRestart()
        {
            Console.WriteLine("Do you want to play one more game?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");

            bool isExitCode = true;
            bool isValidInput = int.TryParse(Console.ReadLine(), out int responseToOneMoreGame);

            while (isValidInput && isExitCode)
            {
                if (responseToOneMoreGame == 2)
                {
                    Console.WriteLine("Goodbye, see you next time!");
                    isExitCode = true;
                }
                else if (responseToOneMoreGame == 1)
                {
                    isExitCode = false;
                }
                else
                {
                    Console.WriteLine("Please choose 1 or 2.");
                    isValidInput = int.TryParse(Console.ReadLine(), out responseToOneMoreGame);
                }
            }

            return isExitCode;
        }
    }
}
