using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Core
{
    public static class BruteStrengthRules
    {
        public static RuleResult BruteStrengthRule(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
        {
            int squaresSolved = 0;

            return new RuleResult(squaresSolved, gameBoard, gameBoardPossibilities);
        }
    }
}
