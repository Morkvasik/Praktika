using System;

// Создаем интерфейс с методами площади и периметра
interface IFigure
{
    double Area();
    double Perimeter();
}

// Класс круга
class Circle : IFigure
{
    public double Radius;

    public Circle(double radius)
    {
        Radius = radius;
    }

    public double Area()
    {
        return Math.PI * Radius * Radius;
    }

    public double Perimeter()
    {
        return 2 * Math.PI * Radius;
    }
}

// Класс прямоугольника
class Rectangle : IFigure
{
    public double A;
    public double B;

    public Rectangle(double a, double b)
    {
        A = a;
        B = b;
    }

    public double Area()
    {
        return A * B;
    }

    public double Perimeter()
    {
        return 2 * (A + B);
    }
}

// Класс треугольника
class Triangle : IFigure
{
    public double A;
    public double B;
    public double C;

    public Triangle(double a, double b, double c)
    {
        A = a;
        B = b;
        C = c;
    }

    public double Area()
    {
        // Считаем площадь по формуле Герона
        double p = Perimeter() / 2;
        return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
    }

    public double Perimeter()
    {
        return A + B + C;
    }
}

class Задание_1
{
    static void Main()
    {
        // Создаем три фигуры
        IFigure circle = new Circle(5);
        IFigure rectangle = new Rectangle(4, 6);
        IFigure triangle = new Triangle(3, 4, 5);

        // Выводим площадь и периметр каждой фигуры
        Console.WriteLine("Круг:");
        Console.WriteLine("Площадь: " + circle.Area().ToString("F2"));
        Console.WriteLine("Периметр: " + circle.Perimeter().ToString("F2"));

        Console.WriteLine("\nПрямоугольник:");
        Console.WriteLine("Площадь: " + rectangle.Area().ToString("F2"));
        Console.WriteLine("Периметр: " + rectangle.Perimeter().ToString("F2"));

        Console.WriteLine("\nТреугольник:");
        Console.WriteLine("Площадь: " + triangle.Area().ToString("F2"));
        Console.WriteLine("Периметр: " + triangle.Perimeter().ToString("F2"));
    }
}
