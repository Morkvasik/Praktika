using System;

class Program
{
    static void Main()
    {
        Random random = new Random();

        Console.Write("Введите размер матрицы: ");
        int n = int.Parse(Console.ReadLine());

        int[,] matrix = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = random.Next(-50, 51);
            }
        }

        Console.WriteLine("Исходная матрица:");

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }

            Console.WriteLine();
        }

        // Сортировка строк по сумме элементов
        for (int i = 0; i < n - 1; i++)
        {
            for (int k = i + 1; k < n; k++)
            {
                int sum1 = 0;
                int sum2 = 0;

                for (int j = 0; j < n; j++)
                {
                    sum1 += matrix[i, j];
                    sum2 += matrix[k, j];
                }

                if (sum1 > sum2)
                {
                    for (int j = 0; j < n; j++)
                    {
                        int temp = matrix[i, j];
                        matrix[i, j] = matrix[k, j];
                        matrix[k, j] = temp;
                    }
                }
            }
        }

        Console.WriteLine("Матрица после сортировки:");

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }

            Console.WriteLine();
        }
    }
}