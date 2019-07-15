using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class GameUi
    {
        private OtheloLogic m_GameLogic = new OtheloLogic();
        private Player m_Player1 = new Player();
        private Player m_Player2 = new Player();
        private int m_SelectedBoardSize;
        private char[,] m_Board;

        public void GameMenu()
        {
            this.startGame();
            m_Board = buildBoard();
            int returnValue = playGame();
            if (returnValue == 0)
            {
                System.Console.WriteLine("Thank you for playing");
                return;
            }

            System.Console.WriteLine("Do you want to play again ? (Y/N)");
            string input = System.Console.ReadLine();
            while (true)
            {
                while (!input.Equals("Y") && !input.Equals("N"))
                {
                    System.Console.WriteLine("Do you want to play again ? (Y/N) enter Y for yes or N for no ");
                    input = System.Console.ReadLine();
                }

                if (input.Equals("Y") == true)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    break;
                }
                else
                {
                    System.Console.WriteLine("Thank you for playing");
                    return;
                }
            }

            m_Board = buildBoard();
            playGame();
        }

        private void startGame()
        {
            string input;
            string name;
            int selectedOption;
            bool result;
            System.Console.WriteLine("Welcome to Othelo game what is your name ?");
            name = System.Console.ReadLine();
            m_Player1.SetPlayerName(name);
            m_Player1.SetColor('X');
            System.Console.WriteLine("Hey " + m_Player1.GetPlayerName() + " do you want to play against another player or against the computer? please select 1 or 2" + Environment.NewLine + "1.Against another player" + Environment.NewLine + "2.Against the computer");
            input = System.Console.ReadLine();
            result = checkInput(input);
            while (!result)
            {
                System.Console.WriteLine("please select 1 or 2" + Environment.NewLine + "1.Against another player" + Environment.NewLine + "2.Against the computer");
                input = System.Console.ReadLine();
                result = checkInput(input);
            }

            selectedOption = int.Parse(input);
            if (selectedOption == 1)
            {
                System.Console.WriteLine("Please enter player number 2 name");
                name = System.Console.ReadLine();
                m_Player2.SetPlayerName(name);
            }
            else
            {
                name = "Computer";
                m_Player2.SetPlayerName(name);
            }

            m_Player2.SetColor('O');
            System.Console.WriteLine("Please select board size" + Environment.NewLine + "1.6X6" + Environment.NewLine + "2.8X8" + Environment.NewLine + "please select 1 or 2");
            input = System.Console.ReadLine();
            result = checkInput(input);
            while (!result)
            {
                System.Console.WriteLine("please select 1 or 2" + Environment.NewLine + "1.6X6" + Environment.NewLine + "2.8X8");
                input = System.Console.ReadLine();
                result = checkInput(input);
            }

            selectedOption = int.Parse(input);
            if (selectedOption == 1)
            {
                m_SelectedBoardSize = 6;
            }
            else
            {
                m_SelectedBoardSize = 8;
            }

            return;
        }

        private bool checkInput(string i_Input)
        {
            int selectedInput;
            bool result = int.TryParse(i_Input, out selectedInput);
            if (!result || (selectedInput != 1 && selectedInput != 2))
            {
                return false;
            }

            return true;
        }

        private char[,] buildBoard()
        {
            char[,] board = new char[m_SelectedBoardSize, m_SelectedBoardSize];
            for (int row = 0; row < m_SelectedBoardSize; row++)
            {
                for (int col = 0; col < m_SelectedBoardSize; col++)
                {
                    board[row, col] = '0';
                }
            }

            board[(m_SelectedBoardSize / 2) - 1, (m_SelectedBoardSize / 2) - 1] = 'O';
            board[(m_SelectedBoardSize / 2) - 1, m_SelectedBoardSize / 2] = 'X';
            board[m_SelectedBoardSize / 2, (m_SelectedBoardSize / 2) - 1] = 'X';
            board[m_SelectedBoardSize / 2, m_SelectedBoardSize / 2] = 'O';
            return board;
        }

        private void printBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            char letter = 'A';
            System.Console.Write("   ");
            for (int col = 1; col <= m_SelectedBoardSize; col++)
            {
                System.Console.Write("  " + letter + " ");
                letter += (char)1;
            }

            for (int row = 0; row < m_SelectedBoardSize; row++)
            {
                System.Console.Write(Environment.NewLine);
                for (int col = 0; col <= m_SelectedBoardSize; col++)
                {
                    System.Console.Write("====");
                }

                System.Console.Write(Environment.NewLine);
                System.Console.Write(" " + (row + 1) + " |");
                for (int col = 0; col < m_SelectedBoardSize; col++)
                {
                    if (m_Board[row, col] != '0')
                    {
                        System.Console.Write(" " + m_Board[row, col] + " |");
                    }
                    else
                    {
                        System.Console.Write("   |");
                    }
                }
            }

            System.Console.Write(Environment.NewLine);
        }

        private int playGame()
        {
            short playerTurn = 1;
            int returnValueFromTurn;
            m_Player1.SetPlayerValidMoveList(m_GameLogic.MakeListOfValidMoves(m_Board, m_Player1.GetColor()));
            m_Player2.SetPlayerValidMoveList(m_GameLogic.MakeListOfValidMoves(m_Board, m_Player2.GetColor()));
            while (m_Player1.GetPlayerValidMoveList().Count != 0 || m_Player2.GetPlayerValidMoveList().Count != 0)
            {
                m_Player1.SetPlayerValidMoveList(m_GameLogic.MakeListOfValidMoves(m_Board, m_Player1.GetColor()));
                m_Player2.SetPlayerValidMoveList(m_GameLogic.MakeListOfValidMoves(m_Board, m_Player2.GetColor()));
                playerTurn *= -1;
                printBoard();
                if (playerTurn == -1)
                {
                    if (m_Player1.GetPlayerValidMoveList().Count == 0)
                    {
                        System.Console.WriteLine(m_Player1.GetPlayerName() + " has no valid moves");
                        continue;
                    }

                    returnValueFromTurn = turn(this.m_Player1);
                }
                else
                {
                    if (m_Player2.GetPlayerValidMoveList().Count == 0)
                    {
                        System.Console.WriteLine(m_Player2.GetPlayerName() + " has no valid moves");
                        continue;
                    }

                    returnValueFromTurn = turn(this.m_Player2);
                }

                if (returnValueFromTurn == 0)
                {
                    return 0;
                }
            }

            printBoard();
            checkWhoWon();
            return 1;
        }

        private int turn(Player i_Player)
        {
            string move;
            int row;
            int col;
            int listIndex;
            Random randomNumber = new Random();
            if (i_Player.GetPlayerName().Equals("Computer") == true)
            {
                System.Console.WriteLine(Environment.NewLine + i_Player.GetPlayerName() + " turn");
                listIndex = randomNumber.Next(0, i_Player.GetPlayerValidMoveList().Count);
                move = i_Player.GetPlayerValidMoveList()[listIndex];
                row = (int)char.GetNumericValue(move[0]);
                col = (int)char.GetNumericValue(move[1]);
                System.Threading.Thread.Sleep(2000);
                m_GameLogic.FlipDiscs(ref m_Board, row, col, i_Player.GetColor());
                return 1;
            }
            else
            {
                System.Console.WriteLine(Environment.NewLine + "Enter 'Q' if you wish to end the current game");
                System.Console.WriteLine(i_Player.GetPlayerName() + "'s turn (color = " + i_Player.GetColor() + ") please enter your move first row(number) and then column(capital letter)");
                move = scanMove(i_Player);
                if (move.Equals("0") == true)
                {
                    return 0;
                }

                row = (int)char.GetNumericValue(move[0]);
                col = (int)char.GetNumericValue(move[1]);
                m_GameLogic.FlipDiscs(ref m_Board, row, col, i_Player.GetColor());
                return 1;
            }
        }

        private string scanMove(Player i_Player)
        {
            string input;
            bool isInputOk;
            input = System.Console.ReadLine();
            if (input.Equals("Q") == true)
            {
                return "0";
            }

            isInputOk = checkIfMoveIsOk(input, i_Player.GetPlayerValidMoveList());
            while (!isInputOk)
            {
                input = System.Console.ReadLine();
                if (input.Equals("Q") == true)
                {
                    return "0";
                }

                isInputOk = checkIfMoveIsOk(input, i_Player.GetPlayerValidMoveList());
            }

            int firstChar = (int)char.GetNumericValue(input[0]);
            firstChar -= 1;
            int secondChar = (int)input[1] - 'A';
            string move = firstChar.ToString() + secondChar.ToString();
            return move;
        }

        private bool checkIfMoveIsOk(string i_Input, List<string> i_PlayerValidMovesList)
        {
            if (i_Input.Length != 2)
            {
                System.Console.WriteLine("Please enter your move, up to 2 chars ONLY!, first row(number) then column(capital letter) ");
                return false;
            }

            if (!char.IsDigit(i_Input[0]) && !char.IsLetter(i_Input[1]) && !char.IsUpper(i_Input[1]))
            {
                System.Console.WriteLine("Please enter valid move, first row(number) then column(capital letter)");
                return false;
            }

            char letter = 'A';
            letter += (char)(m_SelectedBoardSize - 1);
            if (i_Input[1] < 'A' || i_Input[1] > letter)
            {
                System.Console.WriteLine("Please enter valid move, first row(number) then column(capital letter) capital letter must be between A to " + letter);
                return false;
            }

            if (char.GetNumericValue(i_Input[0]) > m_SelectedBoardSize || char.GetNumericValue(i_Input[0]) < 0)
            {
                System.Console.WriteLine("Please enter valid move, first row(number) then column(capital letter) number must be between 1 to " + m_SelectedBoardSize.ToString());
                return false;
            }

            int firstChar = (int)char.GetNumericValue(i_Input[0]);
            firstChar -= 1;
            int secondChar = (int)i_Input[1] - 'A';
            string move = firstChar.ToString() + secondChar.ToString();
            foreach (string validMove in i_PlayerValidMovesList)
            {
                if (validMove.Equals(move))
                {
                    return true;
                }
            }

            System.Console.WriteLine("You can not make this move please insert a valid move");
            return false;
        }

        private void checkWhoWon()
        {
            int xCount = 0;
            int oCount = 0;
            for (int row = 0; row < m_SelectedBoardSize; row++)
            {
                for (int col = 0; col < m_SelectedBoardSize; col++)
                {
                    if (m_Board[row, col] == 'X')
                    {
                        xCount++;
                    }
                    else if (m_Board[row, col] == 'O')
                    {
                        oCount++;
                    }
                }
            }

            System.Console.WriteLine(m_Player1.GetPlayerName() + " points: " + xCount + Environment.NewLine + m_Player2.GetPlayerName() + " points: " + oCount);
            if (xCount > oCount)
            {
                System.Console.WriteLine(m_Player1.GetPlayerName() + " won the game");
            }
            else if (oCount > xCount)
            {
                System.Console.WriteLine(m_Player2.GetPlayerName() + " won the game");
            }
            else
            {
                System.Console.WriteLine("it's a tie !");
            }

            return;
        }
    }
}