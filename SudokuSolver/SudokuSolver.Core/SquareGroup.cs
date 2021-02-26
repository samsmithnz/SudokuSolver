using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Core
{
    public class SquareGroup
    {
        //Assumed format of:
        //123
        //456
        //789
        public Square[] squares = new Square[9];

        public SquareGroup(string row1, string row2, string row3)
        {
            int i = 0;
            foreach (char item in row1)
            {
                squares[i] = new Square(GetNumber(item.ToString()));
                i++;
            }
            i = 3;
            foreach (char item in row2)
            {
                squares[i] = new Square(GetNumber(item.ToString()));
                i++;
            }
            i = 6;
            foreach (char item in row3)
            {
                squares[i] = new Square(GetNumber(item.ToString()));
                i++;
            }
        }

        //Convert the string number to a integer
        private int GetNumber(string number)
        {
            int itemNumber = 0;
            if (number != "0")
            {
                itemNumber = int.Parse(number.ToString());
            }
            return itemNumber;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (Square item in squares)
            {
                i++;
                if (item.CurrentSquare == 0)
                {
                    sb.Append(".");
                }
                else
                {
                    sb.Append(item.CurrentSquare.ToString());
                }
                if (i % 3 == 0 && i < squares.Length )
                {
                    sb.Append(Environment.NewLine);
                }
            }
            return sb.ToString();
        }
    }
}
