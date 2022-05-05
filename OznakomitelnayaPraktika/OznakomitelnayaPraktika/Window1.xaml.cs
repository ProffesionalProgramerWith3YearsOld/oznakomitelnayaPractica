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
using Microsoft.Win32;


namespace OznakomitelnayaPraktika
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private static string userId1;

        public static void ShowContext_wb(List<MainWindow.rowBuilder.employ_workbook> table, string userContext, string user_id = "-1")
        {
            Window1 win1 = new Window1();
            win1.contextGrid.ItemsSource = table;
            win1.Title = userContext;
            if (user_id != "-1")
                userId1 = user_id;
            win1.Show();
        }

        public static void ShowContext_doc(List<MainWindow.rowBuilder.employ_document> tabl, string userContext, string user_id = "-1")
        {
            Window1 win1 = new Window1();
            win1.contextGrid.ItemsSource = tabl;
            win1.Title = userContext;
            if (user_id != "-1")
                userId1 = user_id;
            win1.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.Title == "Трудовая книжка сотрудника")
                {
                    var delete_row = (MainWindow.rowBuilder.employ_workbook)contextGrid.SelectedItem;
                    MainWindow.delete_wb_rec(delete_row, this);
                }
                else
                {
                    var delete_row = (MainWindow.rowBuilder.employ_document)contextGrid.SelectedItem;
                    MainWindow.delete_doc_rec(delete_row, this);

                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.Title == "Трудовая книжка сотрудника")
                {
                    var changed_row = (MainWindow.rowBuilder.employ_workbook)contextGrid.SelectedItem;
                    MainWindow.change_wb_rec(changed_row, this);
                }
                else
                {
                    var changed_row = (MainWindow.rowBuilder.employ_document)contextGrid.SelectedItem;
                    MainWindow.change_doc_rec(changed_row, this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Текстовый файл(*.txt)|*.txt";
            saveFile.ShowDialog();

            if (saveFile.FileName.Length != 0)
            {
                var saved_grid = contextGrid.ItemsSource;

                using (StreamWriter sw = new StreamWriter(saveFile.FileName, true))
                {
                    if (this.Title == "Трудовая книжка сотрудника")
                    {
                        sw.WriteLine($"{this.Title}");
                        foreach (MainWindow.rowBuilder.employ_workbook i in saved_grid)
                        {
                            sw.WriteLine($"{i.Фамилия} {i.Имя} {i.Отчетсво} | {i.ДатаЗаписи} | {i.Управление} | {i.Отдел} | {i.Примечание}");
                        }
                    }
                    else
                    {
                        sw.WriteLine($"{this.Title}");
                        foreach (MainWindow.rowBuilder.employ_document i in saved_grid)
                        {
                            sw.WriteLine($"{i.Название_документа} | {i.Серия} {i.Номер} | {i.Дата_получения}");
                        }
                    }
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.Title == "Трудовая книжка сотрудника")
                {
                    string userId;
                    if (((List<MainWindow.rowBuilder.employ_workbook>)contextGrid.ItemsSource).Count == 0)
                    {
                        userId = userId1;
                    }
                    else
                    {
                        userId = ((List<MainWindow.rowBuilder.employ_workbook>)contextGrid.ItemsSource)[0].ид_сотрудника;
                    }
                    string[] insert = Window3.getAddInsert_wb();
                    MainWindow.add_wb_rec(userId, insert, this);
                }
                else
                {
                    string userId;
                    if (((List<MainWindow.rowBuilder.employ_document>)contextGrid.ItemsSource).Count == 0)
                    {
                        userId = userId1;
                    }
                    else
                    {
                        userId = ((List<MainWindow.rowBuilder.employ_document>)contextGrid.ItemsSource)[0].ид_сотрудника;
                    }
                    string[] insert = Window4.getAddInsert_doc();
                    MainWindow.add_doc_rec(userId, insert, this);
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (this.Title == "Трудовая книжка сотрудника")
            {
                Window10 win = new Window10();
                win.ShowDialog();
                if (win.closeFlag == true)
                {
                    List<MainWindow.rowBuilder.employ_workbook> resultList = new List<MainWindow.rowBuilder.employ_workbook>();

                    foreach (MainWindow.rowBuilder.employ_workbook i in contextGrid.ItemsSource)
                    {
                        if (i.Фамилия.Contains(win.result.Text) || i.Имя.Contains(win.result.Text) || i.Отчетсво.Contains(win.result.Text) || i.ДатаЗаписи.Contains(win.result.Text) || i.Отдел.Contains(win.result.Text) || i.Управление.Contains(win.result.Text) || i.Должность.Contains(win.result.Text)|| i.Примечание.Contains(win.result.Text))
                        {
                            resultList.Add(i);
                        }
                    }
                    contextGrid.ItemsSource = resultList;
                }
                else
                {
                    List<MainWindow.rowBuilder.employ_document> resultList = new List<MainWindow.rowBuilder.employ_document>();

                    foreach (MainWindow.rowBuilder.employ_document i in contextGrid.ItemsSource)
                    {
                        if (i.Название_документа.Contains(win.result.Text)|| i.Дата_получения.Contains(win.result.Text)||i.Номер.Contains(win.result.Text)||i.Серия.Contains(win.result.Text))
                        {
                            resultList.Add(i);
                        }
                    }
                    contextGrid.ItemsSource = resultList;
                }
            }
        }
    }
}
