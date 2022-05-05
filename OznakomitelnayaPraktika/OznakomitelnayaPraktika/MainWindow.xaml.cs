using Microsoft.Data.Sqlite;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;

namespace OznakomitelnayaPraktika
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        public static OpenFileDialog dbPath = new OpenFileDialog();


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dbPath.Filter = "Файл БД (*.db)|*.db| Все файлы|*.*";
                dbPath.ShowDialog();
                if (dbPath.FileName != string.Empty) {
                    Refresh(dbPath, this.myGrid);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                int flag = Window2.GetAddFlag();
                switch (flag)
                {
                    case 0:
                        break;
                    case 1:
                        MainWindow.addEmploy();
                        break;
                    case 2:
                        MainWindow.addDepartament();
                        break;
                    case 3:
                        MainWindow.addManaging();
                        break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }


        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Window2 window = new Window2();
            window.EmployButton.Visibility = Visibility.Hidden;
            window.ShowDialog();
            switch (window.flagMode)
            {
                case 2:
                    Window8 win = new Window8(dbPath);
                    win.ShowDialog();
                    break;
                case 3:
                    Window9 win1 = new Window9(dbPath);
                    win1.ShowDialog();
                    break;
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Window10 win = new Window10();
            win.ShowDialog();

            List<rowBuilder.start_view> resultList = new List<rowBuilder.start_view>();

            foreach (rowBuilder.start_view i in myGrid.ItemsSource)
            {

            }

        }


        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Window10 win = new Window10();
            
            win.ShowDialog();
            if (win.closeFlag == true)
            {
                List<rowBuilder.start_view> resultList = new List<rowBuilder.start_view>();

                foreach (rowBuilder.start_view i in myGrid.ItemsSource)
                {
                    if (i.Фамилия.Contains(win.result.Text) || i.Имя.Contains(win.result.Text) || i.Отчетсво.Contains(win.result.Text) || i.Дата_рождения.Contains(win.result.Text) || i.Отдел.Contains(win.result.Text) || i.Управление.Contains(win.result.Text) || i.Должность.Contains(win.result.Text))
                    {
                        resultList.Add(i);
                    }
                }
                this.myGrid.ItemsSource = resultList;
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Refresh(dbPath,myGrid);
        }

        private void myGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(((rowBuilder.start_view)myGrid.SelectedItem).ИД);
        }

        private void Show_employ_workbook(object sender, RoutedEventArgs e)
        {
            try
            {
                showEmploy_wb(((rowBuilder.start_view)myGrid.SelectedItem).ИД, dbPath);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Show_employ_document(object sender, RoutedEventArgs e)
        {
            try
            {
                showEmploy_doc(((rowBuilder.start_view)myGrid.SelectedItem).ИД, dbPath);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_employ_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string delId = ((rowBuilder.start_view)myGrid.SelectedItem).ИД;
                MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить сотрудника", "Удаление", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (var connect = new SqliteConnection($"Data Source={dbPath.FileName};Mode=ReadWrite;"))
                    {
                        connect.Open();
                        var command = new SqliteCommand($"DELETE FROM Employ WHERE id_employ = {delId}", connect);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
        //-------------------------------------------------------------------------------------------




        public static void Refresh(OpenFileDialog dbpath, DataGrid myGrid)
        {
            using (var connect = new SqliteConnection($"Data Source={dbpath.FileName};Mode=ReadWrite;"))
            {
                connect.Open();
                var command = new SqliteCommand("SELECT * FROM start_view", connect);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        rowBuilder.start_view row = new rowBuilder.start_view();
                        List<rowBuilder.start_view> table = new List<rowBuilder.start_view>();
                        while (reader.Read())
                        {
                            bool flag = true;
                            foreach (rowBuilder.start_view i in table.ToArray())
                            {
                                if (i.ИД == reader[0].ToString() & DateTime.Parse(i.Дата) < DateTime.Parse(reader[7].ToString()))
                                {
                                    table.Remove(i);
                                    flag = true;
                                }
                                else if (i.ИД == reader[0].ToString() & DateTime.Parse(i.Дата) >= DateTime.Parse(reader[7].ToString()))
                                {
                                    flag = false;
                                }
                            }

                            if (flag == true)
                            {
                                row.ИД = reader[0].ToString();
                                row.Фамилия = reader[1].ToString();
                                row.Имя = reader[2].ToString();
                                row.Отчетсво = reader[3].ToString();
                                row.Дата_рождения = reader[4].ToString();
                                row.Отдел = reader[5].ToString();
                                row.Управление = reader[6].ToString();
                                row.Дата = reader[7].ToString();
                                row.Должность = reader[8].ToString();
                                table.Add(row);
                            }
                        }
                        myGrid.ItemsSource = table;
                    }
                }
            }
        }

        public static void showEmploy_wb(string id, OpenFileDialog dbpath)
        {
            using (var connect = new SqliteConnection($"Data Source={dbpath.FileName};Mode=ReadWrite;"))
            {
                connect.Open();
                var command = new SqliteCommand($"SELECT id_employee_workbook_record,id_employ, surname,name ,patronymic,departament_name,managing_name,record_date, note ,position FROM Employee_workbook_record JOIN Employ ON Employ.id_employ = {id} JOIN Managing ON Employee_workbook_record.to_managing_fk = Managing.id_managing AND Employee_workbook_record.id_employ_fk = {id} JOIN Departament ON  Departament.id_departament = Managing.id_departament_fk AND Employee_workbook_record.id_employ_fk = {id}", connect);
                using (var reader = command.ExecuteReader())
                {
                    List<rowBuilder.employ_workbook> table = new List<rowBuilder.employ_workbook>();
                    if (reader.HasRows)
                    {
                        rowBuilder.employ_workbook row = new rowBuilder.employ_workbook();
                        while (reader.Read())
                        {
                            row.ид_записи = reader[0].ToString();
                            row.ид_сотрудника = reader[1].ToString();
                            row.Фамилия = reader[2].ToString();
                            row.Имя = reader[3].ToString();
                            row.Отчетсво = reader[4].ToString();
                            row.Отдел = reader[5].ToString();
                            row.Управление = reader[6].ToString();
                            row.ДатаЗаписи = reader[7].ToString();
                            row.Примечание = reader[8].ToString();
                            row.Должность = reader[9].ToString();
                            table.Add(row);
                        }
                        Window1.ShowContext_wb(table, "Трудовая книжка сотрудника");
                    }
                    else
                    {
                        MessageBox.Show("Сотрудник не имеет записи в трудовой");
                        Window1.ShowContext_wb(table, "Трудовая книжка сотрудника", id);
                    }
                }
            }
        }

        public static void showEmploy_doc(string id, OpenFileDialog dbpath)
        {
            using (var connect = new SqliteConnection($"Data Source={ dbpath.FileName};Mode=ReadWrite;"))
            {
                connect.Open();
                var command = new SqliteCommand($"SELECT id_employ,id_employ_document,document_name,issue_date,serial,number FROM Employ_document JOIN Employ ON Employ_document.id_employ_fk = {id} AND Employ.id_employ = {id}", connect);
                using (var reader = command.ExecuteReader())
                {
                    List<rowBuilder.employ_document> table = new List<rowBuilder.employ_document>();
                    if (reader.HasRows)
                    {
                        
                        rowBuilder.employ_document row = new rowBuilder.employ_document();
                        while (reader.Read())
                        {
                            row.ид_сотрудника = reader[0].ToString();
                            row.ид_документа = reader[1].ToString();
                            row.Название_документа = reader[2].ToString();
                            row.Дата_получения = reader[3].ToString();
                            row.Серия = reader[4].ToString();
                            row.Номер = reader[5].ToString();
                            table.Add(row);
                        }
                        Window1.ShowContext_doc(table, "Документы сотрудника");
                    }
                    else
                    {
                        MessageBox.Show("Сотрудник не имеет документов");
                        Window1.ShowContext_doc(table, "Документы сотрудника",id);
                    }

                }
            }
        }


        public static void change_wb_rec(rowBuilder.employ_workbook changed_row, Window1 win)
        {
            using (var connect = new SqliteConnection($"Data Source={dbPath.FileName};Mode=ReadWrite;"))
            {
                DateTime s;
                if (DateTime.TryParse(changed_row.ДатаЗаписи, out s))
                {
                    connect.Open();
                    var command = new SqliteCommand($"UPDATE Employee_workbook_record SET record_date = '{changed_row.ДатаЗаписи}' ,note = '{changed_row.Примечание}' , position = '{changed_row.Должность}' WHERE Employee_workbook_record.id_employee_workbook_record = '{changed_row.ид_записи}'", connect);
                    command.ExecuteNonQuery();
                    win.Close();
                }
                else
                {
                    MessageBox.Show("Некорректное время");
                }
            }
        }
        public static void delete_wb_rec(rowBuilder.employ_workbook delete_row,Window1 win)
        {
            using (var connect = new SqliteConnection($"Data Source={dbPath.FileName};Mode=ReadWrite;"))
            {
                connect.Open();
                var command = new SqliteCommand($"DELETE FROM Employee_workbook_record WHERE Employee_workbook_record.id_employee_workbook_record = '{delete_row.ид_записи}'", connect);
                command.ExecuteNonQuery();
                win.Close();
            }
        }

        public static void add_wb_rec(string userId,string[] insert, Window1 win)
        {
            try
            {
                using (var connect = new SqliteConnection($"Data Source={dbPath.FileName};Mode=ReadWrite;"))
                {
                    connect.Open();
                    var command = new SqliteCommand($"INSERT INTO Employee_workbook_record (id_employ_fk, to_managing_fk, record_date, note, position) VALUES ({userId},'{insert[0]}','{insert[1]}','{insert[2]}','{insert[3]}')", connect);
                    command.ExecuteNonQuery();
                    win.Close();
                }
            }
            catch(Exception ex)
            {
              //  MessageBox.Show(ex.Message);
            }
        }

        public static void change_doc_rec(rowBuilder.employ_document changed_row, Window1 win)
        {
            using (var connect = new SqliteConnection($"Data Source={dbPath.FileName};Mode=ReadWrite;"))
            {
                connect.Open();
                DateTime s;
                if (DateTime.TryParse(changed_row.Дата_получения,out s)) {
                    var command = new SqliteCommand($"UPDATE Employ_document SET document_name = '{changed_row.Название_документа}' , issue_date = '{changed_row.Дата_получения}', serial = '{changed_row.Серия}', number = '{changed_row.Номер}' WHERE Employ_document.id_employ_document = '{changed_row.ид_документа}'", connect);
                    command.ExecuteNonQuery();
                    win.Close();
                }
                else
                {
                    MessageBox.Show("Некорректное время");
                }
            }
        }

        public static void delete_doc_rec(rowBuilder.employ_document delete_row, Window1 win)
        {
            using (var connect = new SqliteConnection($"Data Source={dbPath.FileName};Mode=ReadWrite;"))
            {
                connect.Open();
                var command = new SqliteCommand($"DELETE FROM Employ_document WHERE Employ_document.id_employ_document ='{delete_row.ид_документа}' ", connect);
                command.ExecuteNonQuery();
                win.Close();
            }
        }

        public static void add_doc_rec(string userId, string[] insert, Window1 win)
        {
            try
            {
                using (var connect = new SqliteConnection($"Data Source={dbPath.FileName};Mode=ReadWrite;"))
                {
                    connect.Open();
                    var command = new SqliteCommand($"INSERT INTO Employ_document (id_employ_fk,document_name,issue_date,serial,number) VALUES ({userId},'{insert[0]}','{insert[1]}','{insert[2]}','{insert[3]}')",connect);
                    command.ExecuteNonQuery();
                    win.Close();
                }
            }
            catch(Exception ex)
            {
              //  MessageBox.Show(ex.Message);
            }
        }

        public static void addEmploy()
        {
            try
            {
                Window5 window5 = new Window5();
                window5.boxManaging.ItemsSource = getManagingList();
                window5.ShowDialog();
                if (window5.closeFlag == true)
                {
                    string idEmploy = string.Empty;
                    using (SqliteConnection connect = new SqliteConnection($"Data Source={dbPath.FileName};Mode=ReadWrite;"))
                    {
                        connect.Open();
                        SqliteCommand comand = new SqliteCommand($"INSERT INTO Employ (surname,name,patronymic,date_of_birth) VALUES ('{window5.boxSurame.Text}','{window5.boxName.Text}','{window5.boxPatronomyc.Text}','{window5.boxbirthDate.Text}')", connect);
                        comand.ExecuteNonQuery();
                        comand.CommandText = "SELECT MAX(id_employ) FROM Employ";

                        using (var reader = comand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                idEmploy = reader[0].ToString();
                            }
                        }
                        comand.CommandText = $"INSERT INTO Employee_workbook_record (id_employ_fk, to_managing_fk, record_date, note, position) VALUES ({idEmploy},{window5.boxManaging.SelectedItem.ToString().Split('|')[0]},'{window5.boxAcceptDate.Text}','{window5.boxNote.Text}','{window5.boxPosition.Text}')";
                        comand.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void addDepartament()
        {
            Window6 win = new Window6();
            win.ShowDialog();
            if (win.closeFlag == true)
            {
                using (SqliteConnection connect = new SqliteConnection($"Data Source={dbPath.FileName};Mode=ReadWrite;"))
                {
                    connect.Open();
                    SqliteCommand command = new SqliteCommand($"INSERT INTO Departament (departament_name) VALUES ('{win.boxDepName.Text}')",connect);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void addManaging()
        {
            Window7 win = new Window7();
            win.boxDepName.ItemsSource = getDepartamentList();
            win.ShowDialog();
            if(win.closeFlag == true)
            {
                using (SqliteConnection connect = new SqliteConnection ($"Data Source={dbPath.FileName};Mode=ReadWrite;"))
                {
                    connect.Open();
                    SqliteCommand command = new SqliteCommand($"INSERT INTO Managing (id_departament_fk,managing_name) VALUES ({win.boxDepName.SelectedItem.ToString().Split('|')[0]},'{win.boxManagName.Text}')",connect);
                    command.ExecuteNonQuery();
                }
            }

        }

        public static List<string> getManagingList()
        {
            List<string> managList = new List<string>();
            using (var connect = new SqliteConnection($"Data Source={MainWindow.dbPath.FileName};Mode=ReadWrite;"))
            {
                connect.Open();
                SqliteCommand command = new SqliteCommand("SELECT id_managing, managing_name FROM Managing", connect);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        string managingi;
                        while (reader.Read())
                        {
                            managingi = reader[0].ToString() + "|" + reader[1].ToString();
                            managList.Add(managingi);
                        }
                        return managList;
                    }
                    return managList;
                }
            }
        }

        public static List<string> getDepartamentList()
        {
            List<string> depList = new List<string>();
            using (var connect = new SqliteConnection($"Data Source={MainWindow.dbPath.FileName};Mode=ReadWrite;"))
            {
                connect.Open();
                SqliteCommand command = new SqliteCommand($"SELECT * FROM Departament", connect);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows) {
                        string departamen;
                        while (reader.Read())
                        {
                            departamen = reader[0].ToString() + "|" + reader[1].ToString();
                            depList.Add(departamen);
                        }
                        return depList;
                    }
                    return depList;
                }
            }

        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Текстовый файл(*.txt)|*.txt";
            saveFile.ShowDialog();

            if (saveFile.FileName.Length != 0)
            {
                var saved_grid = myGrid.ItemsSource;

                using (StreamWriter sw = new StreamWriter(saveFile.FileName, true))
                {
                        sw.WriteLine("Все сотрудники");
                        foreach (MainWindow.rowBuilder.start_view i in saved_grid)
                        {
                            sw.WriteLine($"{i.Фамилия}  {i.Имя} {i.Отчетсво} | {i.Дата_рождения}| {i.Отдел}| {i.Управление} | {i.Дата} | {i.Должность}");
                        }
                }
            }
        }



        public class rowBuilder
        {
            public struct start_view
            {
                public string ИД { get; set; }
                public string Фамилия { get; set; }
                public string Имя { get; set; }
                public string Отчетсво { get; set; }
                public string Дата_рождения { get; set; }
                public string Отдел { get; set; }
                public string Управление { get; set; }
                public string Дата { get; set; }
                public string Должность { get; set; }
            }

            public struct employ_workbook
            {
                public string ид_записи { get; set; }
                public string ид_сотрудника { get; set; }
                public string Фамилия { get; set; }
                public string Имя { get; set; }
                public string Отчетсво { get; set; }
                public string Отдел { get; set; }
                public string Управление { get; set; }
                public string ДатаЗаписи { get; set; }
                public string Примечание { get; set; }
                public string Должность { get; set; }
            }

            public struct employ_document
            {
                public string ид_сотрудника { get; set; }
                public string ид_документа { get; set; }
                public string Название_документа { get; set; }
                public string Дата_получения { get; set; }
                public string Серия { get; set; }
                public string Номер { get; set; }

            }

        }
    }
}