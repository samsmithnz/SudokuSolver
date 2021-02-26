using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Core
{
    public class InnerSquare
    {
        //Assumed format of:
        //123
        //456
        //789
        public Square[] squares = new Square[9];

        public InnerSquare(string row1, string row2, string row3)
        {
            int i = 0;
            foreach (char item in row1)
            {
                squares[i] = new Square(item);
                i++;
            }
            i = 3;
            foreach (char item in row2)
            {
                squares[i] = new Square(item);
                i++;
            }
            i = 6;
            foreach (char item in row2)
            {
                squares[i] = new Square(item);
                i++;
            }
        }
    }
}
