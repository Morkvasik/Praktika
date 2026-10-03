using System;

abstract class Shape
{
    public abstract double Area();
}

class Circle : Shape
{
    public double radius;

    public Circle(double r)
    {
        radius = r;
    }

    public override double Area()
    {
        return Math.PI * radius * radius;
    }
}

class Rectangle : Shape
{
    public double width;
    public double height;

    public Rectangle(double w, double h)
    {
        width = w;
        height = h;
    }

    public override double Area()
    {
        return width * height;
    }
}

class Triangle : Shape
{
    public double width;
    public double height;

    public Triangle(double w, double h)
    {
        width = w;
        height = h;
    }

    public override double Area()
    {
        return width * height / 2;
    }
}

class Задание_8
{
    static void Main()
    {
        Circle circle = new Circle(5);
        Rectangle rectangle = new Rectangle(4, 6);
        Triangle triangle = new Triangle(4, 5);

        Console.WriteLine("Площадь круга: " + circle.Area());
        Console.WriteLine("Площадь прямоугольника: " + rectangle.Area());
        Console.WriteLine("Площадь треугольника: " + triangle.Area());
    }
}