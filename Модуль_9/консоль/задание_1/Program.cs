using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Student
{
    public string name;
    public int age;
    public string gruppa;
}

class Program
{
    static List<Student> spisok = new List<Student>();

    static void Main()
    {
        load();

        while (true)
        {
            clear();

            Console.WriteLine("Список студентов\n");
            Console.WriteLine("1. Показать студентов");
            Console.WriteLine("2. Добавить студента");
            Console.WriteLine("3. Удалить студента");
            Console.WriteLine("4. Изменить данные студента");
            Console.WriteLine("5. Найти студента");
            Console.WriteLine("6. Сортировать список");
            Console.WriteLine("7. Сохранить данные");
            Console.WriteLine("0. Выход");
            Console.Write("\nВыберите действие: ");

            string a = Console.ReadLine();

            if (a == "1") show();
            else if (a == "2") add();
            else if (a == "3") del();
            else if (a == "4") edit();
            else if (a == "5") find();
            else if (a == "6") sort();
            else if (a == "7")
            {
                save();
                Console.WriteLine("Данные сохранены.");
            }
            else if (a == "0")
            {
                save();
                Console.WriteLine("Программа завершена.");
                break;
            }
            else
            {
                Console.WriteLine("Неверный пункт меню.");
            }

            Console.WriteLine("\nНажмите Enter, чтобы вернуться в меню.");
            Console.ReadLine();
        }
    }

    // Очистка консоли
    static void clear()
    {
        Console.Clear();
    }

    // Показать список студентов
    static void show()
    {
        Console.WriteLine("\nСписок студентов:");

        if (spisok.Count == 0)
        {
            Console.WriteLine("Список пока пуст.");
            return;
        }

        for (int i = 0; i < spisok.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + spisok[i].name + ", возраст: " + spisok[i].age + ", группа: " + spisok[i].gruppa);
        }
    }

    // Добавить студента
    static void add()
    {
        Console.WriteLine("\nДобавление студента");

        Student s = new Student();

        Console.Write("Введите имя и фамилию: ");
        s.name = Console.ReadLine();

        Console.Write("Введите возраст: ");
        s.age = int.Parse(Console.ReadLine());

        Console.Write("Введите группу: ");
        s.gruppa = Console.ReadLine();

        spisok.Add(s);
        save();

        Console.WriteLine("Студент добавлен.");
    }

    // Удалить студента
    static void del()
    {
        Console.WriteLine("\nУдаление студента");
        show();

        if (spisok.Count == 0)
            return;

        Console.Write("Введите номер студента: ");
        int a = int.Parse(Console.ReadLine());

        if (a > 0 && a <= spisok.Count)
        {
            spisok.RemoveAt(a - 1);
            save();
            Console.WriteLine("Студент удалён.");
        }
        else
        {
            Console.WriteLine("Такого студента нет.");
        }
    }

    // Изменить данные студента
    static void edit()
    {
        Console.WriteLine("\nИзменение данных");
        show();

        if (spisok.Count == 0)
            return;

        Console.Write("Введите номер студента: ");
        int a = int.Parse(Console.ReadLine());

        if (a > 0 && a <= spisok.Count)
        {
            Console.Write("Новое имя и фамилия: ");
            spisok[a - 1].name = Console.ReadLine();

            Console.Write("Новый возраст: ");
            spisok[a - 1].age = int.Parse(Console.ReadLine());

            Console.Write("Новая группа: ");
            spisok[a - 1].gruppa = Console.ReadLine();

            save();
            Console.WriteLine("Данные изменены.");
        }
        else
        {
            Console.WriteLine("Такого студента нет.");
        }
    }

    // Найти студента
    static void find()
    {
        Console.WriteLine("\nПоиск студента");
        Console.Write("Введите имя или фамилию: ");

        string a = Console.ReadLine();
        bool b = false;

        foreach (Student s in spisok)
        {
            if (s.name.Contains(a, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(s.name + ", возраст: " + s.age + ", группа: " + s.gruppa);

                b = true;
            }
        }

        if (!b)
            Console.WriteLine("Студент не найден.");
    }

    // Сортировка студентов
    static void sort()
    {
        Console.WriteLine("\nСортировка списка");
        Console.WriteLine("1. По имени");
        Console.WriteLine("2. По возрасту");
        Console.WriteLine("3. По группе");
        Console.Write("Выберите вариант: ");

        string a = Console.ReadLine();

        if (a == "1")
            spisok = spisok.OrderBy(s => s.name).ToList();
        else if (a == "2")
            spisok = spisok.OrderBy(s => s.age).ToList();
        else if (a == "3")
            spisok = spisok.OrderBy(s => s.gruppa).ToList();
        else
        {
            Console.WriteLine("Неверный вариант.");
            return;
        }

        save();
        Console.WriteLine("Список отсортирован.");
        show();
    }

    // Сохранить данные в файл
    static void save()
    {
        using (StreamWriter w = new StreamWriter("students.txt"))
        {
            foreach (Student s in spisok)
            {
                w.WriteLine(s.name + "|" + s.age + "|" + s.gruppa);
            }
        }
    }

    // Загрузить данные из файла
    static void load()
    {
        if (File.Exists("students.txt"))
        {
            foreach (string a in File.ReadAllLines("students.txt"))
            {
                string[] b = a.Split('|');

                if (b.Length == 3)
                {
                    Student s = new Student();
                    s.name = b[0];
                    s.age = int.Parse(b[1]);
                    s.gruppa = b[2];

                    spisok.Add(s);
                }
            }
        }
    }
}
