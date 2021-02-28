using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Core
{
    public class GameState
    {
        //public string RawGameString;
        public string ProcessedGameBoardString;
        public int UnsolvedSquareCount;
        public int[,] GameBoard;
        public HashSet<int>[,] GameBoardPossibilities;
        public int IterationsToSolve = 0;

        public void LoadGame(string game)
        {
            //RawGameString = game;
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

            //Create the 9x9 board
            GameBoard = new int[9, 9];
            //Create the possibilities object
            GameBoardPossibilities = new HashSet<int>[9, 9];
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

        public int ProcessRules(bool useRowRule = true,
            bool useColumnRule = true,
            bool useSquareGroupRule = true)
        {
            if (GameBoard == null || GameBoardPossibilities == null)
            {
                throw new Exception("Game not loaded");
            }

            Rules rules = new Rules();
            RuleResult ruleResult = null;
            int squaresSolved = 0;

            //Process the board and update with any changes
            if (useRowRule == true)
            {
                ruleResult = rules.RowEliminationRule(GameBoard, GameBoardPossibilities);
                if (ruleResult != null)
                {
                    GameBoard = ruleResult.GameBoard;
                    squaresSolved += ruleResult.SquaresSolved;
                    ProcessedGameBoardString = UpdateProcessedGameBoardString(ruleResult.GameBoard);
                }
            }

            if (useColumnRule == true)
            {
                ruleResult = rules.ColumnEliminationRule(GameBoard, GameBoardPossibilities);
                if (ruleResult != null)
                {
                    GameBoard = ruleResult.GameBoard;
                    squaresSolved += ruleResult.SquaresSolved;
                    ProcessedGameBoardString = UpdateProcessedGameBoardString(ruleResult.GameBoard);
                }
            }

            if (useSquareGroupRule == true)
            {
                ruleResult = rules.SquareGroupEliminationRule(GameBoard, GameBoardPossibilities);
                if (ruleResult != null)
                {
                    GameBoard = ruleResult.GameBoard;
                    squaresSolved += ruleResult.SquaresSolved;
                    ProcessedGameBoardString = UpdateProcessedGameBoardString(ruleResult.GameBoard);
                }
            }

            UnsolvedSquareCount = ProcessedGameBoardString.Split('0').Length - 1;

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
                newSquaresSolved = ProcessRules(true, true, true);
                squaresSolved += newSquaresSolved;
                IterationsToSolve++;
            } while (newSquaresSolved > 0);

            return squaresSolved;
        }

        private string UpdateProcessedGameBoardString(int[,] gameBoard)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            //Load the rows into a 2d array
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    sb.Append(gameBoard[x, y].ToString());
                    i++;
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        public string OutputState()
        {
            return Utility.TrimNewLines(ProcessedGameBoardString.Replace("0", "."));
        }

    }
}