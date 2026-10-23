
using Microsoft.Data.Sqlite;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Задание_3
{
    public partial class MainWindow : Window
    {
        // Путь к нашей базе данных
        string db = @"C:\Users\ADMIN\Desktop\практика\Praktika\Модуль_9\задания\Задание_3\BD_Z3.db";

        public MainWindow()
        {
            InitializeComponent();

            // Сразу показываем задачи
            ShowData();
        }

        // Подключение к базе
        SqliteConnection Connect()
        {
            return new SqliteConnection("Data Source=" + db);
        }

        // Загружаем задачи из базы
        void ShowData()
        {
            using (SqliteConnection con = Connect())
            {
                con.Open();

                string sql = "SELECT * FROM Tasks ORDER BY Id";

                SqliteCommand cmd = new SqliteCommand(sql, con);

                DataTable tab = new DataTable();

                using (var r = cmd.ExecuteReader())
                {
                    tab.Load(r);
                }

                TaskTable.ItemsSource = tab.DefaultView;
            }

            // Проверяем сроки задач
            CheckDate();
        }

        // Получаем выбранный пункт из списка
        string GetText(ComboBox box)
        {
            ComboBoxItem item = box.SelectedItem as ComboBoxItem;

            if (item == null)
                return "";

            return item.Content.ToString();
        }

        // Выбираем нужный пункт в списке
        void SetText(ComboBox box, string s)
        {
            foreach (ComboBoxItem item in box.Items)
            {
                if (item.Content.ToString() == s)
                {
                    box.SelectedItem = item;
                    return;
                }
            }
        }

        // Добавление задачи
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string name = NameBox.Text.Trim();
            string opis = OpisBox.Text;
            string prior = GetText(PriorityBox);
            string status = GetText(StatusBox);

            if (name == "")
            {
                MessageBox.Show("Введи название задачи!");
                return;
            }

            if (DateBox.SelectedDate == null)
            {
                MessageBox.Show("Выбери дату выполнения!");
                return;
            }

            if (prior == "" || status == "")
            {
                MessageBox.Show("Выбери приоритет и статус!");
                return;
            }

            // Если задача завершена, ставим 100 процентов
            int proc = (int)ProgressBox.Value;

            if (status == "Завершена")
                proc = 100;

            using (SqliteConnection con = Connect())
            {
                con.Open();

                string sql = @"INSERT INTO Tasks
                    (Name, Description, Priority, DueDate, Status, Progress)
                    VALUES (@n, @o, @p, @d, @s, @v)";

                SqliteCommand cmd = new SqliteCommand(sql, con);

                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@o", opis);
                cmd.Parameters.AddWithValue("@p", prior);
                cmd.Parameters.AddWithValue("@d",
                    DateBox.SelectedDate.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@s", status);
                cmd.Parameters.AddWithValue("@v", proc);

                cmd.ExecuteNonQuery();
            }

            ShowData();
            ClearBoxes();

            MessageBox.Show("Задача добавлена!");
        }

        // Изменение задачи
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            DataRowView r = TaskTable.SelectedItem as DataRowView;

            if (r == null)
            {
                MessageBox.Show("Сначала выбери задачу в таблице!");
                return;
            }

            string name = NameBox.Text.Trim();
            string opis = OpisBox.Text;
            string prior = GetText(PriorityBox);
            string status = GetText(StatusBox);

            if (name == "")
            {
                MessageBox.Show("Введи название задачи!");
                return;
            }

            if (DateBox.SelectedDate == null)
            {
                MessageBox.Show("Выбери дату выполнения!");
                return;
            }

            if (prior == "" || status == "")
            {
                MessageBox.Show("Выбери приоритет и статус!");
                return;
            }

            int proc = (int)ProgressBox.Value;

            if (status == "Завершена")
                proc = 100;

            using (SqliteConnection con = Connect())
            {
                con.Open();

                string sql = @"UPDATE Tasks SET
                    Name=@n,
                    Description=@o,
                    Priority=@p,
                    DueDate=@d,
                    Status=@s,
                    Progress=@v
                    WHERE Id=@id";

                SqliteCommand cmd = new SqliteCommand(sql, con);

                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@o", opis);
                cmd.Parameters.AddWithValue("@p", prior);
                cmd.Parameters.AddWithValue("@d",
                    DateBox.SelectedDate.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@s", status);
                cmd.Parameters.AddWithValue("@v", proc);
                cmd.Parameters.AddWithValue("@id", r["Id"]);

                cmd.ExecuteNonQuery();
            }

            ShowData();

            MessageBox.Show("Задача изменена!");
        }

        // Удаление задачи
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView r = TaskTable.SelectedItem as DataRowView;

            if (r == null)
            {
                MessageBox.Show("Сначала выбери задачу!");
                return;
            }

            if (MessageBox.Show("Точно удалить задачу?",
                "Удаление",
                MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }

            using (SqliteConnection con = Connect())
            {
                con.Open();

                string sql = "DELETE FROM Tasks WHERE Id=@id";

                SqliteCommand cmd = new SqliteCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", r["Id"]);

                cmd.ExecuteNonQuery();
            }

            ShowData();
            ClearBoxes();

            MessageBox.Show("Задача удалена!");
        }

        // При выборе задачи заполняем поля
        private void TaskTable_SelectionChanged(
            object sender, SelectionChangedEventArgs e)
        {
            DataRowView r = TaskTable.SelectedItem as DataRowView;

            if (r == null)
                return;

            NameBox.Text = r["Name"].ToString();
            OpisBox.Text = r["Description"].ToString();

            SetText(PriorityBox, r["Priority"].ToString());
            SetText(StatusBox, r["Status"].ToString());

            DateTime d;

            if (DateTime.TryParseExact(
                r["DueDate"].ToString(),
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out d))
            {
                DateBox.SelectedDate = d;
            }

            int proc;

            if (int.TryParse(r["Progress"].ToString(), out proc))
                ProgressBox.Value = proc;
        }

        // Очищаем поля после добавления
        void ClearBoxes()
        {
            NameBox.Clear();
            OpisBox.Clear();

            PriorityBox.SelectedIndex = 1;
            StatusBox.SelectedIndex = 0;

            DateBox.SelectedDate = null;
            ProgressBox.Value = 0;

            TaskTable.SelectedItem = null;
        }

        // Проверяем сроки выполнения
        void CheckDate()
        {
            string msg = "";
            DateTime today = DateTime.Today;

            using (SqliteConnection con = Connect())
            {
                con.Open();

                string sql = "SELECT Name, DueDate, Status FROM Tasks";

                SqliteCommand cmd = new SqliteCommand(sql, con);

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        string name = r["Name"].ToString();
                        string date = r["DueDate"].ToString();
                        string status = r["Status"].ToString();

                        // Завершённые задачи не проверяем
                        if (status == "Завершена")
                            continue;

                        DateTime d;

                        if (DateTime.TryParseExact(
                            date,
                            "yyyy-MM-dd",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out d))
                        {
                            if (d.Date < today)
                            {
                                msg += "Просрочена: " + name + "\n";
                            }
                            else if ((d.Date - today).Days <= 3)
                            {
                                msg += "Скоро срок: " + name +
                                       " (" + d.ToString("dd.MM.yyyy") + ")\n";
                            }
                        }
                    }
                }
            }

            if (msg == "")
                NoticeText.Text = "Нет срочных задач.";
            else
                NoticeText.Text = msg;
        }
    }
}
