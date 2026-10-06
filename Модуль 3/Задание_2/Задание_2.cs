using System;

class Notification
{
    public event Action MessageSent;
    public event Action CallReceived;
    public event Action EmailReceived;

    public void SendMessage()
    {
        Console.WriteLine("Отправлено сообщение");
        MessageSent?.Invoke();
    }

    public void MakeCall()
    {
        Console.WriteLine("Поступил звонок");
        CallReceived?.Invoke();
    }

    public void SendEmail()
    {
        Console.WriteLine("Получено электронное письмо");
        EmailReceived?.Invoke();
    }
}

class Задание_2
{
    static void MessageHandler()
    {
        Console.WriteLine("Обработчик: сообщение обработано!");
    }

    static void CallHandler()
    {
        Console.WriteLine("Обработчик: звонок обработан!");
    }

    static void EmailHandler()
    {
        Console.WriteLine("Обработчик: письмо обработано!");
    }

    static void Main()
    {
        Notification notification = new Notification();

        notification.MessageSent += MessageHandler;
        notification.CallReceived += CallHandler;
        notification.EmailReceived += EmailHandler;

        notification.SendMessage();
        notification.MakeCall();
        notification.SendEmail();
    }
}