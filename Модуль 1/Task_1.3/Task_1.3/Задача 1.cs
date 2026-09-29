using System;

class Program
{
    static int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = a % b;
            a = b;
            b = temp;
        }

        return a;
    }

    static void Main()
    {
        Console.Write("Введите числитель: ");
        int numerator = int.Parse(Console.ReadLine());

        Console.Write("Введите знаменатель: ");
        int denominator = int.Parse(Console.ReadLine());

        int gcd = GCD(numerator, denominator);

        numerator /= gcd;
        denominator /= gcd;

        Console.WriteLine("Сокращенная дробь: " + numerator + "/" + denominator);
    }
}