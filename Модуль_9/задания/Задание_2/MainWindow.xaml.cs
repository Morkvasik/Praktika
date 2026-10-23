
using System;
using System.Data;
using System.IO;
using System.Windows;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;

namespace Задание_2
{
    public partial class MainWindow : Window
    {
        // Путь к нашей базе данных
        string db = @"C:\Users\ADMIN\Desktop\практика\Praktika\Модуль_9\задания\Задание_2\BD_Z2.db";

        // Какой раздел сейчас открыт
        string page = "Проекты";

        public MainWindow()
        {
            InitializeComponent();

            // При запуске показываем проекты
            ShowData();
        }

        // Подключение к базе данных
        SqliteConnection Connect()
        {
            return new SqliteConnection("Data Source=" + db + ";Foreign Keys=True");
        }

        // Показываем данные в таблице
        void ShowData()
        {
            using (SqliteConnection con = Connect())
            {
                con.Open();

                string sql = "";

                if (page == "Проекты")
                    sql = "SELECT * FROM Projects";

                if (page == "Сотрудники")
                    sql = "SELECT * FROM Employees";

                if (page == "Задачи")
                    sql = "SELECT * FROM Tasks";

                if (page == "Отчёты")
                {
                    sql = @"SELECT Projects.Name AS Проект,
                                   Tasks.Name AS Задача,
                                   Tasks.Status AS Статус,
                                   Tasks.Progress AS Выполнение
                            FROM Tasks
                            JOIN Projects ON Tasks.ProjectId = Projects.Id";
                }

                SqliteCommand cmd = new SqliteCommand(sql, con);
                DataTable tab = new DataTable();

                using (var reader = cmd.ExecuteReader())
                {
                    tab.Load(reader);
                }

                // Настраиваем таблицу под выбранный раздел
                MainTable.Columns.Clear();
                MainTable.AutoGenerateColumns = true;
                MainTable.ItemsSource = tab.DefaultView;
                MainTable.IsReadOnly = true;

                PageTitle.Text = page;
            }
        }

        // Открываем проекты
        private void Projects_Click(object sender, RoutedEventArgs e)
        {
            page = "Проекты";
            ShowData();
        }

        // Открываем задачи
        private void Tasks_Click(object sender, RoutedEventArgs e)
        {
            page = "Задачи";
            ShowData();
        }

        // Открываем сотрудников
        private void Employees_Click(object sender, RoutedEventArgs e)
        {
            page = "Сотрудники";
            ShowData();
        }

        // Открываем отчёт
        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            page = "Отчёты";
            ShowData();
        }

