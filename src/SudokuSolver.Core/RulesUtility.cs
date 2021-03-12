using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSolver.Core
{
    public class RulesUtility
    {
        public static readonly int[] SquareSet = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public static string TrimNewLines(string input)
        {
            //Trim off any leading or trailing new lines 
            input = input.TrimStart('\r', '\n');
            input = input.TrimEnd('\r', '\n');
            return input;
        }

        //Convert the string number to a integer
        public static int ConvertStringToNumber(string number)
        {
            int itemNumber = 0;
            if (number != "0")
            {
                _ = int.TryParse(number, out itemNumber);
            }
            return itemNumber;
        }

        //Note that n is NOT zero based - hence the -1 on line 35
        public static int NthElement(HashSet<int> mySet, int n)
        {
            List<int> items = mySet.ToList<int>();
            if (items.Count >= n)
            {
                return items[n - 1];
            }
            else
            {
                return 0;
            }
        }

        public static string ConvertGameBoardToString(int[,] gameBoard)
        {
            StringBuilder sb = new StringBuilder();
            //Load the rows into a 2d array
            for (int y = 0; y < gameBoard.GetLength(1); y++)
            {
                for (int x = 0; x < gameBoard.GetLength(0); x++)
                {
                    sb.Append(gameBoard[x, y]);
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}
