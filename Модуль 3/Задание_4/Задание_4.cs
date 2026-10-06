using System;
using System.Collections.Generic;

class Program
{
    delegate bool FilterDelegate(string text);

    static bool ContainsWord(string text)
    {
        return text.ToLower().Contains("c#");
    }

    static bool StartsWithA(string text)
    {
        return text.StartsWith("А");
    }

    static void ShowFiltered(List<string> data, FilterDelegate filter)
    {
        foreach (string item in data)
        {
            if (filter(item))
            {
                Console.WriteLine(item);
            }
        }
    }

    static void Main()
    {
        List<string> data = new List<string>
        {
            "Изучение C#",
            "Программирование",
            "Алгоритмы",
            "Основы C#",
            "Работа с массивами"
        };

        Console.WriteLine("Выберите фильтр:");
        Console.WriteLine("1 - Содержит C#");
        Console.WriteLine("2 - Начинается с буквы A");

        int choice = int.Parse(Console.ReadLine());

        FilterDelegate filter;

        if (choice == 1)
        {
            filter = ContainsWord;
        }
        else if(choice == 2)
        {
            filter = StartsWithA;
        }
        else
        {
            Console.WriteLine("неверный выбор");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Результат:");

        ShowFiltered(data, filter);
    }
}