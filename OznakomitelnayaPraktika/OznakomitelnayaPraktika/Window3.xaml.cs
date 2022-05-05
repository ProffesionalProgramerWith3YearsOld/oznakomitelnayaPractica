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
using Microsoft.Data.Sqlite;

namespace OznakomitelnayaPraktika
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
        }

        public static string[] getAddInsert_wb()
        {
            Window3 window3 = new Window3();
            window3.managing.ItemsSource = MainWindow.getManagingList();
            window3.ShowDialog();
            string[] result = { window3.managing.SelectedItem.ToString().Split('|')[0], window3.insertTime.Text, window3.note.Text, window3.position.Text };
            if (window3.timelabel.Visibility == Visibility.Visible | window3.managLabel.Visibility == Visibility.Visible | window3.positionLabel.Visibility == Visibility.Visible)
                result = null;
            return result;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DateTime s;
            if (!DateTime.TryParse (insertTime.Text, out s))
            {
                timelabel.Visibility = Visibility.Visible;
            }
            else
            {
                timelabel.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (timelabel.Visibility != Visibility.Visible & managLabel.Visibility != Visibility.Visible & positionLabel.Visibility != Visibility.Visible)
            {

                this.Close();
            }
            else
            {
                MessageBox.Show("Введите корректные данные");
            }
        }

        private void managing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(managing.SelectedItem == null)
            {
                managLabel.Visibility = Visibility.Visible;
            }
            else 
            {
                managLabel.Visibility = Visibility.Hidden;
            }
        }

        private void position_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(position.Text == string.Empty)
            {
                positionLabel.Visibility = Visibility.Visible;
            }
            else
            {
                positionLabel.Visibility = Visibility.Hidden;
            }
        }
    }
}
