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
using Microsoft.Win32;
using Microsoft.Data.Sqlite;

namespace OznakomitelnayaPraktika
{
    /// <summary>
    /// Логика взаимодействия для Window9.xaml
    /// </summary>
    public partial class Window9 : Window
    {
        public Window9(OpenFileDialog dbpath)
        {
            InitializeComponent();
            Window9.dbPath = dbpath;
            refresh(dbPath);
        }

        public static OpenFileDialog dbPath;


        private void delete_Click(object sender, RoutedEventArgs e)
        {
            using (SqliteConnection connection = new SqliteConnection($"Data Source={dbPath.FileName};Mode=ReadWrite;"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand($"DELETE FROM Managing WHERE id_managing = {((managament)ManagGrid.SelectedItem).id_managing}", connection);
                command.ExecuteNonQuery();
            }
            refresh(dbPath);
        }


        private void change_Click(object sender, RoutedEventArgs e)
        {
            using (SqliteConnection connection = new SqliteConnection($"Data Source={dbPath.FileName};Mode=ReadWrite;"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand($"UPDATE Managing SET managing_name = '{((managament)ManagGrid.SelectedItem).managing_name}'  WHERE id_managing = {((managament)ManagGrid.SelectedItem).id_managing}", connection);
                command.ExecuteNonQuery();
            }
            refresh(dbPath);
        }

        public void refresh(OpenFileDialog dbPath)
        {
            using (SqliteConnection connection = new SqliteConnection($"Data Source={dbPath.FileName};Mode=ReadWrite;"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand($"SELECT id_managing , departament_name, managing_name FROM Managing JOIN Departament ON	Departament.id_departament = Managing.id_departament_fk", connection);
                using (var reader = command.ExecuteReader())
                {
                    List<managament> managaments = new List<managament>();
                    managament dp = new managament();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dp.id_managing = reader[0].ToString();
                            dp.Departament = reader[1].ToString();
                            dp.managing_name = reader[2].ToString();
                            managaments.Add(dp);
                        }
                        ManagGrid.ItemsSource = managaments;
                    }
                }
            }
        }

        struct managament
        {
            public string id_managing { get; set; }
            public string Departament { get; set; }
            public string managing_name { get; set; }
        }
    }
}
