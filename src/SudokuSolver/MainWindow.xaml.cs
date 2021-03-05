using SudokuSolver.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private GameState GameState = new GameState();


        public MainWindow()
        {
            InitializeComponent();
            LoadGame();
            LoadGrid();
        }

        public void LoadGame()
        {
            string game = @"
27.1.5..3
354...71.
9162.3.8.
6.28.73.4
.........
1.53.98.6
.2.7.1.6.
.81...24.
7..4.2..1
";

            GameState.LoadGame(game);
            GameState.ProcessRules(true, true, true);
        }

        public void LoadGrid()
        {
            for (int i = 1; i <= 9; i++)
            {
                SudokuSolver.SquareGroupUserControl squareGroup = FindSquareGroup(i);
                if (squareGroup != null)
                {
                    for (int j = 1; j <= 9; j++)
                    {
                        SudokuSolver.SquareUserControl square = FindSquare(j, squareGroup);
                        square.LoadSquare(GameState.GameBoard[0, 0], GameState.GameBoardPossibilities[0, 0]);
                    }
                }
            }
            //foreach (var squareObj in squareGroup.SquareGroupGrid.Children)
            //{
            //    if (squareObj.GetType().ToString() == "SudokuSolver.SquareUserControl")
            //    {
            //        SudokuSolver.SquareUserControl square = (SudokuSolver.SquareUserControl)squareObj;
            //        square.LoadSquare(GameState.GameBoard[0, 0], GameState.GameBoardPossibilities[0, 0]);
            //        square.LoadSquare(GameState.GameBoard[1, 0], GameState.GameBoardPossibilities[1, 0]);
            //        square.LoadSquare(GameState.GameBoard[2, 0], GameState.GameBoardPossibilities[2, 0]);
            //        square.LoadSquare(GameState.GameBoard[0, 1], GameState.GameBoardPossibilities[0, 1]);
            //        square.LoadSquare(GameState.GameBoard[1, 1], GameState.GameBoardPossibilities[1, 1]);
            //        square.LoadSquare(GameState.GameBoard[2, 1], GameState.GameBoardPossibilities[2, 1]);
            //        square.LoadSquare(GameState.GameBoard[0, 2], GameState.GameBoardPossibilities[0, 2]);
            //        square.LoadSquare(GameState.GameBoard[1, 2], GameState.GameBoardPossibilities[1, 2]);
            //        square.LoadSquare(GameState.GameBoard[2, 2], GameState.GameBoardPossibilities[2, 2]);

            //        square.LoadSquare(GameState.GameBoard[3, 0], GameState.GameBoardPossibilities[3, 0]);
            //        square.LoadSquare(GameState.GameBoard[4, 0], GameState.GameBoardPossibilities[4, 0]);
            //        square.LoadSquare(GameState.GameBoard[5, 0], GameState.GameBoardPossibilities[5, 0]);
            //        square.LoadSquare(GameState.GameBoard[3, 1], GameState.GameBoardPossibilities[3, 1]);
            //        square.LoadSquare(GameState.GameBoard[4, 1], GameState.GameBoardPossibilities[4, 1]);
            //        square.LoadSquare(GameState.GameBoard[5, 1], GameState.GameBoardPossibilities[5, 1]);
            //        square.LoadSquare(GameState.GameBoard[3, 2], GameState.GameBoardPossibilities[3, 2]);
            //        square.LoadSquare(GameState.GameBoard[4, 2], GameState.GameBoardPossibilities[4, 2]);
            //        square.LoadSquare(GameState.GameBoard[5, 2], GameState.GameBoardPossibilities[5, 2]);

            //        square.LoadSquare(GameState.GameBoard[6, 0], GameState.GameBoardPossibilities[6, 0]);
            //        square.LoadSquare(GameState.GameBoard[7, 0], GameState.GameBoardPossibilities[7, 0]);
            //        square.LoadSquare(GameState.GameBoard[8, 0], GameState.GameBoardPossibilities[8, 0]);
            //        square.LoadSquare(GameState.GameBoard[6, 1], GameState.GameBoardPossibilities[6, 1]);
            //        square.LoadSquare(GameState.GameBoard[7, 1], GameState.GameBoardPossibilities[7, 1]);
            //        square.LoadSquare(GameState.GameBoard[8, 1], GameState.GameBoardPossibilities[8, 1]);
            //        square.LoadSquare(GameState.GameBoard[6, 2], GameState.GameBoardPossibilities[6, 2]);
            //        square.LoadSquare(GameState.GameBoard[7, 2], GameState.GameBoardPossibilities[7, 2]);
            //        square.LoadSquare(GameState.GameBoard[8, 2], GameState.GameBoardPossibilities[8, 2]);
            //    }
            //}
            //}
            //for (int y = 0; y < 9; y++)
            //{
            //    //GameState.GameBoard[x, y];

            //}
            //}


            //int x = 0;
            //int y = 0;
            //int xBase = 0;
            //int yBase = 0;
            //foreach (var item in GameGrid.Children)
            //{
            //    if (item.GetType().ToString() == "SudokuSolver.SquareGroupUserControl")
            //    {
            //        SudokuSolver.SquareGroupUserControl squareGroup = (SudokuSolver.SquareGroupUserControl)item;
            //        foreach (var item2 in squareGroup.SquareGroupGrid.Children)
            //        {
            //            if (item2.GetType().ToString() == "SudokuSolver.SquareUserControl")
            //            {
            //                x = xBase + x;
            //                y = yBase + y;
            //                SudokuSolver.SquareUserControl square = (SudokuSolver.SquareUserControl)item2;
            //                square.LoadSquare(GameState.GameBoard[x, y], GameState.GameBoardPossibilities[x, y]);
            //                Debug.WriteLine("x: " + x.ToString() + ", y: " + y.ToString());
            //                x++;
            //                if (x == 3 || x == 6 || x == 9)
            //                {
            //                    if (x == 3 || x == 6)
            //                    {
            //                        x -= 3;
            //                    }
            //                    else
            //                    {
            //                        x = 0;
            //                    }
            //                    y++;
            //                    if (y >= 9)
            //                    {
            //                        y = 0;
            //                    }
            //                }
            //            }
            //            x += 3;
            //            if (x >= 9)
            //            {
            //                x = 0;
            //                y += 3;
            //            }


            //            //00
            //            //10
            //            //20
            //            //01
            //            //11
            //            //21
            //            //02
            //            //12
            //            //22
            //        }
            //    }
            //}
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

        private SudokuSolver.SquareUserControl FindSquare(int i, SudokuSolver.SquareGroupUserControl squareGroup)
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
