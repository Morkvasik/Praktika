using System;

class Shape
{
    public virtual double Area()
    {
        return 0;
    }

    public virtual double Perimeter()
    {
        return 0;
    }
}

class Circle : Shape
{
    public double radius = 5;

    public override double Area()
    {
        return Math.PI * radius * radius;
    }

    public override double Perimeter()
    {
        return 2 * Math.PI * radius;
    }
}

class Rectangle : Shape
{
    public double width = 4;
    public double height = 6;

    public override double Area()
    {
        return width * height;
    }

    public override double Perimeter()
    {
        return 2 * (width + height);
    }
}

class Program
{
    static void Main()
    {
        Shape shape = new Shape();
        Circle circle = new Circle();
        Rectangle rectangle = new Rectangle();

        Console.WriteLine("Фигура:");
        Console.WriteLine("Площадь: " + shape.Area());
        Console.WriteLine("Периметр: " + shape.Perimeter());

        Console.WriteLine("\nКруг:");
        Console.WriteLine("Площадь: " + circle.Area());
        Console.WriteLine("Периметр: " + circle.Perimeter());

        Console.WriteLine("\nПрямоугольник:");
        Console.WriteLine("Площадь: " + rectangle.Area());
        Console.WriteLine("Периметр: " + rectangle.Perimeter());
    }
}