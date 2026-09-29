using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите K: ");
        int k = int.Parse(Console.ReadLine());

        Random random = new Random();

        string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        string vowels = "аеёиоуыэюя";

        char[] array = new char[k];

        for (int i = 0; i < k; i++)
        {
            array[i] = alphabet[random.Next(alphabet.Length)];
        }

        char[] consonants = new char[k];
        int count = 0;

        for (int i = 0; i < k; i++)
        {
            if (!vowels.Contains(array[i]))
            {
                consonants[count] = array[i];
                count++;
            }
        }

        Console.WriteLine("Первый массив:");
        for (int i = 0; i < k; i++)
        {
            Console.Write(array[i] + " ");
        }

        Console.WriteLine();
        Console.WriteLine("Массив согласных:");

        for (int i = 0; i < count; i++)
        {
            Console.Write(consonants[i] + " ");
        }
    }
}