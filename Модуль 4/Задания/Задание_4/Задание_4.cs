using System;

// Создаем интерфейс книги
interface IBook
{
    bool IsAvailable();
    void GiveBook();
}

// Класс книги
class Book : IBook
{
    public string Name;
    public bool Available;

    public Book(string name)
    {
        Name = name;
        Available = true;
    }

    // Проверяем, доступна ли книга
    public bool IsAvailable()
    {
        return Available;
    }

    // Выдаем книгу читателю
    public void GiveBook()
    {
        if (Available)
        {
            Console.WriteLine("Книга выдана: " + Name);
            Available = false;
        }
        else
        {
            Console.WriteLine("Книга уже выдана: " + Name);
        }
    }
}

class Program
{
    static void Main()
    {
        // Создаем две книги
        IBook book1 = new Book("Война и мир");
        IBook book2 = new Book("Преступление и наказание");

        // Проверяем доступность и выдаем первую книгу
        Console.WriteLine("Доступна ли первая книга: " + (book1.IsAvailable() ? "Да" : "Нет"));
        book1.GiveBook();

        // Проверяем доступность после выдачи
        Console.WriteLine("Доступна ли первая книга: " + (book1.IsAvailable() ? "Да" : "Нет"));

        Console.WriteLine();

        // Выдаем вторую книгу
        Console.WriteLine("Доступна ли вторая книга: " + (book2.IsAvailable() ? "Да" : "Нет"));
        book2.GiveBook();

        Console.WriteLine("Доступна ли вторая книга: " + (book2.IsAvailable() ? "Да" : "Нет"));
    }
}

