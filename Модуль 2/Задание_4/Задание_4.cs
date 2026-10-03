using System;

interface IDrawable
{
    void Draw();
}

class Circle : IDrawable
{
    public void Draw()
    {
        Console.WriteLine("Рисуется круг");
    }
}

class Rectangle : IDrawable
{
    public void Draw()
    {
        Console.WriteLine("Рисуется прямоугольник");
    }
}

class Triangle : IDrawable
{
    public void Draw()
    {
        Console.WriteLine("Рисуется треугольник");
    }
}

class Program
{
    static void Main()
    {
        IDrawable[] objects =
        {
            new Circle(),
            new Rectangle(),
            new Triangle()
        };

        foreach (IDrawable obj in objects)
        {
            obj.Draw();
        }
    }
}