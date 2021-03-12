using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace SudokuSolver.Core
{
    public class BasicRules
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
                        Debug.WriteLine("Solving square (" + x + ", " + y + ") from possibility (" + x + ", " + y + "), with options [" + string.Join(",", gameBoardPossibilities[x, y]) + "], using number: " + number.ToString() + " (Current value is " + gameBoard[x, y] + ")");
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
                                Debug.WriteLine("Solving square (" + x + ", " + y + ") from possibility (" + x + ", " + y + "), with options [" + string.Join(",", gameBoardPossibilities[x, y]) + "], using number: " + i.ToString() + " (Current value is " + gameBoard[x, y] + ")");
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
                                Debug.WriteLine("Solving square (" + x + ", " + y + ") from possibility (" + x + ", " + y + "), with options [" + string.Join(",", gameBoardPossibilities[x, y]) + "], using number: " + i.ToString() + " (Current value is " + gameBoard[x, y] + ")");
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
            int[] checker;

            //Check that each row only contains a number once
            for (int y = 0; y < 9; y++)
            {
                checker = new int[10];
                for (int x = 0; x < 9; x++)
                {
                    checker[gameBoard[x, y]]++;
                }
                for (int j = 1; j <= 9; j++)
                {
                    if (checker[j] > 1)
                    {
                        return false;
                    }
                }
            }

            //Check that each column only contains a number once
            for (int x = 0; x < 9; x++)
            {
                checker = new int[10];
                for (int y = 0; y < 9; y++)
                {
                    checker[gameBoard[x, y]]++;
                }
                for (int j = 1; j <= 9; j++)
                {
                    if (checker[j] > 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static int[,] ExtractSquareGroupFromGameBoard(int[,] gameBoard, int squareGroupX, int squareGroupY)
        {
            int[,] result = new int[3, 3];

            int xLow = (squareGroupX * 3);
            int xHigh = ((squareGroupX + 1) * 3) - 1;
            int yLow = (squareGroupY * 3);
            int yHigh = ((squareGroupY + 1) * 3) - 1;
            int x2 = 0;
            int y2 = 0;
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (x >= xLow & x <= xHigh & y >= yLow & y <= yHigh)
                    {
                        int number = gameBoard[x, y];
                        result[x2, y2] = number;
                        Debug.WriteLine(number);
                    }
                    x2++;
                    if (x2 >= 3)
                    {
                        x2 = 0;
                    }
                }
                y2++;
                if (y2 >= 3)
                {
                    y2 = 0;
                }
            }

            return result;
        }

        public static int[,] InsertSquareGroupIntoGameBoard(int[,] gameBoard, int[,] squareBoard, int squareGroupX, int squareGroupY)
        {
            int xLow = (squareGroupX * 3);
            int xHigh = ((squareGroupX + 1) * 3) - 1;
            int yLow = (squareGroupY * 3);
            int yHigh = ((squareGroupY + 1) * 3) - 1;
            int x2 = 0;
            int y2 = 0;
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (x >= xLow & x <= xHigh & y >= yLow & y <= yHigh)
                    {
                        int number = squareBoard[x2, y2];
                        gameBoard[x, y] = number;
                        Debug.WriteLine(number);
                    }
                    x2++;
                    if (x2 >= 3)
                    {
                        x2 = 0;
                    }
                }
                y2++;
                if (y2 >= 3)
                {
                    y2 = 0;
                }
            }

            return gameBoard;
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
            Debug.WriteLine("Solving square (" + x + ", " + y + ") using number: " + i.ToString());
            gameBoard[x, y] = i;
            return gameBoard;
        }
    }
}
