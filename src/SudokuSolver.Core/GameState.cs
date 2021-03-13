using System;
using System.Collections.Generic;
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

            //validate result
            CrossCheckSuccessful = RulesUtility.CrossCheckResult(GameBoard);

            return squaresSolved;
        }

    }
}