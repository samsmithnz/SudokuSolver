using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SudokuSolver.Core
{
    public class GameState
    {
        public string ProcessedGameBoardString;
        public int UnsolvedSquareCount;
        public int[,] GameBoard;
        public HashSet<int>[,] GameBoardPossibilities;
        public int IterationsToSolve = 0;
        public bool CrossCheckSuccessful;
        public string GameLevel;

        public GameState()
        {
            //Create the 9x9 board
            GameBoard = new int[9, 9];
            //Create the possibilities object
            GameBoardPossibilities = new HashSet<int>[9, 9];
            //Create the gameboard string
            ProcessedGameBoardString = "";

            int i = 0;
            //Load the rows into a 2d array
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    GameBoard[x, y] = 0;
                    //add all 9 possible numbers to initialize
                    GameBoardPossibilities[x, y] = new HashSet<int>(RulesUtility.SquareSet);
                    i++;
                }
            }
        }

        //Convert the game string into an array
        public void LoadGame(string game)
        {
            game = game.Trim();
            StringBuilder sb = new StringBuilder();
            foreach (string line in game.Split(Environment.NewLine))
            {
                //Strip out any comments
                if (line.Trim().StartsWith("#") == false)
                {
                    sb.Append(line);
                    sb.Append(Environment.NewLine);
                }
                else
                {
                    if (line.Trim().StartsWith("#L") == true)
                    {
                        GameLevel = line.Trim().Replace("#L", "").Trim();
                    }
                }
            }
            if (string.IsNullOrEmpty(GameLevel) == true)
            {
                GameLevel = "Unknown";
            }
            //Strip out new lines before and after the board
            ProcessedGameBoardString = RulesUtility.TrimNewLines(sb.ToString());
            UnsolvedSquareCount = ProcessedGameBoardString.Split('.').Length - 1;
            //Replace the .'s with 0's
            ProcessedGameBoardString = ProcessedGameBoardString.Replace(".", "0");
            string[] rows = ProcessedGameBoardString.Split(Environment.NewLine);

            int i = 0;
            //Load the rows into a 2d array
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < rows.Length; y++)
                {
                    int number = RulesUtility.ConvertStringToNumber(rows[y].Substring(x, 1));
                    GameBoard[x, y] = number;
                    if (number == 0)
                    {
                        //add all 9 possible numbers to initialize
                        GameBoardPossibilities[x, y] = new HashSet<int>(RulesUtility.SquareSet);
                    }
                    else
                    {
                        //empty hashset
                        GameBoardPossibilities[x, y] = new HashSet<int>();
                    }
                    i++;
                }
            }
        }

        //Process the rules on the game array
        public int ProcessRules(bool useRowRule,
            bool useColumnRule,
            bool useSquareGroupRule,
            bool useNakedPairsRule,
            bool useHiddenNakedPairsRule,
            bool solveSquares)
        {
            RuleResult ruleResult;
            int squaresSolved = 0;

            //1. Look to eliminate possibilities

            //Process the board and update with any changes
            if (useRowRule == true)
            {
                ruleResult = BasicRules.RowEliminationRule(GameBoard, GameBoardPossibilities);
                if (ruleResult != null)
                {
                    GameBoard = ruleResult.GameBoard;
                    GameBoardPossibilities = ruleResult.GameBoardPossibilities;
                }
            }

            if (useColumnRule == true)
            {
                ruleResult = BasicRules.ColumnEliminationRule(GameBoard, GameBoardPossibilities);
                if (ruleResult != null)
                {
                    GameBoard = ruleResult.GameBoard;
                    GameBoardPossibilities = ruleResult.GameBoardPossibilities;
                }
            }

            if (useSquareGroupRule == true)
            {
                ruleResult = BasicRules.SquareGroupEliminationRule(GameBoard, GameBoardPossibilities);
                if (ruleResult != null)
                {
                    GameBoard = ruleResult.GameBoard;
                    GameBoardPossibilities = ruleResult.GameBoardPossibilities;
                }
            }

            if (useNakedPairsRule == true)
            {
                ruleResult = AdvancedRules.NakedPairsEliminationRule(GameBoard, GameBoardPossibilities);
                if (ruleResult != null)
                {
                    GameBoard = ruleResult.GameBoard;
                    GameBoardPossibilities = ruleResult.GameBoardPossibilities;
                }
            }

            if (useHiddenNakedPairsRule == true)
            {
                ruleResult = AdvancedRules.HiddenNakedPairsEliminationRule(GameBoard, GameBoardPossibilities);
                if (ruleResult != null)
                {
                    GameBoard = ruleResult.GameBoard;
                    GameBoardPossibilities = ruleResult.GameBoardPossibilities;
                }
            }

            //2. Now looking at the possibilities, solve squares
            if (solveSquares == true)
            {
                //look for any squares with only one possibility 
                RuleResult finalOptionRuleResult = BasicRules.LoneSingleEliminationRule(GameBoard, GameBoardPossibilities);
                if (finalOptionRuleResult != null)
                {
                    GameBoard = finalOptionRuleResult.GameBoard;
                    GameBoardPossibilities = finalOptionRuleResult.GameBoardPossibilities;
                    ProcessedGameBoardString = RulesUtility.ConvertGameBoardToString(finalOptionRuleResult.GameBoard);
                    squaresSolved += finalOptionRuleResult.SquaresSolved;
                }

                //look for any numbers with just one possibility in a row/column/square group
                RuleResult possibilitiesRuleResult = BasicRules.HiddenSingleEliminationRule(GameBoard, GameBoardPossibilities);
                if (possibilitiesRuleResult != null)
                {
                    GameBoard = possibilitiesRuleResult.GameBoard;
                    GameBoardPossibilities = possibilitiesRuleResult.GameBoardPossibilities;
                    ProcessedGameBoardString = RulesUtility.ConvertGameBoardToString(possibilitiesRuleResult.GameBoard);
                    squaresSolved += possibilitiesRuleResult.SquaresSolved;
                }
            }

            UnsolvedSquareCount = ProcessedGameBoardString.Split('0').Length - 1;
            ProcessedGameBoardString = RulesUtility.TrimNewLines(ProcessedGameBoardString.Replace("0", "."));

            return squaresSolved;
        }

        public int SolveGame(bool useRowRule = true,
            bool useColumnRule = true,
            bool useSquareGroupRule = true,
            bool useNakedPairsRule = true,
            bool useHiddenNakedPairsRule = true,
            bool useBruteStrengthRule = false,
            bool solveSquares = true)
        {
            int squaresSolved = 0;
            int newSquaresSolved;
            IterationsToSolve = 0;
            do
            {
                //Keep looping while new squares are solved
                newSquaresSolved = ProcessRules(useRowRule, useColumnRule, useSquareGroupRule, useNakedPairsRule, useHiddenNakedPairsRule, solveSquares);
                squaresSolved += newSquaresSolved;
                IterationsToSolve++;
            } while (newSquaresSolved > 0);

            //Do the brute strength processing, to minimise the chance of breaking the puzzle
            if (useBruteStrengthRule == true & UnsolvedSquareCount > 0)
            {
                newSquaresSolved = ProcessBruteStrength(GameBoard, GameBoardPossibilities);
                squaresSolved += newSquaresSolved;
                IterationsToSolve++;
            }

            //validate result
            CrossCheckSuccessful = RulesUtility.CrossCheckResult(GameBoard, GameBoardPossibilities);

            return squaresSolved;
        }

        public int ProcessBruteStrength(int[,] gameBoard, HashSet<int>[,] gameBoardPossibilities)
        {
            int squaresSolved = 0;
            int[,] gameBoardBackup = (int[,])gameBoard.Clone();
            HashSet<int>[,] gameBoardPossibilitiesBackup = RulesUtility.CopyHashset(gameBoardPossibilities);
            int index = 0;
            int currentIndex = -1;

            //1. first build a list of possible moves
            List<KeyValuePair<Point, int>> possibleMoves = new List<KeyValuePair<Point, int>>();
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (GameBoardPossibilities[x, y].Count > 0)
                    {
                        for (int i = 0; i < GameBoardPossibilities[x, y].Count; i++)
                        {
                            possibleMoves.Add(new KeyValuePair<Point, int>(new Point(x, y), RulesUtility.GetNthElement(GameBoardPossibilities[x, y], i + 1)));
                        }
                    }
                }
            }

            if (possibleMoves.Count > 0)
            {
                do
                {
                    int squaresSolvedCircuitBreaker = squaresSolved;

                    //do work
                    //2. Try putting in a random number.
                    if (currentIndex < index)
                    {
                        currentIndex = index;
                        GameBoardPossibilities[possibleMoves[index].Key.X, possibleMoves[index].Key.Y].Remove(possibleMoves[index].Value);
                    }

                    //3. Process the rules
                    squaresSolved += SolveGame(true, true, true, true, true, false, true);

                    //4. Was it successful? Break out. If not, loop back to 1 and try another number
                    if (RulesUtility.CrossCheckResult(GameBoard, GameBoardPossibilities) == false)
                    {
                        //We failed. We need to reset and try removing another number.
                        GameBoard = (int[,])gameBoardBackup.Clone();
                        GameBoardPossibilities = RulesUtility.CopyHashset(gameBoardPossibilitiesBackup);
                        squaresSolved = 0;
                        index++;
                    }
                    //Otherwise, we have a valid board, check to see if we have been making progress.
                    else if (squaresSolved == squaresSolvedCircuitBreaker)
                    {
                        break;
                    }
                } while (UnsolvedSquareCount > 0);
            }

            return squaresSolved;
        }
    }
}