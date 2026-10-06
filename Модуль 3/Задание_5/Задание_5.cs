using System;

class Program
{
    delegate void SortDelegate(int[] array);

    static void BubbleSort(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = 0; j < array.Length - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }

    static void QuickSort(int[] array, int left, int right)
    {
        int i = left;
        int j = right;
        int pivot = array[(left + right) / 2];

        while (i <= j)
        {
            while (array[i] < pivot)
                i++;

            while (array[j] > pivot)
                j--;

            if (i <= j)
            {
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;

                i++;
                j--;
            }
        }

        if (left < j)
            QuickSort(array, left, j);

        if (i < right)
            QuickSort(array, i, right);
    }

    static void QuickSortStart(int[] array)
    {
        QuickSort(array, 0, array.Length - 1);
    }

    static void Main()
    {
        int[] numbers = { 8, 3, 7, 1, 5, 2, 9, 4 };

        Console.WriteLine("Исходный массив:");
        Console.WriteLine(string.Join(" ", numbers));

        Console.WriteLine();
        Console.WriteLine("Выберите сортировку:");
        Console.WriteLine("1 - Пузырьковая сортировка");
        Console.WriteLine("2 - Быстрая сортировка");

        int choice = int.Parse(Console.ReadLine());

        SortDelegate sort;

        if (choice == 1)
        {
            sort = BubbleSort;
        }
        else
        {
            sort = QuickSortStart;
        }

        sort(numbers);

        Console.WriteLine();
        Console.WriteLine("Отсортированный массив:");
        Console.WriteLine(string.Join(" ", numbers));
    }
}