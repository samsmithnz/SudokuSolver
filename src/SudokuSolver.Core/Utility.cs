using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Core
{
    public class Utility
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
        public static int GetNumber(string number)
        {
            int itemNumber = 0;
            if (number != "0")
            {
                itemNumber = int.Parse(number.ToString());
            }
            return itemNumber;
        }

        public static int NthElement(HashSet<int> mySet, int n)
        {
            List<int> items = mySet.ToList<int>();
            if (items.Count > n)
            {
                return items[n - 1];
            }
            else
            {
                return 0;
            }
        }
    }
}
