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
    /// Логика взаимодействия для Window5.xaml
    /// </summary>
    public partial class Window5 : Window
    {
        public Window5()
        {
            InitializeComponent();
        }

        public bool closeFlag = false;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(surnameFill.Visibility == Visibility.Hidden & nameFill.Visibility == Visibility.Hidden & patronomycFill.Visibility == Visibility.Hidden & birthDateFill.Visibility == Visibility.Hidden & managinFill.Visibility == Visibility.Hidden & AcceptDateFill.Visibility == Visibility.Hidden & positionFill.Visibility == Visibility.Hidden)
            {
                closeFlag = true;
                this.Close();
            }
        }

        private void boxSurame_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (boxSurame.Text == string.Empty)
            {
                surnameFill.Visibility = Visibility.Visible;
            }
            else
            {
                surnameFill.Visibility = Visibility.Hidden;
            }
        }

        private void boxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (boxName.Text == string.Empty)
            {
                nameFill.Visibility = Visibility.Visible;
            }
            else
            {
                nameFill.Visibility = Visibility.Hidden;
            }
        }

        private void boxPatronomyc_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (boxPatronomyc.Text == string.Empty)
            {
                patronomycFill.Visibility = Visibility.Visible;
            }
            else
            {
                patronomycFill.Visibility = Visibility.Hidden;
            }
        }

        private void boxbirthDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            DateTime s;
            if (!DateTime.TryParse(boxbirthDate.Text,out s))
            {
                birthDateFill.Visibility = Visibility.Visible;
            }
            else
            {
                birthDateFill.Visibility = Visibility.Hidden;
            }
        }

        private void boxPosition_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(boxPosition.Text  == string.Empty)
            {
                positionFill.Visibility = Visibility.Visible;
            }
            else
            {
                positionFill.Visibility = Visibility.Hidden;
            }
        }

        private void boxAcceptDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            DateTime s;
            if(!DateTime.TryParse(boxAcceptDate.Text,out s))
            {
                AcceptDateFill.Visibility = Visibility.Visible;
            }
            else
            {
                AcceptDateFill.Visibility = Visibility.Hidden;
            }
        }

        private void boxManaging_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(boxManaging.SelectedItem == null)
            {
                managinFill.Visibility = Visibility.Visible;
            }
            else
            {
                managinFill.Visibility = Visibility.Hidden;
            }
        }
    }
}
