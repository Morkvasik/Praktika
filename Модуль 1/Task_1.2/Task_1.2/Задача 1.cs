using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите размер массива N: ");
        int n = int.Parse(Console.ReadLine());

        double[] array = new double[n];

        Console.WriteLine("Введите элементы массива:");

        for (int i = 0; i < n; i++)
        {
            array[i] = double.Parse(Console.ReadLine());
        }

        double max = Math.Abs(array[0]);

        for (int i = 1; i < n; i++)
        {
            if (Math.Abs(array[i]) > max)
            {
                max = Math.Abs(array[i]);
            }
        }

        for (int i = 0; i < n; i++)
        {
            array[i] = array[i] / max;
        }

        Console.WriteLine("Измененный массив:");

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine(array[i] + " ");
        }
    }
}