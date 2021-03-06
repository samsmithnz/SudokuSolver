using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SudokuSolver.Core
{
    public class Rules
    {
        public static HashSet<int>[,] UpdateRowPossibilities(HashSet<int>[,] gameBoardPossibilities, int y, int number)
        {
            //Check each column in row
            for (int x = 0; x < 9; x++)
            {
                if (gameBoardPossibilities[x, y].Contains(number) == true)
                {
                    gameBoardPossibilities[x, y].Remove(number);
                }
            }

            return gameBoardPossibilities;
        }

        public static HashSet<int>[,] UpdateColumnPossibilities(HashSet<int>[,] gameBoardPossibilities, int x, int number)
        {
            //Check each row in column
            for (int y = 0; y < 9; y++)
            {
                if (gameBoardPossibilities[x, y].Contains(number) == true)
                {
                    gameBoardPossibilities[x, y].Remove(number);
                }
            }

            return gameBoardPossibilities;
        }

        //Look to solve square groups (3x3 sections), by eliminating square group options
        public static RuleResult UpdateSquareGroupPossibilities(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
        {
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

            return new RuleResult(0, gameBoard, gameBoardPossibilities);
        }

        //Look to solve rows by eliminating row options
        public static RuleResult RowEliminationRule(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
        {
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

            return new RuleResult(0, gameBoard, gameBoardPossibilities);
        }

        //Look to solve columns by eliminating column options
        public static RuleResult ColumnEliminationRule(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
        {
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
                                //if (x == 2 && y2 == 0)
                                //{
                                //    Debug.WriteLine("Removing col: " + gameBoard[x, y] + " at (" + x + ", " + y + ") from possibility from (2,0):" + string.Join(",", gameBoardPossibilities[2, 0]));
                                //}
                                gameBoardPossibilities[x, y2].Remove(gameBoard[x, y]);
                            }
                        }
                    }
                }
            }

            return new RuleResult(0, gameBoard, gameBoardPossibilities);
        }

        public static RuleResult FinalOptionEliminationRule(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
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
                        //remove the last item from the hashset.
                        int number = gameBoardPossibilities[x, y].First();
                        Debug.WriteLine("Solving square at (" + x + ", " + y + ") from possibility at(" + x + ", " + y + "):" + string.Join(",", gameBoardPossibilities[x, y]) + " using number: " + number.ToString() + "(Current value is " + gameBoard[x, y] + ")");
                        gameBoard = SetGameBoard(gameBoard, x, y, number);
                        gameBoardPossibilities[x, y].Remove(gameBoard[x, y]);
                        squaresSolved++;
                        gameBoardPossibilities = UpdateRowPossibilities(gameBoardPossibilities, y, number);
                        gameBoardPossibilities = UpdateColumnPossibilities(gameBoardPossibilities, x, number);
                        return new RuleResult(squaresSolved, gameBoard, gameBoardPossibilities);
                    }
                }
            }


            return new RuleResult(squaresSolved, gameBoard, gameBoardPossibilities);
        }

        //Check possibilities in a row, to see if there if a number only has one possible option (even though multiple squares appear to be able to go into that square)

        public static RuleResult PossibilitiesEliminationRule(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
        {
            int squaresSolved = 0;

            //Check each row
            for (int y = 0; y < 9; y++)
            {
                //Check each number
                for (int i = 1; i <= 9; i++)
                {
                    int numberFrequency = 0;
                    //Check each column
                    for (int x = 0; x < 9; x++)
                    {
                        if (gameBoardPossibilities[x, y].Contains(i) == true | gameBoard[x, y] == i)
                        {
                            numberFrequency++;
                        }
                    }
                    //If there is only one instance of a number, solve it
                    if (numberFrequency == 1)
                    {
                        for (int x = 0; x < 9; x++)
                        {
                            if (gameBoardPossibilities[x, y].Contains(i) == true && gameBoard[x, y] == 0)
                            {
                                //remove remaining items from the hashset.
                                Debug.WriteLine("Solving square at (" + x + ", " + y + ") from possibility at(" + x + ", " + y + "):" + string.Join(",", gameBoardPossibilities[x, y]) + " using number: " + i.ToString() + "(Current value is " + gameBoard[x, y] + ")");
                                gameBoard = SetGameBoard(gameBoard, x, y, i);
                                gameBoardPossibilities[x, y] = new HashSet<int>();
                                squaresSolved++;
                                gameBoardPossibilities = UpdateRowPossibilities(gameBoardPossibilities, y, i);
                                gameBoardPossibilities = UpdateColumnPossibilities(gameBoardPossibilities, x, i);
                                // return new RuleResult(squaresSolved, gameBoard, gameBoardPossibilities);
                            }
                        }
                    }
                }
            }

            //Check each column
            for (int x = 0; x < 9; x++)
            {
                //Check each number
                for (int i = 1; i <= 9; i++)
                {
                    int numberFrequency = 0;
                    //Check each column
                    for (int y = 0; y < 9; y++)
                    {
                        if (gameBoardPossibilities[x, y].Contains(i) == true | gameBoard[x, y] == i)
                        {
                            numberFrequency++;
                        }
                    }
                    //If there is only one instance of a number, solve it
                    if (numberFrequency == 1)
                    {
                        for (int y = 0; y < 9; y++)
                        {
                            if (gameBoardPossibilities[x, y].Contains(i) == true && gameBoard[x, y] == 0)
                            {
                                //remove remaining items from the hashset.
                                Debug.WriteLine("Solving square at (" + x + ", " + y + ") from possibility at(" + x + ", " + y + "):" + string.Join(",", gameBoardPossibilities[x, y]) + " using number: " + i.ToString() + "(Current value is " + gameBoard[x, y] + ")");
                                gameBoard = SetGameBoard(gameBoard, x, y, i);
                                gameBoardPossibilities[x, y] = new HashSet<int>();
                                squaresSolved++;
                                gameBoardPossibilities = UpdateRowPossibilities(gameBoardPossibilities, y, i);
                                gameBoardPossibilities = UpdateColumnPossibilities(gameBoardPossibilities, x, i);
                                // return new RuleResult(squaresSolved, gameBoard, gameBoardPossibilities);
                            }
                        }
                    }
                }
            }

            return new RuleResult(squaresSolved, gameBoard, gameBoardPossibilities);
        }


        //Confirms that the puzzle has been solved correctly
        public static bool CrossCheckResultRule(int[,] gameBoard)
        {
            //Check at each row to ensure it adds up to 45 and is complete.
            for (int y = 0; y < 9; y++)
            {
                int rowSum = 0;
                int unsolvedSquares = 0;
                for (int x = 0; x < 9; x++)
                {
                    rowSum += gameBoard[x, y];
                    if (gameBoard[x, y] == 0)
                    {
                        unsolvedSquares++;
                    }
                }
                if (unsolvedSquares == 0 & rowSum != 45)
                {
                    //Note that if the code works, Code coverage should never get to here
                    return false;
                }
            }

            //Check at each column to ensure it adds up to 45 and is complete.
            for (int x = 0; x < 9; x++)
            {
                int rowSum = 0;
                int unsolvedSquares = 0;
                for (int y = 0; y < 9; y++)
                {
                    rowSum += gameBoard[x, y];
                    if (gameBoard[x, y] == 0)
                    {
                        unsolvedSquares++;
                    }
                }
                if (unsolvedSquares == 0 & rowSum != 45)
                {
                    //Note that if the code works, Code coverage should never get to here
                    return false;
                }
            }
            return true;
        }

        private static int[,] SetGameBoard(int[,] gameBoard, int x, int y, int i)
        {
            Debug.WriteLine("Solving square at (" + x + ", " + y + ") using number: " + i.ToString());
            gameBoard[x, y] = i;
            return gameBoard;
        }
    }
}
