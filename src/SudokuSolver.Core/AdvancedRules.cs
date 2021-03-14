using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace SudokuSolver.Core
{
    public class AdvancedRules
    {
        //The phrase refers to pencil marks — specifically, when two cells in the same house have the exact same two pencil marks. 
        //For example, if two cells in the same block/row/column have pencil marks of 2 or 3.
        //Therefore the other cells in the block/row/column cannot be 2 or 3.
        public static RuleResult NakedPairsEliminationRule(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
        {
            int squaresSolved = 0;
            List<KeyValuePair<Point, HashSet<int>>> nakedPair = new List<KeyValuePair<Point, HashSet<int>>>();

            //TODO: refactor this into a separate function (it helps to keep the variables declared within just this if statement scope
            if (true)
            {
                //Check each row
                for (int y = 0; y < 9; y++)
                {
                    nakedPair = new List<KeyValuePair<Point, HashSet<int>>>();
                    //Check each column
                    for (int x = 0; x < 9; x++)
                    {
                        //If there are only two possibilities, add the item to a hashset.
                        if (gameBoardPossibilities[x, y].Count == 2)
                        {
                            nakedPair.Add(new KeyValuePair<Point, HashSet<int>>(new Point(x, y), gameBoardPossibilities[x, y]));
                        }
                    }
                    //Now loop through the hashsets and check if their are duplicates and hence can remove some possibilities in the row
                    foreach (KeyValuePair<Point, HashSet<int>> item in nakedPair)
                    {
                        //double check there is still a value - as this evolves as we solve the puzzle
                        if (item.Value.Count == 2)
                        {
                            int number1 = RulesUtility.GetNthElement(item.Value, 1); //get the first item (not zero based)         
                            int number2 = RulesUtility.GetNthElement(item.Value, 2); //get the second item (not zero based)         
                            for (int x2 = 0; x2 < 9; x2++)
                            {
                                if (x2 != item.Key.X)
                                {
                                    Point point1 = item.Key;
                                    if (item.Value.SetEquals(gameBoardPossibilities[x2, y]))
                                    {
                                        Point point2 = new Point(x2, y);
                                        //Loop back through the column, removing all numbers not at the two points
                                        for (int x3 = 0; x3 < 9; x3++)
                                        {
                                            if (new Point(x3, y) != point1 & new Point(x3, y) != point2)
                                            {
                                                gameBoardPossibilities[x3, y].Remove(number1);
                                                gameBoardPossibilities[x3, y].Remove(number2);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //TODO: refactor this into a separate function (it helps to keep the variables declared within just this if statement scope
            if (true)
            {
                //Check each column
                for (int x = 0; x < 9; x++)
                {
                    nakedPair = new List<KeyValuePair<Point, HashSet<int>>>();
                    //Check each row
                    for (int y = 0; y < 9; y++)
                    {
                        //If there are only two possibilities, add the item to a hashset.
                        if (gameBoardPossibilities[x, y].Count == 2)
                        {
                            nakedPair.Add(new KeyValuePair<Point, HashSet<int>>(new Point(x, y), gameBoardPossibilities[x, y]));
                        }
                    }
                    //Now loop through the hashsets and check if their are duplicates and hence can remove some possibilities in the column
                    foreach (KeyValuePair<Point, HashSet<int>> item in nakedPair)
                    {
                        //double check there is still a value - as this evolves as we solve the puzzle
                        if (item.Value.Count == 2)
                        {
                            int number1 = RulesUtility.GetNthElement(item.Value, 1); //get the first item (not zero based)    
                            int number2 = RulesUtility.GetNthElement(item.Value, 2); //get the second item (not zero based)         
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
                }
            }

            //TODO: refactor this into a separate function (it helps to keep the variables declared within just this if statement scope
            //square group
            if (true)
            {
                //Get each row 
                for (int ySquare = 0; ySquare < 3; ySquare++)
                {
                    //Get each column
                    for (int xSquare = 0; xSquare < 3; xSquare++)
                    {
                        nakedPair = new List<KeyValuePair<Point, HashSet<int>>>();
                        HashSet<int>[,] gameBoardPossibilitiesSquare = RulesUtility.ExtractSquareGroupFromGamePossibilities(gameBoardPossibilities, xSquare, ySquare);
                        //Loop through the square group
                        for (int y2 = 0; y2 < 3; y2++)
                        {
                            for (int x2 = 0; x2 < 3; x2++)
                            {
                                //If there is only a pair of possible numbers, add it to the shortlist
                                if (gameBoardPossibilitiesSquare[x2, y2].Count == 2)
                                {
                                    nakedPair.Add(new KeyValuePair<Point, HashSet<int>>(new Point(x2, y2), gameBoardPossibilitiesSquare[x2, y2]));
                                }
                            }
                        }

                        //Now loop through the hashsets and check if their are duplicates and hence can remove some possibilities in the square group
                        foreach (KeyValuePair<Point, HashSet<int>> item in nakedPair)
                        {
                            //double check there is still a value - as this evolves as we solve the puzzle
                            if (item.Value.Count == 2)
                            {
                                int number1 = RulesUtility.GetNthElement(item.Value, 1); //get the first item (not zero based)    
                                int number2 = RulesUtility.GetNthElement(item.Value, 2); //get the second item (not zero based)   

                                //Loop through the square group
                                for (int y2 = 0; y2 < 3; y2++)
                                {
                                    for (int x2 = 0; x2 < 3; x2++)
                                    {
                                        if (x2 != item.Key.X | y2 != item.Key.Y)
                                        {
                                            Point point1 = item.Key;
                                            if (item.Value.SetEquals(gameBoardPossibilitiesSquare[x2, y2]))
                                            {
                                                Point point2 = new Point(x2, y2);
                                                //Loop back through the square group, removing all numbers not at the two points
                                                for (int x3 = 0; x3 < 3; x3++)
                                                {
                                                    for (int y3 = 0; y3 < 3; y3++)
                                                    {
                                                        if (new Point(x3, y3) != point1 & new Point(x3, y3) != point2)
                                                        {
                                                            gameBoardPossibilitiesSquare[x3, y3].Remove(number1);
                                                            gameBoardPossibilitiesSquare[x3, y3].Remove(number2);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        gameBoardPossibilities = RulesUtility.InsertSquareGroupIntoGamePossibilities(gameBoardPossibilities, gameBoardPossibilitiesSquare, xSquare, ySquare);

                    }
                }
            }

            return new RuleResult(squaresSolved, gameBoard, gameBoardPossibilities);
        }

        public static RuleResult HiddenNakedPairsEliminationRule(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
        {
            int squaresSolved = 0;

            return new RuleResult(squaresSolved, gameBoard, gameBoardPossibilities);
        }

    }
}
