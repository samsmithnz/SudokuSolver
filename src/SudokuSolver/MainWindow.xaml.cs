﻿using SudokuSolver.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SudokuSolver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly GameState GameState = new GameState();


        public MainWindow()
        {
            InitializeComponent();
            LoadGamesIntoDropdown();
            LoadGrid();
        }

        private void DropdownChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\games\\" + cboSudokuGames.SelectedItem.ToString());
            StringBuilder sb = new StringBuilder();
            foreach (string item in lines)
            {
                sb.Append(item);
                sb.Append(Environment.NewLine);
            }
            string game = sb.ToString();
            GameState.LoadGame(game);
            GameState.ProcessRules(true, true, true, true, false);
            LoadGrid();
        }

        private void ButtonSolvePartialSudoku_Click(object sender, RoutedEventArgs e)
        {
            GameState.ProcessRules(true, true, true, true, true);
            GameState.CrossCheckSuccessful = Rules.CrossCheckResultRule(GameState.GameBoard);
            LoadGrid();
            UpdateTextStatus();
        }

        private void ButtonSolveEntireSudoku_Click(object sender, RoutedEventArgs e)
        {
            GameState.SolveGame();
            LoadGrid();
            UpdateTextStatus();
        }

        private void LoadGamesIntoDropdown()
        {
            string gameFolder = Environment.CurrentDirectory + "\\games";
            FileInfo[] files = new DirectoryInfo(gameFolder).GetFiles("*.sdk");
            foreach (FileInfo file in files)
            {
                cboSudokuGames.Items.Add(file.Name);
            }
            cboSudokuGames.SelectedIndex = 0;
        }

        private void LoadGrid()
        {
            int i = 0;
            for (int k = 1; k <= 9; k++)
            {
                SudokuSolver.SquareGroupUserControl squareGroup = FindSquareGroup(k);
                if (squareGroup != null)
                {
                    for (int j = 1; j <= 9; j++)
                    {
                        //This looks super complex, because it is. Essentially we have a [x,y] array, and we need to fill it by 3x3 square, top left, to bottom right.
                        int xBase;
                        if (i >= 54)
                        {
                            xBase = (int)Math.Truncate(Math.Truncate((((decimal)i - 54m) / 9m)) * 3m);
                        }
                        else if (i >= 27)
                        {
                            xBase = (int)Math.Truncate(Math.Truncate((((decimal)i - 27m) / 9m)) * 3m);
                        }
                        else
                        {
                            xBase = (int)Math.Truncate((decimal)i / 9m) * 3;
                        }
                        int yBase = (int)Math.Truncate((decimal)i / 27m) * 3;
                        int x = xBase + (i % 3);
                        int y = yBase + (int)Math.Truncate((((decimal)i / 3m) % 3m));

                        SudokuSolver.SquareUserControl square = FindSquare(j, squareGroup);
                        square.LoadSquare(GameState.GameBoard[x, y], GameState.GameBoardPossibilities[x, y]);
                        i++;
                    }
                }
            }

        }

        private SudokuSolver.SquareGroupUserControl FindSquareGroup(int i)
        {
            foreach (var item in GameGrid.Children)
            {
                if (item.GetType().ToString() == "SudokuSolver.SquareGroupUserControl")
                {
                    SudokuSolver.SquareGroupUserControl squareGroup = (SudokuSolver.SquareGroupUserControl)item;
                    if (squareGroup.Name == "squareGroup" + i)
                    {
                        return squareGroup;
                    }
                }
            }
            return null;
        }

        private static SudokuSolver.SquareUserControl FindSquare(int i, SudokuSolver.SquareGroupUserControl squareGroup)
        {
            foreach (var item in squareGroup.SquareGroupGrid.Children)
            {
                if (item.GetType().ToString() == "SudokuSolver.SquareUserControl")
                {
                    SudokuSolver.SquareUserControl square = (SudokuSolver.SquareUserControl)item;
                    if (square.Name == "square" + i)
                    {
                        return square;
                    }
                }
            }
            return null;
        }

        private void UpdateTextStatus()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append( "Cross check successful: " + GameState.CrossCheckSuccessful);
            sb.Append(Environment.NewLine);
            sb.Append("Unsolved squares: " + GameState.UnsolvedSquareCount);
            sb.Append(Environment.NewLine);
            sb.Append("Possiblity breakdown: " );
            sb.Append(Environment.NewLine);

            int[] possiblities = new int[9];
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        if (GameState.GameBoardPossibilities[x, y].Contains(i)==true)
                        {
                            possiblities[i-1]++;
                        }

                    }
                }
            }
            sb.Append("1:");
            sb.Append(possiblities[0]);
            sb.Append(Environment.NewLine);
            sb.Append("2:");
            sb.Append(possiblities[1]);
            sb.Append(Environment.NewLine);
            sb.Append("3:");
            sb.Append(possiblities[2]);
            sb.Append(Environment.NewLine);
            sb.Append("4:");
            sb.Append(possiblities[3]);
            sb.Append(Environment.NewLine);
            sb.Append("5:");
            sb.Append(possiblities[4]);
            sb.Append(Environment.NewLine);
            sb.Append("6:");
            sb.Append(possiblities[5]);
            sb.Append(Environment.NewLine);
            sb.Append("7:");
            sb.Append(possiblities[6]);
            sb.Append(Environment.NewLine);
            sb.Append("8:");
            sb.Append(possiblities[7]);
            sb.Append(Environment.NewLine);
            sb.Append("9:");
            sb.Append(possiblities[8]);
            sb.Append(Environment.NewLine);

            txtStatus.Text = sb.ToString();
        }

    }
}
