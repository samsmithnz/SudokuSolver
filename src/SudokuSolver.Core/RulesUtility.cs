using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSolver.Core
{
    public class RulesUtility
    {
        public static readonly int[] SquareSet = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public static string TrimNewLines(string input)
        {
            //Trim off any leading or trailing new lines 
            input = input.TrimStart('\r', '\n');
            input = input.TrimEnd('\r', '\n');
            return input;
        }

        //Convert the string number to a integer
        public static int ConvertStringToNumber(string number)
        {
            int itemNumber = 0;
            if (number != "0")
            {
                _ = int.TryParse(number, out itemNumber);
            }
            return itemNumber;
        }

        //Note that n is NOT zero based - hence the -1 on line 37
        public static int GetNthElement(HashSet<int> mySet, int n)
        {
            List<int> items = mySet.ToList<int>();
            if (items.Count >= n)
            {
                return items[n - 1];
            }
            else
            {
                return 0;
            }
        }

        public static string ConvertGameBoardToString(int[,] gameBoard)
        {
            StringBuilder sb = new StringBuilder();
            //Load the rows into a 2d array
            for (int y = 0; y < gameBoard.GetLength(1); y++)
            {
                for (int x = 0; x < gameBoard.GetLength(0); x++)
                {
                    sb.Append(gameBoard[x, y]);
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
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
                        //Debug.WriteLine(number);
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
                        //Debug.WriteLine(number);
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

        public static HashSet<int>[,] ExtractSquareGroupFromGamePossibilities(HashSet<int>[,] gameBoardPossibilities, int squareGroupX, int squareGroupY)
        {
            HashSet<int>[,] result = new HashSet<int>[3, 3];

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
                        HashSet<int> number = gameBoardPossibilities[x, y];
                        result[x2, y2] = number;
                        //Debug.WriteLine(number);
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

        public static HashSet<int>[,] InsertSquareGroupIntoGamePossibilities(HashSet<int>[,] gameBoardPossibilities, HashSet<int>[,] squareBoard, int squareGroupX, int squareGroupY)
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
                        HashSet<int> number = squareBoard[x2, y2];
                        gameBoardPossibilities[x, y] = number;
                        //Debug.WriteLine(number);
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

            return gameBoardPossibilities;
        }

        //Confirms that the puzzle has been solved correctly
        public static bool CrossCheckResult(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
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

            //Finally check that the solves are correct
            int numberOfUnsolvedSquares = 0;
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (gameBoard[x, y] == 0)
                    {
                        numberOfUnsolvedSquares++;
                    }
                }
            }
            if (numberOfUnsolvedSquares > 0 & CountRemainingPossibilities(gameBoard, gameBoardPossibilities) == 0)
            {
                return false;
            }
            return true;
        }

        public static int CountRemainingPossibilities(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
        {
            int result = 0;
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    //Break out if we find any unsolved squares with no possibilities
                    if (gameBoard[x, y] == 0 & gameBoardPossibilities[x, y].Count == 0)
                    {
                        return 0;
                    }
                    result += gameBoardPossibilities[x, y].Count;
                }
            }
            return result;
        }

        public static HashSet<int>[,] CopyHashset(HashSet<int>[,] hashSet)
        {
            HashSet<int>[,] newHashSet = new HashSet<int>[9, 9];

            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    newHashSet[x, y] = new HashSet<int>();
                    for (int i = 1; i <= hashSet[x, y].Count; i++)
                    {
                        int number = RulesUtility.GetNthElement(hashSet[x, y], i);
                        newHashSet[x, y].Add(number);
                    }
                }
            }
            return newHashSet;
        }
    }
}
