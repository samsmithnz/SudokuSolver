using System;

namespace SudokuSolver.Core
{
    public class Rules
    {
        public RuleResult InnerSquareEliminationRule(SquareGroup group)
        {
            int squaresSolved = 0;
            int i = 0;

            //First mark all of the possible numbers in available squares, within the square group
            foreach (int item in group.SolvedSquares)
            {
                i = 0;
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        if (group.Squares[i].CurrentSquare == 0)
                        {
                            group.Squares[i].EliminatePossibleSquare(item);
                        }
                        i++;
                    }
                }
            }

            //Then run a simple elimination, to see if the item can be solved
            i = 0;
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (group.Squares[i].CurrentSquare == 0 && group.Squares[i].PossibleSquareCount == 1)
                    {
                        squaresSolved++;
                        group.Squares[i].CurrentSquare = group.Squares[i].PossibleSquaresFiltered[0];
                    }
                    i++;
                }
            }

            return new RuleResult(squaresSolved, group);
        }
    }

    public class RuleResult
    {
        public int SquaresSolved;
        public SquareGroup Group;

        public RuleResult(int squaresSolved, SquareGroup group)
        {
            SquaresSolved = squaresSolved;
            Group = group;
        }
    }
}
