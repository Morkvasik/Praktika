using System;

class Program
{
    static void Main()
    {
        Random random = new Random();

        double[] array = new double[10];
        int[] indexes = new int[10];

        for (int i = 0; i < 10; i++)
        {
            array[i] = random.Next(-1000, 1000) / 100.0;
            indexes[i] = i;
        }

        // Сортируем индексы по значениям массива
        for (int i = 0; i < 9; i++)
        {
            for (int j = i + 1; j < 10; j++)
            {
                if (array[indexes[i]] > array[indexes[j]])
                {
                    int temp = indexes[i];
                    indexes[i] = indexes[j];
                    indexes[j] = temp;
                }
            }
        }

        Console.WriteLine("Массив:");

        for (int i = 0; i < 10; i++)
        {
            Console.Write(array[i] + " ");
        }

        Console.WriteLine();
        Console.WriteLine("Индексы в порядке возрастания значений:");

        for (int i = 0; i < 10; i++)
        {
            Console.Write(indexes[i] + " ");
        }
    }
}