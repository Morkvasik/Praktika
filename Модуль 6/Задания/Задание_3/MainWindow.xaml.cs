using System.Data;
using System.Windows;
using Microsoft.Data.Sqlite;

namespace Задание_3
{
    public partial class MainWindow : Window
    {
        string path = @"Data Source=C:\Users\ADMIN\Desktop\практика\Praktika\Модуль 6\Задания\Задание_3\Z_3.db";

        public MainWindow()
        {
            InitializeComponent();
            ShowBooks("");
        }

        // Показать книги
        void ShowBooks(string s)
        {
            using (SqliteConnection con = new SqliteConnection(path))
            {
                con.Open();

                SqliteCommand cmd = new SqliteCommand(
                    "SELECT * FROM Books WHERE Name LIKE @s OR Author LIKE @s", con);

                cmd.Parameters.AddWithValue("@s", "%" + s + "%");

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                Grid1.ItemsSource = dt.DefaultView;
            }
        }

        // Найти книгу
        void Search_Click(object sender, RoutedEventArgs e)
        {
            ShowBooks(Search1.Text);
        }

        // Показать все книги
        void All_Click(object sender, RoutedEventArgs e)
        {
            Search1.Clear();
            ShowBooks("");
        }

        // Взять книгу
        void Rent_Click(object sender, RoutedEventArgs e)
        {
            if (Grid1.SelectedItem == null)
            {
                MessageBox.Show("Выберите книгу!");
                return;
            }

            DataRowView r = (DataRowView)Grid1.SelectedItem;

            if (r["Status"].ToString() == "В аренде")
            {
                MessageBox.Show("Эта книга уже выдана!");
                return;
            }

            int id = Convert.ToInt32(r["Id"]);

            using (SqliteConnection con = new SqliteConnection(path))
            {
                con.Open();

                SqliteCommand cmd = new SqliteCommand(
                    "UPDATE Books SET Status='В аренде' WHERE Id=@id", con);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            ShowBooks(Search1.Text);
            MessageBox.Show("Книга выдана!");
        }

        // Вернуть книгу
        void Return_Click(object sender, RoutedEventArgs e)
        {
            if (Grid1.SelectedItem == null)
            {
                MessageBox.Show("Выберите книгу!");
                return;
            }

            DataRowView r = (DataRowView)Grid1.SelectedItem;

            if (r["Status"].ToString() == "Доступна")
            {
                MessageBox.Show("Эта книга уже в библиотеке!");
                return;
            }

            int id = Convert.ToInt32(r["Id"]);

            using (SqliteConnection con = new SqliteConnection(path))
            {
                con.Open();

                SqliteCommand cmd = new SqliteCommand(
                    "UPDATE Books SET Status='Доступна' WHERE Id=@id", con);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            ShowBooks(Search1.Text);
            MessageBox.Show("Книга возвращена!");
        }
    }
}