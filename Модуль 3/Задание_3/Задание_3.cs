using System;

class TaskItem
{
    public string Name { get; set; }
    public Action Action { get; set; }

    public TaskItem(string name, Action action)
    {
        Name = name;
        Action = action;
    }

    public void Execute()
    {
        Console.WriteLine("Выполняется задача: " + Name);
        Action();
    }
}

class Program
{
    static void SendNotification()
    {
        Console.WriteLine("Уведомление отправлено.");
    }

    static void WriteToLog()
    {
        Console.WriteLine("Задача записана в журнал.");
    }

    static void Main()
    {
        Console.WriteLine("Введите название задачи:");
        string name = Console.ReadLine();

        Console.WriteLine("Выберите действие:");
        Console.WriteLine("1 - Отправить уведомление");
        Console.WriteLine("2 - Записать в журнал");

        int choice = int.Parse(Console.ReadLine());

        Action selectedAction;

        if (choice == 1)
        {
            selectedAction = SendNotification;
        }
        else
        {
            selectedAction = WriteToLog;
        }

        TaskItem task = new TaskItem(name, selectedAction);

        Console.WriteLine();
        task.Execute();
    }
}