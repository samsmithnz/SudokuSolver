using System;
using System.Collections.Generic;
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
    /// Interaction logic for SquareUserControl.xaml
    /// </summary>
    public partial class SquareUserControl : UserControl
    {
        public SquareUserControl()
        {
            InitializeComponent();
        }

        public bool LoadSquare(int number, HashSet<int> possibilities)
        {
            txtSquare.Text = number.ToString();
            if (number == 0)
            {
                txtSquare.Visibility = Visibility.Visible;
                //txtSquare.IsEnabled = false;
                //txtSquare.Background = Brushes.White;
                txtSquare.Text = "";
            }
            else
            {
                txtSquare.Visibility = Visibility.Visible;
                //txtSquare.IsEnabled = true;
                //txtSquare.Background = Brushes.LightGray;
            }
            if (possibilities.Contains(1) == true)
            {
                PencilMark1.Visibility = Visibility.Visible;
            }
            else
            {
                PencilMark1.Visibility = Visibility.Hidden;
            }
            if (possibilities.Contains(2) == true)
            {
                PencilMark2.Visibility = Visibility.Visible;
            }
            else
            {
                PencilMark2.Visibility = Visibility.Hidden;
            }
            if (possibilities.Contains(3) == true)
            {
                PencilMark3.Visibility = Visibility.Visible;
            }
            else
            {
                PencilMark3.Visibility = Visibility.Hidden;
            }
            if (possibilities.Contains(4) == true)
            {
                PencilMark4.Visibility = Visibility.Visible;
            }
            else
            {
                PencilMark4.Visibility = Visibility.Hidden;
            }
            if (possibilities.Contains(5) == true)
            {
                PencilMark5.Visibility = Visibility.Visible;
            }
            else
            {
                PencilMark5.Visibility = Visibility.Hidden;
            }
            if (possibilities.Contains(6) == true)
            {
                PencilMark6.Visibility = Visibility.Visible;
            }
            else
            {
                PencilMark6.Visibility = Visibility.Hidden;
            }
            if (possibilities.Contains(7) == true)
            {
                PencilMark7.Visibility = Visibility.Visible;
            }
            else
            {
                PencilMark7.Visibility = Visibility.Hidden;
            }
            if (possibilities.Contains(8) == true)
            {
                PencilMark8.Visibility = Visibility.Visible;
            }
            else
            {
                PencilMark8.Visibility = Visibility.Hidden;
            }
            if (possibilities.Contains(9) == true)
            {
                PencilMark9.Visibility = Visibility.Visible;
            }
            else
            {
                PencilMark9.Visibility = Visibility.Hidden;
            }
            return true;
        }
    }
}
