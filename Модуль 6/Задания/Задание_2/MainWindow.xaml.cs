
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.Sqlite;

namespace Задание_2
{
    public partial class MainWindow : Window
    {
        string path = @"Data Source=C:\Users\ADMIN\Desktop\практика\Praktika\Модуль 6\Задания\Задание_2\Z_2.db";

        public MainWindow()
        {
            InitializeComponent();
            Show();
        }

        // Показать задачи
        void Show()
        {
            using (SqliteConnection con = new SqliteConnection(path))
            {
                con.Open();

                SqliteCommand cmd = new SqliteCommand(
                    "SELECT * FROM Tasks", con);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                Grid1.ItemsSource = dt.DefaultView;
            }
        }

        // Добавить задачу
        void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Name1.Text == "")
            {
                MessageBox.Show("Введите название!");
                return;
            }

            using (SqliteConnection con = new SqliteConnection(path))
            {
                con.Open();

                SqliteCommand cmd = new SqliteCommand(
                    "INSERT INTO Tasks (Name, Info, Status) VALUES (@n, @i, @s)", con);

                cmd.Parameters.AddWithValue("@n", Name1.Text);
                cmd.Parameters.AddWithValue("@i", Info1.Text);
                cmd.Parameters.AddWithValue("@s",
                    ((ComboBoxItem)Status1.SelectedItem).Content.ToString());

                cmd.ExecuteNonQuery();
            }

            Show();
            ClearFields();
        }

        // Выбрать задачу
        void Grid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Grid1.SelectedItem != null)
            {
                DataRowView r = (DataRowView)Grid1.SelectedItem;

                Name1.Text = r["Name"].ToString();
                Info1.Text = r["Info"].ToString();

                string s = r["Status"].ToString();

                foreach (ComboBoxItem item in Status1.Items)
                {
                    if (item.Content.ToString() == s)
                        Status1.SelectedItem = item;
                }
            }
        }

        // Изменить задачу
        void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (Grid1.SelectedItem == null)
            {
                MessageBox.Show("Выберите задачу!");
                return;
            }

            DataRowView r = (DataRowView)Grid1.SelectedItem;
            int id = Convert.ToInt32(r["Id"]);

            using (SqliteConnection con = new SqliteConnection(path))
            {
                con.Open();

                SqliteCommand cmd = new SqliteCommand(
                    "UPDATE Tasks SET Name=@n, Info=@i, Status=@s WHERE Id=@id", con);

                cmd.Parameters.AddWithValue("@n", Name1.Text);
                cmd.Parameters.AddWithValue("@i", Info1.Text);
                cmd.Parameters.AddWithValue("@s",
                    ((ComboBoxItem)Status1.SelectedItem).Content.ToString());
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }

            Show();
            ClearFields();
        }

        // Удалить задачу
        void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Grid1.SelectedItem == null)
            {
                MessageBox.Show("Выберите задачу!");
                return;
            }

            DataRowView r = (DataRowView)Grid1.SelectedItem;
            int id = Convert.ToInt32(r["Id"]);

            using (SqliteConnection con = new SqliteConnection(path))
            {
                con.Open();

                SqliteCommand cmd = new SqliteCommand(
                    "DELETE FROM Tasks WHERE Id=@id", con);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            Show();
            ClearFields();
        }

        // Очистить поля
        void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        void ClearFields()
        {
            Name1.Clear();
            Info1.Clear();
            Status1.SelectedIndex = 0;
            Grid1.SelectedItem = null;
        }
    }
}
