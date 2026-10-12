using System;

// Создаем интерфейс для рисования
interface IDrawing
{
    void DrawLine(int x1, int y1, int x2, int y2);
    void DrawCircle(int x, int y, int radius);
    void DrawRectangle(int x, int y, int width, int height);
}

// Класс для работы с холстом
class Canvas : IDrawing
{
    // Рисуем линию
    public void DrawLine(int x1, int y1, int x2, int y2)
    {
        Console.WriteLine("Нарисована линия");
        Console.WriteLine("От точки " + x1 + ", " + y1 +
                          " до точки " + x2 + ", " + y2);
    }

    // Рисуем круг
    public void DrawCircle(int x, int y, int radius)
    {
        Console.WriteLine("Нарисован круг");
        Console.WriteLine("Центр: " + x + ", " + y);
        Console.WriteLine("Радиус: " + radius);
    }

    // Рисуем прямоугольник
    public void DrawRectangle(int x, int y, int width, int height)
    {
        Console.WriteLine("Нарисован прямоугольник");
        Console.WriteLine("Ширина: " + width);
        Console.WriteLine("Высота: " + height);
    }
}

class Program
{
    static void Main()
    {
        // Создаем холст
        IDrawing canvas = new Canvas();

        // Вызываем методы рисования
        canvas.DrawLine(0, 0, 10, 10);
        canvas.DrawCircle(5, 5, 3);
        canvas.DrawRectangle(2, 2, 8, 4);
    }
}
