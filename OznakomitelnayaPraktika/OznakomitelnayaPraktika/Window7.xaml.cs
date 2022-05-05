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
using System.Windows.Shapes;
using System.IO;

namespace OznakomitelnayaPraktika
{
    /// <summary>
    /// Логика взаимодействия для Window7.xaml
    /// </summary>
    public partial class Window7 : Window
    {
        public Window7()
        {
            InitializeComponent();
        }

        public bool closeFlag = false;

        private void boxManagName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (boxManagName.Text == string.Empty)
            {
                ManagNameFill.Visibility = Visibility.Visible;
            }
            else
            {
                ManagNameFill.Visibility = Visibility.Hidden;
            }
        }

        private void boxDepName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (boxDepName.SelectedItem == null)
            {
                DepNameFill.Visibility = Visibility.Visible;
            }
            else
            {
                DepNameFill.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(ManagNameFill.Visibility == Visibility.Hidden & DepNameFill.Visibility == Visibility.Hidden)
            {
                closeFlag = true;
                this.Close();
            }
        }
    }
}
