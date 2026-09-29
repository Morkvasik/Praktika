using System;

class Program
{
    static void Main()
    {
        Random random = new Random();

        Console.Write("Введите число: ");
        int number = int.Parse(Console.ReadLine());

        int sum = 0;
        int count = 0;

        int[] array = new int[number];

        while (sum < number)
        {
            array[count] = random.Next(1, 10);
            sum += array[count];
            count++;
        }

        Console.WriteLine("Массив:");

        for (int i = 0; i < count; i++)
        {
            Console.Write(array[i] + " ");
        }

        Console.WriteLine();
        Console.WriteLine("Количество элементов: " + count);
        Console.WriteLine("Сумма: " + sum);
    }
}