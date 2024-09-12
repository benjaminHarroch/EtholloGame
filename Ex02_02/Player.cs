using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_02
{
    internal class Player
    {
        // Define signs 
        private const string k_Black = "X";
        private const string k_White = "O";

        private string m_UserName;
        private string m_Sign;
        private int m_Score;
        private bool m_IsComputer;

        public Player(string i_Name, string i_Sign, bool i_IsComputer)
        {
            m_UserName = i_Name;
            m_Sign = i_Sign;
            m_IsComputer = i_IsComputer;
        }

        public bool IsComputer
        {
            get { return m_IsComputer; }
        }

        public string Sign
        {
            get { return m_Sign; }
            set { m_Sign = value; }
        }

        public string UserName
        {
            get { return m_UserName; }
            set { m_UserName = value; }
        }

        public int PlayerScore
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public string Name
        {
            get { return m_UserName; }
        }
    }
}
