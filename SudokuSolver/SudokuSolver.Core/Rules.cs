using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SudokuSolver.Core
{
    public class Rules
    {

        //Look to solve square groups (3x3 sections), by eliminating square group options
        public static RuleResult SquareGroupEliminationRule(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
        {
            int squaresSolved = 0;

            //First mark all of the possible numbers in available squares, within the square group
            //Then run a simple elimination, to see if the item can be solved

            //Get each row
            for (int y = 0; y < 9; y++)
            {
                //Get each column
                for (int x = 0; x < 9; x++)
                {
                    if (gameBoard[x, y] != 0)
                    {
                        //Get the top left of the square group
                        int xSquare = (int)(x / 3f);
                        int ySquare = (int)(y / 3f);
                        //Loop through the square group
                        for (int y2 = 0; y2 < 3; y2++)
                        {
                            for (int x2 = 0; x2 < 3; x2++)
                            {
                                gameBoardPossibilities[(xSquare * 3) + x2, (ySquare * 3) + y2].Remove(gameBoard[x, y]);
                            }
                        }
                    }
                }
            }

            //do a final loop through, looking for any squares with just one possibility
            RuleResult ruleResult = FinalOptionEliminationRule(gameBoard, gameBoardPossibilities);
            if (ruleResult != null)
            {
                squaresSolved += ruleResult.SquaresSolved;
                gameBoard = ruleResult.GameBoard;
                gameBoardPossibilities = ruleResult.GameBoardPossibilities;
            }
          
            return new RuleResult(squaresSolved, gameBoard, gameBoardPossibilities);
        }

        //Look to solve rows by eliminating row options
        public static RuleResult RowEliminationRule(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
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
                            if (gameBoardPossibilities[x2, y].Contains(gameBoard[x, y]) == true)
                            {
                                gameBoardPossibilities[x2, y].Remove(gameBoard[x, y]);
                            }
                        }
                    }
                }
            }

            //do a final loop through, looking for any squares with just one possibility
            RuleResult ruleResult = FinalOptionEliminationRule(gameBoard, gameBoardPossibilities);
            if (ruleResult != null)
            {
                squaresSolved += ruleResult.SquaresSolved;
                gameBoard = ruleResult.GameBoard;
                gameBoardPossibilities = ruleResult.GameBoardPossibilities;
            }

            return new RuleResult(squaresSolved, gameBoard, gameBoardPossibilities);
        }

        //Look to solve columns by eliminating column options
        public static RuleResult ColumnEliminationRule(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
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
                            if (gameBoardPossibilities[x, y2].Contains(gameBoard[x, y]) == true)
                            {
                                if (x == 2 && y2 == 0)
                                {
                                    Debug.WriteLine("Removing col: " + gameBoard[x, y] + " at (" + x + ", " + y + ") from possibility from (2,0):" + string.Join(",", gameBoardPossibilities[2, 0]));
                                }
                                gameBoardPossibilities[x, y2].Remove(gameBoard[x, y]);
                            }
                        }
                    }
                }
            }

            //do a final loop through, looking for any squares with just one possibility
            RuleResult ruleResult = FinalOptionEliminationRule(gameBoard, gameBoardPossibilities);
            if (ruleResult != null)
            {
                squaresSolved += ruleResult.SquaresSolved;
                gameBoard = ruleResult.GameBoard;
                gameBoardPossibilities = ruleResult.GameBoardPossibilities;
            }

            return new RuleResult(squaresSolved, gameBoard, gameBoardPossibilities);
        }

        private static RuleResult FinalOptionEliminationRule(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
        {
            int squaresSolved = 0;
            //do a final loop through, looking for any squares with just one possibility
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    //If there is only one possibility, set it
                    if (gameBoardPossibilities[x, y].Count == 1 && gameBoard[x, y] == 0)
                    {
                        gameBoard[x, y] = gameBoardPossibilities[x, y].First();
                        //remove the last item from the hashset.
                        Debug.WriteLine("Solving square: " + gameBoard[x, y] + " at (" + x + ", " + y + ") from possibility at (" + x + ", " + y + "):" + string.Join(",", gameBoardPossibilities[x, y]));
                        gameBoardPossibilities[x, y].Remove(gameBoard[x, y]);
                        squaresSolved++;
                    }
                }
            }


            return new RuleResult(squaresSolved, gameBoard, gameBoardPossibilities);
        }


        //TODO: Add a rule that adds up the numbers and calculates what is missing (1 to 9 is 45)

    }

}
