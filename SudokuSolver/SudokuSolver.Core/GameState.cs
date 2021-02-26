using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Core
{
    public class GameState
    {
        public int UnsolvedSquares;
        //public InnerSquare TopLeft;
        //public InnerSquare TopMiddle;
        //public InnerSquare TopRight;
        //public InnerSquare CenterLeft;
        //public InnerSquare CenterMiddle;
        //public InnerSquare CenterRight;
        //public InnerSquare BottomLeft;
        //public InnerSquare BottomMiddle;
        //public InnerSquare BottomRight;

        //public int[] Squares;
        //public int[] Row;
        //public Row[] Rows;
        public string Game;
        public string GameBoard;

        public void LoadGame(string game)
        {
            Game = game;
            StringBuilder sb = new StringBuilder();
            foreach (string line in game.Split(Environment.NewLine))
            {
                if (line.Trim().StartsWith("#")==false)
                {
                    sb.Append(line);
                    sb.Append(Environment.NewLine);
                }
            }
            GameBoard = sb.ToString();
            UnsolvedSquares = GameBoard.Split('.').Length - 1;
        }

        public string OutputState()
        {
            return GameBoard;
        }

        //public void CheckSquare(int i)
        //{

        //}
    }
}