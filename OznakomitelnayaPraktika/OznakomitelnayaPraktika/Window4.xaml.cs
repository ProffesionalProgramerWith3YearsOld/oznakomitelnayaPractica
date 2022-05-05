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
    /// Логика взаимодействия для Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        public Window4()
        {
            InitializeComponent();
        }

        public static string[] getAddInsert_doc()
        {

            Window4 window4 = new Window4();
            window4.ShowDialog();
            string[] result = {window4.docName.Text,window4.issueDate.Text,window4.serial.Text,window4.number.Text };
            if (window4.timelabel.Visibility == Visibility.Visible | window4.docnameLabel.Visibility == Visibility.Visible | window4.seriesLabel.Visibility == Visibility.Visible | window4.numLabel.Visibility == Visibility.Visible)
                return null;
            return result;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DateTime s;
            if (!DateTime.TryParse(issueDate.Text, out s))
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
            if (timelabel.Visibility != Visibility.Visible & docnameLabel.Visibility != Visibility.Visible & seriesLabel.Visibility != Visibility.Visible & numLabel.Visibility != Visibility)
            {

                this.Close();
            }
            else
            {
                MessageBox.Show("Введите корректные данные");
            }
        }

        private void docName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (docName.Text == string.Empty)
            {
                docnameLabel.Visibility = Visibility.Visible;
            }
            else
            {
                docnameLabel.Visibility = Visibility.Hidden;
            }
        }

        private void serial_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (serial.Text == string.Empty)
            {
                seriesLabel.Visibility = Visibility.Visible;
            }
            else
            {
                seriesLabel.Visibility = Visibility.Hidden;
            }
        }

        private void number_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (number.Text == string.Empty)
            {
                numLabel.Visibility = Visibility.Visible;
            }
            else
            {
                numLabel.Visibility = Visibility.Hidden;
            }
        }
    }
}
