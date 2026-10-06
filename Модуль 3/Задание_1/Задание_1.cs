using System;

class Figure
{
    public virtual double GetArea()
    {
        return 0;
    }
}

class Circle : Figure
{
    private double radius;

    public Circle(double radius)
    {
        this.radius = radius;
    }

    public override double GetArea()
    {
        return Math.PI * radius * radius;
    }
}

class Rectangle : Figure
{
    private double width;
    private double height;

    public Rectangle(double width, double height)
    {
        this.width = width;
        this.height = height;
    }

    public override double GetArea()
    {
        return width * height;
    }
}

class Triangle : Figure
{
    private double basis;
    private double height;

    public Triangle(double basis, double height)
    {
        this.basis = basis;
        this.height = height;
    }

    public override double GetArea()
    {
        return basis * height / 2;
    }
}

class Задание_1
{
    delegate double AreaDelegate();

    static void Main()
    {
        Circle circle = new Circle(5);
        Rectangle rectangle = new Rectangle(4, 6);
        Triangle triangle = new Triangle(8, 5);

        AreaDelegate area;

        area = circle.GetArea;
        Console.WriteLine("Площадь круга: " + area().ToString("F2"));

        area = rectangle.GetArea;
        Console.WriteLine("Площадь прямоугольника: " + area().ToString("F2"));

        area = triangle.GetArea;
        Console.WriteLine("Площадь треугольника: " + area().ToString("F2"));
    }
}