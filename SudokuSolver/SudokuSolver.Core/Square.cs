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
        public int CurrentSquare = 0;
        public int PossibleSquareCount = 0;

        public Square(int currentSquare = 0)
        {
            CurrentSquare = currentSquare;
            if (currentSquare > 0)
            {
                //If the current square has a solution, there are no possible squares
                PossibleSquareCount = 0;
                for (int i = 0; i < 9; i++)
                {
                    PossibleSquares[i] = 0;
                }
            }
            else
            {
                //Otherwise, no solution, initialize all of the possible squares
                PossibleSquareCount = 9;
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
                //If the possible square is 0, this option has been ruled out
                if (PossibleSquares[i] == 0)
                {
                    sb.Append(".");
                }
                else
                {
                    //Otherwise it's still a possibility
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
