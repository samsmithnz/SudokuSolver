using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace SudokuSolver.Core
{
    public class Rules
    {
        //Look to solve square groups (3x3 sections), by eliminating square group options
        public static RuleResult SquareGroupEliminationRule(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
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

        //A Lone Single is when a cell has only one pencil mark left.
        public static RuleResult LoneSingleEliminationRule(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
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

        //The definition of a hidden single is when a pencil mark is the only one of its kind in an entire row, column, or block.
        //Similar to a lone single, but more work
        public static RuleResult HiddenSingleEliminationRule(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
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

        //The phrase refers to pencil marks — specifically, when two cells in the same house have the exact same two pencil marks. 
        //For example, if two cells in the same block/row/column have pencil marks of 2 or 3.
        //Therefore the other cells in the block/row/column cannot be 2 or 3.
        public static RuleResult NakedPairsEliminationRule(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
        {
            int squaresSolved = 0;

            ////Check each row
            //for (int y = 0; y < 9; y++)
            //{
            //    //Check each number
            //    for (int i = 1; i <= 9; i++)
            //    {
            //        int numberFrequency = 0;
            //        //Check each column
            //        for (int x = 0; x < 9; x++)
            //        {
            //            if (gameBoardPossibilities[x, y].Contains(i) == true | gameBoard[x, y] == i)
            //            {
            //                numberFrequency++;
            //            }
            //        }
            //        //If there is only one instance of a number, solve it
            //        if (numberFrequency == 1)
            //        {
            //            for (int x = 0; x < 9; x++)
            //            {
            //                if (gameBoardPossibilities[x, y].Contains(i) == true && gameBoard[x, y] == 0)
            //                {
            //                    //remove remaining items from the hashset.
            //                    Debug.WriteLine("Solving square at (" + x + ", " + y + ") from possibility at(" + x + ", " + y + "):" + string.Join(",", gameBoardPossibilities[x, y]) + " using number: " + i.ToString() + "(Current value is " + gameBoard[x, y] + ")");
            //                    gameBoard = SetGameBoard(gameBoard, x, y, i);
            //                    gameBoardPossibilities[x, y] = new HashSet<int>();
            //                    squaresSolved++;
            //                    gameBoardPossibilities = UpdateRowPossibilities(gameBoardPossibilities, y, i);
            //                    gameBoardPossibilities = UpdateColumnPossibilities(gameBoardPossibilities, x, i);
            //                    // return new RuleResult(squaresSolved, gameBoard, gameBoardPossibilities);
            //                }
            //            }
            //        }
            //    }
            //}

            //Check each column
            for (int x = 0; x < 9; x++)
            {
                List<KeyValuePair<Point, HashSet<int>>> nakedPair = new List<KeyValuePair<Point, HashSet<int>>>();

                //Check each column
                for (int y = 0; y < 9; y++)
                {
                    //If there is only one instance of a number, solve it
                    if (gameBoardPossibilities[x, y].Count == 2)
                    {
                        nakedPair.Add(new KeyValuePair<Point, HashSet<int>>(new Point(x, y), gameBoardPossibilities[x, y]));
                    }
                }

                foreach (KeyValuePair<Point, HashSet<int>> item in nakedPair)
                {
                    int number1 = item.Value.First();
                    int number2 = Utility.NthElement(item.Value, 2); //get the second item (not zero based)         
                    for (int y2 = 0; y2 < 9; y2++)
                    {
                        if (y2 != item.Key.Y)
                        {
                            Point point1 = item.Key;
                            if (item.Value.SetEquals(gameBoardPossibilities[x, y2]))
                            {
                                Point point2 = new Point(x, y2);
                                //Loop back through the column, removing all numbers not at the two points
                                for (int y3 = 0; y3 < 9; y3++)
                                {
                                    if (new Point(x, y3) != point1 & new Point(x, y3) != point2)
                                    {
                                        gameBoardPossibilities[x, y3].Remove(number1);
                                        gameBoardPossibilities[x, y3].Remove(number2);
                                    }
                                }
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

        //Looks at a specific row possibilities 
        private static HashSet<int>[,] UpdateRowPossibilities(HashSet<int>[,] gameBoardPossibilities, int y, int number)
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

        //Looks at a specific column possibilities 
        private static HashSet<int>[,] UpdateColumnPossibilities(HashSet<int>[,] gameBoardPossibilities, int x, int number)
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

        private static int[,] SetGameBoard(int[,] gameBoard, int x, int y, int i)
        {
            Debug.WriteLine("Solving square at (" + x + ", " + y + ") using number: " + i.ToString());
            gameBoard[x, y] = i;
            return gameBoard;
        }
    }
}
