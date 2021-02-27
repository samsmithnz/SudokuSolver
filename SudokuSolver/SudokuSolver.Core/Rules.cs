using System;

namespace SudokuSolver.Core
{
    public class Rules
    {
        public (int, SquareGroup) InnerSquareEliminationRule(SquareGroup group)
        {
            int squaresSolved = 0;

            //First mark all of the possible numbers in available squares, within the square group
            foreach (int item in group.SolvedSquares)
            {
                for (int y = 0; y <= 3; y++)
                {
                    for (int x = 0; x <= 3; x++)
                    {
                        if (group.Squares[y + x].CurrentSquare == 0)
                        {
                            group.Squares[y + x].EliminatePossibleSquare(item);
                        }
                    }
                }
            }

            //Then run a simple elimination, to see if the item can be solved
            for (int y = 0; y <= 3; y++)
            {
                for (int x = 0; x <= 3; x++)
                {
                    if (group.Squares[y + x].CurrentSquare == 0 && group.Squares[y + x].PossibleSquareCount == 1)
                    {
                        squaresSolved++;
                        group.Squares[y + x].CurrentSquare = group.Squares[y + x].PossibleSquaresFiltered[0];
                    }
                }
            }

            return new(squaresSolved, group);
        }
    }
}
