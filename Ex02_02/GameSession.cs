using Ex02.ConsoleUtils;
using System;
using System.Collections.Generic;

namespace Ex02_02
{
    internal class GameSession
    {
        private BoardGame m_Board;
        private Player[] m_Players;
        private List<int[]> m_Moves;
        private bool m_ExitCode = false;

        public GameSession()
        {
            initializeGame();
        }

        public void initializeGame()
        {
            setupGameParameters();

            while (!m_ExitCode)
            {
                StartGame();
                m_Board.InitBoardGame(m_Board.GetRow, m_Board.GetCol);
                m_ExitCode = UserInterface.AskToRestart();
            }
        }

        private void setupGameParameters()
        {
            string i_FirstPlayerName = UserInterface.GetUserName();
            m_Players = new Player[2];
            m_Players[0] = new Player(i_FirstPlayerName, "X", false);

            if (UserInterface.ChooseSecondPlayer() == 1)
            {
                m_Players[1] = new Player("Computer", "O", true);
            }
            else
            {
                string i_SecondPlayerName = UserInterface.GetUserName();
                m_Players[1] = new Player(i_SecondPlayerName, "O", false);
            }

            int i_BoardSize = UserInterface.GetDesiredBoardSize();
            m_Board = new BoardGame(i_BoardSize, i_BoardSize);
            UserInterface.PrintBoard(m_Board.GetBoard);
        }

        public void StartGame()
        {
            bool i_GameIsRunning = true;
            Player i_CurrentPlayer = m_Players[0];
            Player i_OpponentPlayer = m_Players[1];

            while (i_GameIsRunning)
            {
                Screen.Clear();
                UserInterface.PrintBoard(m_Board.GetBoard);

                if (!HasValidMoves(i_CurrentPlayer.Sign))
                {
                    UserInterface.PrintMessage($"{i_CurrentPlayer.Name} has no valid moves. Skipping turn.");
                    SwapPlayers(ref i_CurrentPlayer, ref i_OpponentPlayer);
                    continue;
                }

                executeMove(i_CurrentPlayer);

                if (!HasValidMoves(i_CurrentPlayer.Sign) && !HasValidMoves(i_OpponentPlayer.Sign))
                {
                    i_GameIsRunning = false;
                    announceWinner();
                }

                SwapPlayers(ref i_CurrentPlayer, ref i_OpponentPlayer);
            }
        }

        private void executeMove(Player i_CurrentPlayer)
        {
            if (i_CurrentPlayer.IsComputer)
            {
                handleComputerMove(i_CurrentPlayer);
            }
            else
            {
                handlePlayerMove(i_CurrentPlayer);
            }

            m_Board.CountColor(m_Board.GetRow, m_Board.GetCol);
        }

        private void handlePlayerMove(Player i_Player)
        {
            bool i_MoveValid = false;

            while (!i_MoveValid)
            {
                (int i_Row, int i_Col) = UserInterface.GetMoveInput();

                if (m_Board.IsValidMove(i_Row, i_Col, i_Player.Sign))
                {
                    m_Board.DoMove(i_Row, i_Col, i_Player.Sign);
                    i_MoveValid = true;
                }
                else
                {
                    UserInterface.PrintMessage("Invalid move. Please try again.");
                }
            }
        }

        private void handleComputerMove(Player i_Computer)
        {
            generateValidMoves(i_Computer.Sign);

            if (m_Moves.Count > 0)
            {
                Random i_Rnd = new Random();
                int i_RandomMoveIndex = i_Rnd.Next(m_Moves.Count);
                int[] i_SelectedMove = m_Moves[i_RandomMoveIndex];
                m_Board.DoMove(i_SelectedMove[0], i_SelectedMove[1], i_Computer.Sign);
            }
            else
            {
                UserInterface.PrintMessage("No valid moves available for the computer.");
            }
        }

        private void generateValidMoves(string i_Sign)
        {
            m_Moves = new List<int[]>();
            for (int i_Row = 0; i_Row < m_Board.GetRow; i_Row++)
            {
                for (int i_Col = 0; i_Col < m_Board.GetCol; i_Col++)
                {
                    if (m_Board.IsValidMove(i_Row, i_Col, i_Sign))
                    {
                        m_Moves.Add(new int[] { i_Row, i_Col });
                    }
                }
            }
        }

        private void SwapPlayers(ref Player i_CurrentPlayer, ref Player i_OpponentPlayer)
        {
            Player i_Temp = i_CurrentPlayer;
            i_CurrentPlayer = i_OpponentPlayer;
            i_OpponentPlayer = i_Temp;
        }

        private bool HasValidMoves(string i_PlayerSign)
        {
            for (int i_Row = 0; i_Row < m_Board.GetRow; i_Row++)
            {
                for (int i_Col = 0; i_Col < m_Board.GetCol; i_Col++)
                {
                    if (m_Board.IsValidMove(i_Row, i_Col, i_PlayerSign))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void announceWinner()
        {
            int i_BlackCount = m_Board.GetCountBlack;
            int i_WhiteCount = m_Board.GetCountWhite;

            if (i_BlackCount > i_WhiteCount)
            {
                UserInterface.PrintMessage("Black wins!");
            }
            else if (i_WhiteCount > i_BlackCount)
            {
                UserInterface.PrintMessage("White wins!");
            }
            else
            {
                UserInterface.PrintMessage("It's a tie!");
            }
        }
    }
}
