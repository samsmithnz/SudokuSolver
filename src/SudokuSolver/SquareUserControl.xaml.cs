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
                lbl1.Visibility = Visibility.Visible;
            }
            else
            {
                lbl1.Visibility = Visibility.Hidden;
            }
            if (possibilities.Contains(2) == true)
            {
                lbl2.Visibility = Visibility.Visible;
            }
            else
            {
                lbl2.Visibility = Visibility.Hidden;
            }
            if (possibilities.Contains(3) == true)
            {
                lbl3.Visibility = Visibility.Visible;
            }
            else
            {
                lbl3.Visibility = Visibility.Hidden;
            }
            if (possibilities.Contains(4) == true)
            {
                lbl4.Visibility = Visibility.Visible;
            }
            else
            {
                lbl4.Visibility = Visibility.Hidden;
            }
            if (possibilities.Contains(5) == true)
            {
                lbl5.Visibility = Visibility.Visible;
            }
            else
            {
                lbl5.Visibility = Visibility.Hidden;
            }
            if (possibilities.Contains(6) == true)
            {
                lbl6.Visibility = Visibility.Visible;
            }
            else
            {
                lbl6.Visibility = Visibility.Hidden;
            }
            if (possibilities.Contains(7) == true)
            {
                lbl7.Visibility = Visibility.Visible;
            }
            else
            {
                lbl7.Visibility = Visibility.Hidden;
            }
            if (possibilities.Contains(8) == true)
            {
                lbl8.Visibility = Visibility.Visible;
            }
            else
            {
                lbl8.Visibility = Visibility.Hidden;
            }
            if (possibilities.Contains(9) == true)
            {
                lbl9.Visibility = Visibility.Visible;
            }
            else
            {
                lbl9.Visibility = Visibility.Hidden;
            }
            return true;
        }
    }
}
