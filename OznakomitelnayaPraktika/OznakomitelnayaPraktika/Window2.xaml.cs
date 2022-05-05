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

namespace OznakomitelnayaPraktika
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        public int flagMode = 0;

        public static int GetAddFlag()
        {
            Window2 win = new Window2();
            win.ShowDialog();
            return win.flagMode;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            flagMode = 1;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            flagMode = 2;
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            flagMode = 3;
            this.Close();
        }
    }
}
