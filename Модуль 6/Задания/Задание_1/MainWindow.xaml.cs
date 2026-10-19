
using System; // Основные возможности C#
using System.Data; // Работа с таблицами данных
using System.Windows; // Окна и сообщения WPF
using System.Windows.Controls; // Кнопки, таблицы и другие элементы
using Microsoft.Data.Sqlite; // Работа с базой данных SQLite

namespace Задание_1
{
    public partial class MainWindow : Window
    {
        // Строка подключения к файлу базы данных
        string path = @"Data Source=C:\Users\ADMIN\Desktop\практика\Praktika\Модуль 6\Задания\Задание_1\Z_1.db";

        // Конструктор окна — запускается при открытии программы
        public MainWindow()
        {
            InitializeComponent(); // Создаёт элементы из XAML
            Show(); // Сразу показывает сотрудников из базы
        }

        // Показывает всех сотрудников в таблице
        void Show()
        {
            // Создаём подключение к базе данных
            using (SqliteConnection con = new SqliteConnection(path))
            {
                con.Open(); // Открываем подключение

                // SQL-запрос для получения всех сотрудников
                SqliteCommand cmd = new SqliteCommand(
                    "SELECT * FROM Employees", con);

                // Создаём таблицу в памяти программы
                DataTable dt = new DataTable();

                // Заполняем таблицу результатом запроса
                dt.Load(cmd.ExecuteReader());

                // Показываем полученные данные в DataGrid
                Grid1.ItemsSource = dt.DefaultView;
            } // Здесь using автоматически закрывает подключение
        }

        // Добавляет нового сотрудника
        void Add_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, заполнены ли имя и зарплата
            if (Name1.Text == "" || Sal1.Text == "")
            {
                MessageBox.Show("Введите имя и зарплату!");
                return; // Прекращаем выполнение метода
            }

            // Проверяем, можно ли преобразовать зарплату в число
            if (!decimal.TryParse(Sal1.Text, out decimal sal))
            {
                MessageBox.Show("Неверная зарплата!");
                return;
            }

            // Подключаемся к базе данных
            using (SqliteConnection con = new SqliteConnection(path))
            {
                con.Open();

                // Запрос на добавление сотрудника
                SqliteCommand cmd = new SqliteCommand(
                    "INSERT INTO Employees (Name, Position, Salary) VALUES (@n, @p, @s)", con);

                // Передаём значения в запрос
                cmd.Parameters.AddWithValue("@n", Name1.Text);
                cmd.Parameters.AddWithValue("@p", Pos1.Text);
                cmd.Parameters.AddWithValue("@s", sal);

                cmd.ExecuteNonQuery(); // Выполняем запрос
            }

            Show(); // Обновляем таблицу
            ClearFields(); // Очищаем поля ввода
            MessageBox.Show("Сотрудник добавлен!");
        }

        // При выборе строки переносит данные сотрудника в поля
        void Grid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, выбрана ли строка
            if (Grid1.SelectedItem != null)
            {
                // Получаем данные выбранного сотрудника
                DataRowView row = (DataRowView)Grid1.SelectedItem;

                // Заполняем поля данными из выбранной строки
                Name1.Text = row["Name"].ToString();
                Pos1.Text = row["Position"].ToString();
                Sal1.Text = row["Salary"].ToString();
            }
        }

        // Изменяет данные выбранного сотрудника
        void Edit_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, выбран ли сотрудник в таблице
            if (Grid1.SelectedItem == null)
            {
                MessageBox.Show("Выберите сотрудника!");
                return;
            }

            // Проверяем заполнение обязательных полей
            if (Name1.Text == "" || Sal1.Text == "")
            {
                MessageBox.Show("Введите имя и зарплату!");
                return;
            }

            // Проверяем правильность зарплаты
            if (!decimal.TryParse(Sal1.Text, out decimal sal))
            {
                MessageBox.Show("Неверная зарплата!");
                return;
            }

            // Получаем выбранную строку и её ID
            DataRowView row = (DataRowView)Grid1.SelectedItem;
            int id = Convert.ToInt32(row["Id"]);

            // Подключаемся к базе данных
            using (SqliteConnection con = new SqliteConnection(path))
            {
                con.Open();

                // Меняем данные сотрудника с указанным ID
                SqliteCommand cmd = new SqliteCommand(
                    "UPDATE Employees SET Name=@n, Position=@p, Salary=@s WHERE Id=@id", con);

                // Передаём новые данные и ID сотрудника
                cmd.Parameters.AddWithValue("@n", Name1.Text);
                cmd.Parameters.AddWithValue("@p", Pos1.Text);
                cmd.Parameters.AddWithValue("@s", sal);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery(); // Выполняем изменение
            }

            Show(); // Обновляем таблицу
            ClearFields(); // Очищаем поля
            MessageBox.Show("Данные изменены!");
        }

        // Удаляет выбранного сотрудника
        void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, выбран ли сотрудник
            if (Grid1.SelectedItem == null)
            {
                MessageBox.Show("Выберите сотрудника!");
                return;
            }

            // Получаем ID выбранного сотрудника
            DataRowView row = (DataRowView)Grid1.SelectedItem;
            int id = Convert.ToInt32(row["Id"]);

            // Спрашиваем, точно ли нужно удалить запись
            MessageBoxResult res = MessageBox.Show("Удалить сотрудника?", "Подтверждение",MessageBoxButton.YesNo);

            // Если пользователь нажал "Нет", ничего не удаляем
            if (res == MessageBoxResult.No)
                return;

            // Подключаемся к базе
            using (SqliteConnection con = new SqliteConnection(path))
            {
                con.Open();

                // Удаляем сотрудника с выбранным ID
                SqliteCommand cmd = new SqliteCommand(
                    "DELETE FROM Employees WHERE Id=@id", con);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery(); // Выполняем удаление
            }

            Show(); // Обновляем таблицу
            ClearFields(); // Очищаем поля
            MessageBox.Show("Сотрудник удалён!");
        }

        // Обработчик нажатия кнопки "Очистить"
        void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        // Очищает поля ввода и снимает выделение строки
        void ClearFields()
        {
            Name1.Clear();
            Pos1.Clear();
            Sal1.Clear();
            Grid1.SelectedItem = null;
        }
    }
}