using SudokuSolver.Core;
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

        private void ButtonLoadSudoku_Click(object sender, RoutedEventArgs e)
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
            GameState.ProcessRules(true, true, true,true, false);
            LoadGrid();
        }

        private void ButtonSolvePartialSudoku_Click(object sender, RoutedEventArgs e)
        {
            GameState.ProcessRules(true, true, true, true, true);
            GameState.CrossCheckSuccessful = Rules.CrossCheckResultRule(GameState.GameBoard);
            LoadGrid();
            txtStatus.Text = "Cross check successful: " + GameState.CrossCheckSuccessful;
            txtStatus.Text += Environment.NewLine;
            txtStatus.Text += "Unsolved squares: " + GameState.UnsolvedSquareCount;
        }

        private void ButtonSolveEntireSudoku_Click(object sender, RoutedEventArgs e)
        {
            GameState.SolveGame();
            LoadGrid();
            txtStatus.Text = "Cross check successful: " + GameState.CrossCheckSuccessful;
            txtStatus.Text += Environment.NewLine;
            txtStatus.Text += "Unsolved squares: " + GameState.UnsolvedSquareCount;
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
    }
}
