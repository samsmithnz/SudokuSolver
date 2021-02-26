using System;
using System.Text;

namespace SudokuSolver.Core
{
    public class Square
    {
        //Assumed format of:
        //123
        //456
        //789
        public int[] PossibleSquares = new int[9];
        public int? CurrentSquare = null;

        public Square(int? currentSquare = null)
        {
            if (currentSquare != null)
            {
                CurrentSquare = currentSquare;
                for (int i = 0; i < 9; i++)
                {
                    PossibleSquares[i] = 0;
                }
            }
            else
            {
                CurrentSquare = null;
                for (int i = 0; i < 9; i++)
                {
                    PossibleSquares[i] = i + 1;
                }
            }
        }

        //Output the current state. For example, only 3 and 9 are possibilities:
        //..3
        //...
        //..9
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 9; i++)
            {
                if (PossibleSquares[i] == 0)
                {
                    sb.Append(".");
                }
                else
                {
                    sb.Append(PossibleSquares[i]);
                }
                //Add a new line if it's the end of the 1st or 2nd line. (3rd line doesn't need a new line as it's the end of the square)
                if (i == 2 | i == 5)
                {
                    sb.Append(Environment.NewLine);
                }
            }
            return sb.ToString();
        }
    }
}
