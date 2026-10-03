using System;

class Author
{
    public string name;
    public int birthYear;

    public Author(string n, int year)
    {
        name = n;
        birthYear = year;
    }
}

class Book
{
    public string title;
    public int year;
    public Author author;

    public Book(string t, int y, Author a)
    {
        title = t;
        year = y;
        author = a;
    }
}

class Program
{
    static void Main()
    {
        Author author1 = new Author("Александр Пушкин", 1799);
        Author author2 = new Author("Лев Толстой", 1828);

        Book book1 = new Book("Евгений Онегин", 1833, author1);
        Book book2 = new Book("Война и мир", 1869, author2);

        Console.WriteLine("Книги:");

        Console.WriteLine(book1.title + ", " + book1.year);
        Console.WriteLine("Автор: " + book1.author.name);
        Console.WriteLine("Год рождения: " + book1.author.birthYear);

        Console.WriteLine();

        Console.WriteLine(book2.title + ", " + book2.year);
        Console.WriteLine("Автор: " + book2.author.name);
        Console.WriteLine("Год рождения: " + book2.author.birthYear);
    }
}