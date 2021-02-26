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

        public string Game;
        public string GameBoard;
        public SquareGroup[] SquareGroups;

        public void LoadGame(string game)
        {
            Game = game;
            StringBuilder sb = new StringBuilder();
            foreach (string line in game.Split(Environment.NewLine))
            {
                if (line.Trim().StartsWith("#") == false)
                {
                    sb.Append(line);
                    sb.Append(Environment.NewLine);
                }
            }
            GameBoard = Utility.TrimNewLines(sb.ToString());
            UnsolvedSquares = GameBoard.Split('.').Length - 1;

            //Replace the .'s with 0's
            string processedGameBoard = GameBoard.Replace(".", "0");
            string[] rows = processedGameBoard.Split(Environment.NewLine);
            int width = rows[0].Length / 3;
            int height = rows.Length / 3;
            SquareGroups = new SquareGroup[width * height];

            string row1;
            string row2;
            string row3;
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    row1 = rows[(row * 3) + 0].Substring((col * 3), 3);
                    row2 = rows[(row * 3) + 1].Substring((col * 3), 3);
                    row3 = rows[(row * 3) + 2].Substring((col * 3), 3);
                    SquareGroups[row + col] = new SquareGroup(row1, row2, row3);
                }
            }

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