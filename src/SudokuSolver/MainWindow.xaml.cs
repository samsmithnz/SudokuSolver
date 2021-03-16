using SudokuSolver.Core;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            LoadGame();
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadGame();
        }

        private void LoadGame()
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
            GameState.ProcessRules((bool)chkUseRowRule.IsChecked, (bool)chkUseColumnRule.IsChecked, (bool)chkUseSquareGroupRule.IsChecked,
                (bool)chkUseNakedPairsRule.IsChecked, (bool)chkUseHiddenNakedPairsRule.IsChecked,  false);
            LoadGrid();
        }

        private void ButtonSolvePartialSudoku_Click(object sender, RoutedEventArgs e)
        {
            GameState.ProcessRules((bool)chkUseRowRule.IsChecked, (bool)chkUseColumnRule.IsChecked, (bool)chkUseSquareGroupRule.IsChecked,
                (bool)chkUseNakedPairsRule.IsChecked, (bool)chkUseHiddenNakedPairsRule.IsChecked,  true);
            GameState.CrossCheckSuccessful = RulesUtility.CrossCheckResult(GameState.GameBoard, GameState.GameBoardPossibilities);
            LoadGrid();
            UpdateTextStatus();
        }

        private void ButtonSolveEntireSudoku_Click(object sender, RoutedEventArgs e)
        {
            GameState.SolveGame((bool)chkUseRowRule.IsChecked, (bool)chkUseColumnRule.IsChecked, (bool)chkUseSquareGroupRule.IsChecked,
                (bool)chkUseNakedPairsRule.IsChecked, (bool)chkUseHiddenNakedPairsRule.IsChecked, (bool)chkUseBruteStrengthRule.IsChecked);
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
                        //Danger Will Robinson, Danger!!!
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
            sb.Append("Cross check successful: " + GameState.CrossCheckSuccessful);
            sb.Append(Environment.NewLine);
            sb.Append("Unsolved squares: " + GameState.UnsolvedSquareCount);
            sb.Append(Environment.NewLine);
            sb.Append("Possiblity breakdown: ");
            sb.Append(Environment.NewLine);

            int[] possiblities = new int[9];
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        if (GameState.GameBoardPossibilities[x, y].Contains(i) == true)
                        {
                            possiblities[i - 1]++;
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

        private void HighlightPossibilities(int number, bool isHighlighted)
        {
            for (int k = 1; k <= 9; k++)
            {
                SudokuSolver.SquareGroupUserControl squareGroup = FindSquareGroup(k);
                if (squareGroup != null)
                {
                    for (int j = 1; j <= 9; j++)
                    {
                        SquareUserControl square = FindSquare(j, squareGroup);
                        UpdatePencilStatus(number, (Label)square.FindName("PencilMark" + number), isHighlighted);
                    }
                }
            }
        }

        private static void UpdatePencilStatus(int number, Label pencilMark, bool isHighlighted)
        {
            if (isHighlighted == true && pencilMark.Visibility == Visibility.Visible)
            {
                pencilMark.Foreground = Brushes.White;
                if (number == 1)
                {
                    pencilMark.Background = Brushes.Red;
                }
                else if (number == 2)
                {
                    pencilMark.Background = Brushes.Black;
                }
                else if (number == 3)
                {
                    pencilMark.Background = Brushes.DarkGoldenrod;
                }
                else if (number == 4)
                {
                    pencilMark.Background = Brushes.Green;
                }
                else if (number == 5)
                {
                    pencilMark.Background = Brushes.Blue;
                }
                else if (number == 6)
                {
                    pencilMark.Background = Brushes.Purple;
                }
                else if (number == 7)
                {
                    pencilMark.Background = Brushes.Brown;
                }
                else if (number == 8)
                {
                    pencilMark.Background = Brushes.Salmon;
                }
                else if (number == 9)
                {
                    pencilMark.Background = Brushes.HotPink;
                }
                pencilMark.FontWeight = FontWeights.Bold;
            }
            else
            {
                pencilMark.Foreground = Brushes.Black;
                pencilMark.Background = Brushes.White;
                pencilMark.FontWeight = FontWeights.Normal;
            }
        }

        private void CheckBoxAll_Checked(object sender, RoutedEventArgs e)
        {
            chk1.IsChecked = chkAll.IsChecked;
            chk2.IsChecked = chkAll.IsChecked;
            chk3.IsChecked = chkAll.IsChecked;
            chk4.IsChecked = chkAll.IsChecked;
            chk5.IsChecked = chkAll.IsChecked;
            chk6.IsChecked = chkAll.IsChecked;
            chk7.IsChecked = chkAll.IsChecked;
            chk8.IsChecked = chkAll.IsChecked;
            chk9.IsChecked = chkAll.IsChecked;
        }
        private void CheckBox1_Checked(object sender, RoutedEventArgs e)
        {
            HighlightPossibilities(1, (bool)chk1.IsChecked);
        }

        private void CheckBox2_Checked(object sender, RoutedEventArgs e)
        {
            HighlightPossibilities(2, (bool)chk2.IsChecked);
        }

        private void CheckBox3_Checked(object sender, RoutedEventArgs e)
        {
            HighlightPossibilities(3, (bool)chk3.IsChecked);
        }

        private void CheckBox4_Checked(object sender, RoutedEventArgs e)
        {
            HighlightPossibilities(4, (bool)chk4.IsChecked);
        }

        private void CheckBox5_Checked(object sender, RoutedEventArgs e)
        {
            HighlightPossibilities(5, (bool)chk5.IsChecked);
        }

        private void CheckBox6_Checked(object sender, RoutedEventArgs e)
        {
            HighlightPossibilities(6, (bool)chk6.IsChecked);
        }

        private void CheckBox7_Checked(object sender, RoutedEventArgs e)
        {
            HighlightPossibilities(7, (bool)chk7.IsChecked);
        }

        private void CheckBox8_Checked(object sender, RoutedEventArgs e)
        {
            HighlightPossibilities(8, (bool)chk8.IsChecked);
        }

        private void CheckBox9_Checked(object sender, RoutedEventArgs e)
        {
            HighlightPossibilities(9, (bool)chk9.IsChecked);
        }
    }
}
