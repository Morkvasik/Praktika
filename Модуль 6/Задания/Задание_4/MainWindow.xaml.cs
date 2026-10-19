using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.Sqlite;

namespace Задание_4
{
    public partial class MainWindow : Window
    {
        string path = @"Data Source=C:\Users\ADMIN\Desktop\практика\Praktika\Модуль 6\Задания\Задание_4\Z_4.db";

        public MainWindow()
        {
            InitializeComponent();
            ShowData();
        }

        // Показать записи
        void ShowData()
        {
            using (SqliteConnection con = new SqliteConnection(path))
            {
                con.Open();

                SqliteCommand cmd = new SqliteCommand(
                    "SELECT * FROM Finance", con);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                Grid1.ItemsSource = dt.DefaultView;
            }
        }

        // Добавить запись
        void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Name1.Text == "" || Amount1.Text == "")
            {
                MessageBox.Show("Заполните название и сумму!");
                return;
            }

            double sum;

            if (!double.TryParse(Amount1.Text, out sum) || sum <= 0)
            {
                MessageBox.Show("Введите правильную сумму!");
                return;
            }

            using (SqliteConnection con = new SqliteConnection(path))
            {
                con.Open();

                SqliteCommand cmd = new SqliteCommand(
                    "INSERT INTO Finance (Name, Type, Amount, Date, Info) " +
                    "VALUES (@n, @t, @a, @d, @i)", con);

                cmd.Parameters.AddWithValue("@n", Name1.Text);
                cmd.Parameters.AddWithValue("@t",
                    ((ComboBoxItem)Type1.SelectedItem).Content.ToString());
                cmd.Parameters.AddWithValue("@a", sum);
                cmd.Parameters.AddWithValue("@d", Date1.Text);
                cmd.Parameters.AddWithValue("@i", Info1.Text);

                cmd.ExecuteNonQuery();
            }

            ShowData();
            ClearFields();
        }

        // Выбрать запись
        void Grid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Grid1.SelectedItem != null)
            {
                DataRowView r = (DataRowView)Grid1.SelectedItem;

                Name1.Text = r["Name"].ToString();
                Amount1.Text = r["Amount"].ToString();
                Date1.Text = r["Date"].ToString();
                Info1.Text = r["Info"].ToString();

                string s = r["Type"].ToString();

                foreach (ComboBoxItem item in Type1.Items)
                {
                    if (item.Content.ToString() == s)
                        Type1.SelectedItem = item;
                }
            }
        }

        // Изменить запись
        void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (Grid1.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись!");
                return;
            }

            double sum;

            if (Name1.Text == "" ||
                !double.TryParse(Amount1.Text, out sum) || sum <= 0)
            {
                MessageBox.Show("Проверьте название и сумму!");
                return;
            }

            DataRowView r = (DataRowView)Grid1.SelectedItem;
            int id = Convert.ToInt32(r["Id"]);

            using (SqliteConnection con = new SqliteConnection(path))
            {
                con.Open();

                SqliteCommand cmd = new SqliteCommand(
                    "UPDATE Finance SET Name=@n, Type=@t, Amount=@a, " +
                    "Date=@d, Info=@i WHERE Id=@id", con);

                cmd.Parameters.AddWithValue("@n", Name1.Text);
                cmd.Parameters.AddWithValue("@t",
                    ((ComboBoxItem)Type1.SelectedItem).Content.ToString());
                cmd.Parameters.AddWithValue("@a", sum);
                cmd.Parameters.AddWithValue("@d", Date1.Text);
                cmd.Parameters.AddWithValue("@i", Info1.Text);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }

            ShowData();
            ClearFields();
        }

        // Удалить запись
        void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Grid1.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись!");
                return;
            }

            DataRowView r = (DataRowView)Grid1.SelectedItem;
            int id = Convert.ToInt32(r["Id"]);

            using (SqliteConnection con = new SqliteConnection(path))
            {
                con.Open();

                SqliteCommand cmd = new SqliteCommand(
                    "DELETE FROM Finance WHERE Id=@id", con);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            ShowData();
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
            Amount1.Clear();
            Date1.Clear();
            Info1.Clear();
            Type1.SelectedIndex = 0;
            Grid1.SelectedItem = null;
        }
    }
}