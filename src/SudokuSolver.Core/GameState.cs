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
                    GameBoardPossibilities[x, y] = new HashSet<int>(Utility.SquareSet);
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
            }
            //Strip out new lines before and after the board
            ProcessedGameBoardString = Utility.TrimNewLines(sb.ToString());
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
                    int number = Utility.GetNumber(rows[y].Substring(x, 1));
                    GameBoard[x, y] = number;
                    if (number == 0)
                    {
                        //add all 9 possible numbers to initialize
                        GameBoardPossibilities[x, y] = new HashSet<int>(Utility.SquareSet);
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
            bool solveSquares)
        {
            RuleResult ruleResult;
            int squaresSolved = 0;

            //1. Look to eliminate possibilities

            //Process the board and update with any changes
            if (useRowRule == true)
            {
                ruleResult = Rules.RowEliminationRule(GameBoard, GameBoardPossibilities);
                if (ruleResult != null)
                {
                    GameBoard = ruleResult.GameBoard;
                    GameBoardPossibilities = ruleResult.GameBoardPossibilities;
                }
            }

            if (useColumnRule == true)
            {
                ruleResult = Rules.ColumnEliminationRule(GameBoard, GameBoardPossibilities);
                if (ruleResult != null)
                {
                    GameBoard = ruleResult.GameBoard;
                    GameBoardPossibilities = ruleResult.GameBoardPossibilities;
                }
            }

            if (useSquareGroupRule == true)
            {
                ruleResult = Rules.SquareGroupEliminationRule(GameBoard, GameBoardPossibilities);
                if (ruleResult != null)
                {
                    GameBoard = ruleResult.GameBoard;
                    GameBoardPossibilities = ruleResult.GameBoardPossibilities;
                }
            }

            if (useNakedPairsRule == true)
            {
                ruleResult = Rules.NakedPairsEliminationRule(GameBoard, GameBoardPossibilities);
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
                RuleResult finalOptionRuleResult = Rules.LoneSingleEliminationRule(GameBoard, GameBoardPossibilities);
                if (finalOptionRuleResult != null)
                {
                    GameBoard = finalOptionRuleResult.GameBoard;
                    GameBoardPossibilities = finalOptionRuleResult.GameBoardPossibilities;
                    ProcessedGameBoardString = UpdateProcessedGameBoardString(finalOptionRuleResult.GameBoard);
                    squaresSolved += finalOptionRuleResult.SquaresSolved;
                }

                //look for any numbers with just one possibility in a row/column/square group
                RuleResult possibilitiesRuleResult = Rules.HiddenSingleEliminationRule(GameBoard, GameBoardPossibilities);
                if (possibilitiesRuleResult != null)
                {
                    GameBoard = possibilitiesRuleResult.GameBoard;
                    GameBoardPossibilities = possibilitiesRuleResult.GameBoardPossibilities;
                    ProcessedGameBoardString = UpdateProcessedGameBoardString(possibilitiesRuleResult.GameBoard);
                    squaresSolved += possibilitiesRuleResult.SquaresSolved;
                }
            }

            UnsolvedSquareCount = ProcessedGameBoardString.Split('0').Length - 1;
            ProcessedGameBoardString = Utility.TrimNewLines(ProcessedGameBoardString.Replace("0", "."));

            return squaresSolved;
        }

        public int SolveGame()
        {
            int squaresSolved = 0;
            int newSquaresSolved;
            IterationsToSolve = 0;
            do
            {
                //Keep looping while new squares are solved
                newSquaresSolved = ProcessRules(true, true, true,false, true);
                squaresSolved += newSquaresSolved;
                IterationsToSolve++;
            } while (newSquaresSolved > 0);

            //validate result
            CrossCheckSuccessful = Rules.CrossCheckResultRule(GameBoard);

            return squaresSolved;
        }

        private static string UpdateProcessedGameBoardString(int[,] gameBoard)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            //Load the rows into a 2d array
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    sb.Append(gameBoard[x, y]);
                    i++;
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

    }
}