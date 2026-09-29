using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите ваш возраст: ");
        int age = int.Parse(Console.ReadLine());

        if (age > 18)
        {
            Console.WriteLine("Вы можете получить водительские права.");
        }
        else
        {
            Console.WriteLine("Вы не можете получить водительские права.");
        }
    }
}