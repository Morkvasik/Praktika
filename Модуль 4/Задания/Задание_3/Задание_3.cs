using System;

// Создаем интерфейс студента
interface IStudent
{
    double GetAverage();
    int GetCourse();
}

// Класс студента
class Student : IStudent
{
    public string Name;
    public int Course;
    public int Grade1;
    public int Grade2;
    public int Grade3;
    public int Grade4;

    public Student(string name, int course, int g1, int g2, int g3, int g4)
    {
        Name = name;
        Course = course;
        Grade1 = g1;
        Grade2 = g2;
        Grade3 = g3;
        Grade4 = g4;
    }

    // Считаем средний балл
    public double GetAverage()
    {
        return (Grade1 + Grade2 + Grade3 + Grade4) / 3.0;
    }

    // Возвращаем курс студента
    public int GetCourse()
    {
        return Course;
    }
}

class Задание_3
{
    static void Main()
    {
        // Создаем трех студентов
        IStudent student1 = new Student("Иван", 1, 8, 9, 6, 6);
        IStudent student2 = new Student("Анна", 2, 10, 10, 10, 8);
        IStudent student3 = new Student("Пётр", 3, 5, 6, 6, 5);

        // Выводим информацию о студентах
        Console.WriteLine("Студент: Иван");
        Console.WriteLine("Курс: " + student1.GetCourse());
        Console.WriteLine("Средний балл: " + student1.GetAverage().ToString("F2"));

        Console.WriteLine("\nСтудент: Анна");
        Console.WriteLine("Курс: " + student2.GetCourse());
        Console.WriteLine("Средний балл: " + student2.GetAverage().ToString("F2"));

        Console.WriteLine("\nСтудент: Пётр");
        Console.WriteLine("Курс: " + student3.GetCourse());
        Console.WriteLine("Средний балл: " + student3.GetAverage().ToString("F2"));
    }
}

