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
    /// Логика взаимодействия для Window8.xaml
    /// </summary>
    public partial class Window8 : Window
    {
        public Window8(OpenFileDialog dbPath)
        {
            InitializeComponent();
            Window8.dbpath = dbPath;
            refresh(dbpath);
        }

        public static OpenFileDialog dbpath;

        private void change_Click(object sender, RoutedEventArgs e)
        {
            using (SqliteConnection connection = new SqliteConnection($"Data Source={dbpath.FileName};Mode=ReadWrite;"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand($"UPDATE Departament SET departament_name ='{((Departament)depGrid.SelectedItem).departament_name}'  WHERE id_departament = {((Departament)depGrid.SelectedItem).id_departament}", connection);
                command.ExecuteNonQuery();
            }
            refresh(dbpath);
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            using (SqliteConnection connection = new SqliteConnection($"Data Source={dbpath.FileName};Mode=ReadWrite;"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand($"DELETE FROM Departament WHERE id_departament = {((Departament)depGrid.SelectedItem).id_departament}", connection);
                command.ExecuteNonQuery();
            }
            refresh(dbpath);
        }

        public  void refresh(OpenFileDialog dbPath)
        {
            using (SqliteConnection connection = new SqliteConnection($"Data Source={dbPath.FileName};Mode=ReadWrite;"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand($"SELECT * FROM Departament", connection);
                using (var reader = command.ExecuteReader())
                {
                    List<Departament> departaments = new List<Departament>();
                    Departament dp = new Departament();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dp.id_departament = reader[0].ToString();
                            dp.departament_name = reader[1].ToString();
                            departaments.Add(dp);
                        }
                        depGrid.ItemsSource = departaments;
                    }
                }
            }
        }

        struct Departament
        {
            public string id_departament { get; set; }
            public string departament_name { get; set; }
        }
    }
}
