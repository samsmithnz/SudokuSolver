using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Core
{
    public class Rules
    {
        //public RuleResult SquareGroupEliminationRuleMethod1(SquareGroup group)
        //{
        //    int squaresSolved = 0;
        //    int i = 0;

        //    //First mark all of the possible numbers in available squares, within the square group
        //    foreach (int item in group.SolvedSquares)
        //    {
        //        i = 0;
        //        for (int y = 0; y < 3; y++)
        //        {
        //            for (int x = 0; x < 3; x++)
        //            {
        //                if (group.Squares[i].CurrentSquare == 0)
        //                {
        //                    group.Squares[i].EliminatePossibleSquare(item);
        //                }
        //                i++;
        //            }
        //        }
        //    }

        //    //Then run a simple elimination, to see if the item can be solved
        //    i = 0;
        //    for (int y = 0; y < 3; y++)
        //    {
        //        for (int x = 0; x < 3; x++)
        //        {
        //            if (group.Squares[i].CurrentSquare == 0 && group.Squares[i].PossibleSquareCount == 1)
        //            {
        //                squaresSolved++;
        //                group.Squares[i].CurrentSquare = group.Squares[i].PossibleSquaresFiltered[0];
        //            }
        //            i++;
        //        }
        //    }

        //    return new RuleResult(squaresSolved, group, null);
        //}

        public RuleResult SquareGroupEliminationRule(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
        {
            int squaresSolved = 0;

            //First mark all of the possible numbers in available squares, within the square group
            //Then run a simple elimination, to see if the item can be solved

            HashSet<int>[,] squareGroupPossibilities = new HashSet<int>[3, 3];

            //Get each square group
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    squareGroupPossibilities[x, y] = new HashSet<int>(Utility.SquareSet);
                    //Remove all completed squares found in the square group
                    for (int y2 = 0; y2 < 3; y2++)
                    {
                        for (int x2 = 0; x2 < 3; x2++)
                        {
                            squareGroupPossibilities[x, y].Remove(gameBoard[(y * 3) + y2, (x * 3) + x2]);
                        }
                    }
                }
            }

            //do a final loop through, looking for any squares with just one possibility
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    //If there is only one possibility, set it in the unsolved spot in the square group
                    if (squareGroupPossibilities[x, y].Count == 1)
                    {
                        for (int y2 = 0; y2 < 3; y2++)
                        {
                            for (int x2 = 0; x2 < 3; x2++)
                            {
                                int numberToCheck = gameBoard[(y * 3) + y2, (x * 3) + x2];
                                if (numberToCheck == 0)
                                {
                                    gameBoard[(y * 3) + y2, (x * 3) + x2] = squareGroupPossibilities[x, y].First();
                                    ////remove the last item from the hashset.
                                    squareGroupPossibilities[x, y].Remove(numberToCheck);
                                    squaresSolved++;
                                    break;
                                }
                            }
                        }
                    }
                }
            }


            return new RuleResult(squaresSolved, gameBoard, gameBoardPossibilities);
        }

        public RuleResult RowEliminationRule(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
        {
            int squaresSolved = 0;

            //First mark all of the possible numbers in available squares, within the row
            //Then run a simple elimination, to see if the item can be solved

            //Get each row
            for (int y = 0; y < 9; y++)
            {
                //Get each column
                for (int x = 0; x < 9; x++)
                {
                    if (gameBoard[x, y] != 0)
                    {
                        //look to see if the square is already solved. If so - remove possibilities from the rest of the row
                        for (int x2 = 0; x2 < 9; x2++)
                        {
                            gameBoardPossibilities[x2, y].Remove(gameBoard[x, y]);
                        }
                    }
                }
            }

            //do a final loop through, looking for any squares with just one possibility
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    //If there is only one possibility, set it
                    if (gameBoardPossibilities[x, y].Count == 1)
                    {
                        gameBoard[x, y] = gameBoardPossibilities[x, y].First();
                        //remove the last item from the hashset.
                        gameBoardPossibilities[x, y].Remove(gameBoard[x, y]);
                        squaresSolved++;
                    }
                }
            }

            return new RuleResult(squaresSolved, gameBoard, gameBoardPossibilities);
        }

        public RuleResult ColumnEliminationRule(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
        {
            int squaresSolved = 0;

            //First mark all of the possible numbers in available squares, within the column
            //Then run a simple elimination, to see if the item can be solved

            //Get each column
            for (int x = 0; x < 9; x++)
            {
                //Get each row
                for (int y = 0; y < 9; y++)
                {
                    //look to see if the square is already solved. 
                    if (gameBoard[x, y] != 0)
                    {
                        //look to see if the square is already solved. If so - remove possibilities from the rest of the column
                        for (int y2 = 0; y2 < 9; y2++)
                        {
                            gameBoardPossibilities[x, y2].Remove(gameBoard[x, y]);
                        }
                    }
                }
            }

            //do a final loop through, looking for any squares with just one possibility
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    //If there is only one possibility, set it
                    if (gameBoardPossibilities[x, y].Count == 1)
                    {
                        gameBoard[x, y] = gameBoardPossibilities[x, y].First();
                        //remove the last item from the hashset.
                        gameBoardPossibilities[x, y].Remove(gameBoard[x, y]);
                        squaresSolved++;
                    }
                }
            }

            return new RuleResult(squaresSolved, gameBoard, gameBoardPossibilities);
        }

        //TODO: Add a rule that adds up the numbers and calculates what is missing (1 to 9 is 45)
        //TODO: Add a rule to look at the columns and rows
    }

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
