//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SudokuSolver.Core
//{
//    public static class BruteStrengthRules
//    {
//        public static RuleResult BruteStrengthRule(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities, int squaresUnsolved)
//        {
//            int squaresSolved = 0;


//            do
//            {
//                int squaresSolvedCircuitBreaker = squaresSolved;
//                List<KeyValuePair<Point, int>> solveList = new List<KeyValuePair<Point, int>>();

//                //do work
//                //1. Try putting in a random number.
//                bool breaking = false;
//                for (int x = 0; x < 9; x++)
//                {
//                    for (int y = 0; y < 9; y++)
//                    {
//                        if (gameBoardPossibilities[x, y].Count > 0)
//                        {
//                            gameBoardPossibilities[x, y].Remove(gameBoardPossibilities[x, y].First());
//                            breaking = true;
//                        }
//                        if (breaking == true)
//                        {
//                            break;
//                        }
//                    }
//                    if (breaking == true)
//                    {
//                        break;
//                    }
//                }

//                //2. Process the rules
                

//                //3. Was it successful? Break out. If not, loop back to 1 and try another number

//                //Circuit breaker so we don't loop forever
//                if (squaresSolved == squaresSolvedCircuitBreaker)
//                {
//                    break;
//                }
//            } while (squaresUnsolved > 0);

//            return new RuleResult(squaresSolved, gameBoard, gameBoardPossibilities);
//        }
//    }
//}
