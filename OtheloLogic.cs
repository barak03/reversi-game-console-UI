using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class OtheloLogic
    {
        public bool IsValidMove(ref char[,] i_Board, int i_Row, int i_Col, char i_Color)
        {
            if (i_Board[i_Row, i_Col] != '0')
            {
                return false;
            }

            for (int deltaRow = -1; deltaRow <= 1; deltaRow++)
            {
                for (int deltaCol = -1; deltaCol <= 1; deltaCol++)
                {
                    if (!(deltaCol == 0 && deltaRow == 0) && CanBeSwitched(i_Row, i_Col, deltaRow, deltaCol, i_Color, ref i_Board))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CanBeSwitched(int i_Row, int i_Col, int i_DeltaRow, int i_DeltaCol, char i_Color, ref char[,] i_Board)
        {
            int row = i_Row + i_DeltaRow;
            int col = i_Col + i_DeltaCol;
            while (row >= 0 && row < i_Board.GetLength(0) && col >= 0 && col < i_Board.GetLength(0) && !i_Board[row, col].Equals(i_Color) && i_Board[row, col] != '0')
            {
                row += i_DeltaRow;
                col += i_DeltaCol;
            }

            if (row < 0 || row >= i_Board.GetLength(0) || col < 0 || col >= i_Board.GetLength(0) || !i_Board[row, col].Equals(i_Color) || ((row - i_DeltaRow == i_Row && col - i_DeltaCol == i_Col) && (i_Board[row, col] != '0')))
            {
                return false;
            }

            return true;
        }

        public List<string> MakeListOfValidMoves(char[,] i_Board, char i_Color)
        {
            string temporaryString;
            List<string> ValidMovesList = new List<string>();
            for (int row = 0; row < i_Board.GetLength(0); row++)
            {
                for (int col = 0; col < i_Board.GetLength(0); col++)
                {
                    if (IsValidMove(ref i_Board, row, col, i_Color) == true)
                    {
                        temporaryString = row.ToString() + col.ToString();
                        ValidMovesList.Add(temporaryString);
                    }
                }
            }

            return ValidMovesList;
        }

        public void FlipDiscs(ref char[,] i_Board, int i_Row, int i_Col, char i_Color)
        {
            i_Board[i_Row, i_Col] = i_Color;
            int row;
            int col;
            for (int deltaRow = -1; deltaRow <= 1; deltaRow++)
            {
                for (int deltaCol = -1; deltaCol <= 1; deltaCol++)
                {
                    if(!(deltaRow == 0 && deltaCol == 0) && CanBeSwitched(i_Row, i_Col, deltaRow, deltaCol, i_Color, ref i_Board))
                    {
                        row = i_Row + deltaRow;
                        col = i_Col + deltaCol;
                        while (!i_Board[row, col].Equals(i_Color) && i_Board[row, col] != '0')
                        {
                            i_Board[row, col] = i_Color;
                            row += deltaRow;
                            col += deltaCol;
                        }
                    }
                }
            }
        }
    }
}