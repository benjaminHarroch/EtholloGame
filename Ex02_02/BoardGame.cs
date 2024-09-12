using System;

namespace Ex02_02
{
    internal class BoardGame
    {
        private const string k_Empty = " ";
        private const string k_Black = "X";
        private const string k_White = "O";

        private string[,] m_boardGame;
        private int m_row;
        private int m_col;
        private int m_countWhite = 2;
        private int m_countBlack = 2;
        private int m_countEmpty;

        public BoardGame(int i_row, int i_col)
        {
            InitBoardGame(i_row, i_col);
        }

        public void InitBoardGame(int i_row, int i_col)
        {
            m_countWhite = 2;
            m_countBlack = 2;
            m_boardGame = new string[i_row, i_col];
            m_row = i_row;
            m_col = i_col;
            int size = i_row;
            m_countEmpty = (size * size) - (m_countBlack + m_countWhite);

            for (int j = 0; j < i_row; j++)
            {
                for (int k = 0; k < i_col; k++)
                {
                    m_boardGame[j, k] = k_Empty;
                }
            }

            m_boardGame[((size / 2) - 1), ((size / 2) - 1)] = k_White;
            m_boardGame[((size / 2) - 1), (size / 2)] = k_Black;
            m_boardGame[(size / 2), ((size / 2) - 1)] = k_Black;
            m_boardGame[(size / 2), (size / 2)] = k_White;
        }

        public bool IsValidMove(int i_row, int i_col, string i_playerSign)
        {
            if (!IsOnBoundaries(i_row, i_col) || m_boardGame[i_row, i_col] != k_Empty)
            {
                return false;
            }

            bool isValid = false;
            int[] directions = { -1, 0, 1 };

            foreach (int dirRow in directions)
            {
                foreach (int dirCol in directions)
                {
                    if (dirRow == 0 && dirCol == 0)
                    {
                        continue;
                    }

                    int r = i_row + dirRow;
                    int c = i_col + dirCol;
                    bool hasOpponentBetween = false;

                    while (IsOnBoundaries(r, c) && m_boardGame[r, c] == GetOpponentSign(i_playerSign))
                    {
                        hasOpponentBetween = true;
                        r += dirRow;
                        c += dirCol;
                    }

                    if (hasOpponentBetween && IsOnBoundaries(r, c) && m_boardGame[r, c] == i_playerSign)
                    {
                        isValid = true;
                        break;
                    }
                }

                if (isValid)
                {
                    break;
                }
            }

            return isValid;
        }

        private bool IsOpponent(string playerSign, string cellValue)
        {
            return cellValue != playerSign && cellValue != k_Empty;
        }

        private bool IsOnBoundaries(int i_row, int i_col)
        {
            return i_row >= 0 && i_row < m_boardGame.GetLength(0) && i_col >= 0 && i_col < m_boardGame.GetLength(1);
        }

        public void DoMove(int i_row, int i_col, string i_playerSign)
        {
            m_boardGame[i_row, i_col] = i_playerSign;

            int[] directions = { -1, 0, 1 };

            foreach (int dirRow in directions)
            {
                foreach (int dirCol in directions)
                {
                    if (dirRow == 0 && dirCol == 0)
                    {
                        continue;
                    }

                    int currentRow = i_row + dirRow;
                    int currentCol = i_col + dirCol;

                    if (!IsOnBoundaries(currentRow, currentCol) || !IsOpponent(i_playerSign, m_boardGame[currentRow, currentCol]))
                    {
                        continue;
                    }

                    while (IsOnBoundaries(currentRow, currentCol) && IsOpponent(i_playerSign, m_boardGame[currentRow, currentCol]))
                    {
                        currentRow += dirRow;
                        currentCol += dirCol;
                    }

                    if (IsOnBoundaries(currentRow, currentCol) && m_boardGame[currentRow, currentCol] == i_playerSign)
                    {
                        currentRow -= dirRow;
                        currentCol -= dirCol;

                        while (currentRow != i_row || currentCol != i_col)
                        {
                            m_boardGame[currentRow, currentCol] = i_playerSign;
                            currentRow -= dirRow;
                            currentCol -= dirCol;
                        }
                    }
                }
            }
        }

        public void CountColor(int i_row, int i_col)
        {
            m_countWhite = 0;
            m_countBlack = 0;

            for (int i = 0; i < i_row; i++)
            {
                for (int j = 0; j < i_col; j++)
                {
                    if (m_boardGame[i, j] == k_Black)
                    {
                        m_countBlack++;
                    }

                    if (m_boardGame[i, j] == k_White)
                    {
                        m_countWhite++;
                    }
                }
            }
        }

        public int GetCountWhite
        {
            get { return m_countWhite; }
        }

        public int GetCountBlack
        {
            get { return m_countBlack; }
        }

        public string[,] GetBoard
        {
            get { return m_boardGame; }
        }

        public int GetRow
        {
            get { return m_row; }
        }

        public int GetCol
        {
            get { return m_col; }
        }

        public string GetOpponentSign(string i_currentSign)
        {
            return i_currentSign == k_Black ? k_White : k_Black;
        }
    }
}
