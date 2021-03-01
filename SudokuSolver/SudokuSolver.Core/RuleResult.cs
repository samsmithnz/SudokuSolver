using System.Collections.Generic;

namespace SudokuSolver.Core
{
    public class RuleResult
    {
        public int SquaresSolved;
        public int[,] GameBoard;
        public HashSet<int>[,] GameBoardPossibilities;

        public RuleResult(int squaresSolved, int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
        {
            SquaresSolved = squaresSolved;
            GameBoard = gameBoard;
            GameBoardPossibilities = gameBoardPossibilities;
        }
    }
}
