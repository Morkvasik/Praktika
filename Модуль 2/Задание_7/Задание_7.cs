using System;

struct Student
{
    public string name;
    public string group;
    public int[] grades;

    public double Average()
    {
        double sum = 0;

        for (int i = 0; i < grades.Length; i++)
        {
            sum += grades[i];
        }

        return sum / grades.Length;
    }

    public bool goodgrades()
    {
        for (int i = 0; i < grades.Length; i++)
        {
            if (grades[i] != 4 && grades[i] != 5)
                return false;
        }

        return true;
    }
}

class Задание_7
{
    static void Main()
    {
        Student[] students = new Student[10];

        students[0] = new Student { name = "Иванов И.И.", group = "2ПОИС24", grades = new int[] { 5, 4, 5, 4, 5 } };
        students[1] = new Student { name = "Петров П.П.", group = "3ПОИС24", grades = new int[] { 2, 1, 1, 3, 4 } };
        students[2] = new Student { name = "Сидоров С.С.", group = "4ПОИС24", grades = new int[] { 5, 5, 4, 5, 5 } };
        students[3] = new Student { name = "Кузнецов А.А.", group = "2ПОИС24", grades = new int[] { 3, 3, 2, 4, 3 } };
        students[4] = new Student { name = "Смирнов Д.Д.", group = "3ПОИС24", grades = new int[] { 4, 5, 4, 5, 4 } };
        students[5] = new Student { name = "Попов Е.Е.", group = "4ПОИС24", grades = new int[] { 5, 5, 5, 4, 5 } };
        students[6] = new Student { name = "Васильев В.В.", group = "2ПОИС24", grades = new int[] { 3, 4, 3, 4, 4 } };
        students[7] = new Student { name = "Морозов М.М.", group = "3ПОИС24", grades = new int[] { 4, 4, 5, 4, 5 } };
        students[8] = new Student { name = "Новиков Н.Н.", group = "4ПОИС24", grades = new int[] { 5, 3, 1, 3, 5 } };
        students[9] = new Student { name = "Федоров Ф.Ф.", group = "2ПОИС24", grades = new int[] { 4, 5, 5, 5, 5 } };

        // Сортировка по среднему баллу
        for (int i = 0; i < students.Length - 1; i++)
        {
            for (int j = 0; j < students.Length - i - 1; j++)
            {
                if (students[j].Average() > students[j + 1].Average())
                {
                    Student temp = students[j];
                    students[j] = students[j + 1];
                    students[j + 1] = temp;
                }
            }
        }

        Console.WriteLine("Студенты по возрастанию среднего балла:");
        Console.WriteLine();

        for (int i = 0; i < students.Length; i++)
        {
            Console.WriteLine(
                students[i].name + "\t" +
                students[i].group + "\t" +
                students[i].Average());
        }

        Console.WriteLine();
        Console.WriteLine("Студенты только с оценками 4 и 5:");
        Console.WriteLine();

        for (int i = 0; i < students.Length; i++)
        {
            if (students[i].goodgrades())
            {
                Console.WriteLine(
                    students[i].name + "\t" +
                    students[i].group);
            }
        }
    }
}