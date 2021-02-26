using System;

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
    }
}