        // Добавление записи
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            using (SqliteConnection con = Connect())
            {
                con.Open();

                string sql = "";
                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = con;

                if (page == "Проекты")
                {
                    string name = Interaction.InputBox("Название проекта:");
                    if (name == "") return;

                    string opis = Interaction.InputBox("Описание проекта:");

                    sql = "INSERT INTO Projects (Name, Description) VALUES (@n, @o)";
                    cmd.Parameters.AddWithValue("@n", name);
                    cmd.Parameters.AddWithValue("@o", opis);
                }
                else if (page == "Сотрудники")
                {
                    string name = Interaction.InputBox("Имя сотрудника:");
                    if (name == "") return;

                    string dol = Interaction.InputBox("Должность:");

                    sql = "INSERT INTO Employees (Name, Position) VALUES (@n, @d)";
                    cmd.Parameters.AddWithValue("@n", name);
                    cmd.Parameters.AddWithValue("@d", dol);
                }
                else if (page == "Задачи")
                {
                    string name = Interaction.InputBox("Название задачи:");
                    if (name == "") return;

                    string pro = Interaction.InputBox("Номер проекта:");
                    string emp = Interaction.InputBox("Номер сотрудника (или 0):");
                    string opis = Interaction.InputBox("Описание задачи:");

                    int proId, empId;

                    if (!int.TryParse(pro, out proId) || proId <= 0)
                    {
                        MessageBox.Show("Неверный номер проекта!");
                        return;
                    }

                    if (!int.TryParse(emp, out empId) || empId < 0)
                    {
                        MessageBox.Show("Неверный номер сотрудника!");
                        return;
                    }

                    sql = @"INSERT INTO Tasks
                            (ProjectId, EmployeeId, Name, Description, Status, Progress)
                            VALUES (@p, @e, @n, @o, @s, @v)";

                    cmd.Parameters.AddWithValue("@p", proId);
                    cmd.Parameters.AddWithValue("@e", empId == 0 ? DBNull.Value : (object)empId);
                    cmd.Parameters.AddWithValue("@n", name);
                    cmd.Parameters.AddWithValue("@o", opis);
                    cmd.Parameters.AddWithValue("@s", "Новая");
                    cmd.Parameters.AddWithValue("@v", 0);
                }
                else
                {
                    MessageBox.Show("В этом разделе добавление недоступно.");
                    return;
                }

                cmd.CommandText = sql;

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Запись добавлена!");
                }
                catch
                {
                    MessageBox.Show("Ошибка! Проверь введённые данные и номера записей.");
                }
            }

            ShowData();
        }

        // Изменение записи
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            DataRowView r = MainTable.SelectedItem as DataRowView;

            if (r == null)
            {
                MessageBox.Show("Сначала выбери запись в таблице!");
                return;
            }

            using (SqliteConnection con = Connect())
            {
                con.Open();

                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = con;

                if (page == "Проекты")
                {
                    string name = Interaction.InputBox("Название проекта:", "", r["Name"].ToString());
                    string opis = Interaction.InputBox("Описание:", "", r["Description"].ToString());

                    cmd.CommandText = "UPDATE Projects SET Name=@n, Description=@o WHERE Id=@id";
                    cmd.Parameters.AddWithValue("@n", name);
                    cmd.Parameters.AddWithValue("@o", opis);
                    cmd.Parameters.AddWithValue("@id", r["Id"]);
                }
                else if (page == "Сотрудники")
                {
                    string name = Interaction.InputBox("Имя сотрудника:", "", r["Name"].ToString());
                    string dol = Interaction.InputBox("Должность:", "", r["Position"].ToString());

                    cmd.CommandText = "UPDATE Employees SET Name=@n, Position=@d WHERE Id=@id";
                    cmd.Parameters.AddWithValue("@n", name);
                    cmd.Parameters.AddWithValue("@d", dol);
                    cmd.Parameters.AddWithValue("@id", r["Id"]);
                }
                else if (page == "Задачи")
                {
                    string name = Interaction.InputBox("Название задачи:", "", r["Name"].ToString());
                    string status = Interaction.InputBox("Статус задачи:", "", r["Status"].ToString());
                    string proc = Interaction.InputBox("Выполнение от 0 до 100:", "", r["Progress"].ToString());

                    int num;

                    if (!int.TryParse(proc, out num) || num < 0 || num > 100)
                    {
                        MessageBox.Show("Выполнение должно быть от 0 до 100!");
                        return;
                    }

                    cmd.CommandText = @"UPDATE Tasks
                                        SET Name=@n, Status=@s, Progress=@v
                                        WHERE Id=@id";

                    cmd.Parameters.AddWithValue("@n", name);
                    cmd.Parameters.AddWithValue("@s", status);
                    cmd.Parameters.AddWithValue("@v", num);
                    cmd.Parameters.AddWithValue("@id", r["Id"]);
                }
                else
                {
                    MessageBox.Show("Отчёт нельзя изменить.");
                    return;
                }

                cmd.ExecuteNonQuery();
            }

            ShowData();
            MessageBox.Show("Запись изменена!");
        }

        // Удаление записи
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView r = MainTable.SelectedItem as DataRowView;

            if (r == null)
            {
                MessageBox.Show("Сначала выбери запись в таблице!");
                return;
            }

            if (page == "Отчёты")
            {
                MessageBox.Show("В отчёте нельзя удалять записи.");
                return;
            }

            if (MessageBox.Show("Точно удалить запись?", "Удаление",
                MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            using (SqliteConnection con = Connect())
            {
                con.Open();

                string sql = "";

                if (page == "Проекты")
                    sql = "DELETE FROM Projects WHERE Id=@id";

                if (page == "Сотрудники")
                    sql = "DELETE FROM Employees WHERE Id=@id";

                if (page == "Задачи")
                    sql = "DELETE FROM Tasks WHERE Id=@id";

                SqliteCommand cmd = new SqliteCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", r["Id"]);
                cmd.ExecuteNonQuery();
            }

            ShowData();
            MessageBox.Show("Запись удалена!");
        }
    }
}
