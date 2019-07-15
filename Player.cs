using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class Player
    {
        private string m_PlayerName;
        private List<string> m_PlayerValidMoveList;
        private char m_color; 

        public void SetPlayerName(string i_PlayerName)
        {
            this.m_PlayerName = i_PlayerName;
        }

        public string GetPlayerName()
        {
            return this.m_PlayerName;
        }

        public void SetPlayerValidMoveList(List<string> i_PlayerValidMoveList)
        {
            this.m_PlayerValidMoveList = i_PlayerValidMoveList;
        }

        public List<string> GetPlayerValidMoveList()
        {
            return this.m_PlayerValidMoveList;
        }

        public void SetColor(char i_Color)
        {
            this.m_color = i_Color;
        }

        public char GetColor()
        {
            return this.m_color;
        }
    }
}
