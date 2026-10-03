using System;

class Car
{
    public string brand;
    public string model;
    public int year;
    public double price;

    public double Discount(double percent)
    {
        return price - price * percent / 100;
    }

    public double nalog(double percent)
    {
        return price + price * percent / 100;
    }
}

class Задание_6
{
    static void Main()
    {
        Car car = new Car();

        car.brand = "Toyota";
        car.model = "Camry";
        car.year = 2022;
        car.price = 60000;

        Console.WriteLine("Марка: " + car.brand);
        Console.WriteLine("Модель: " + car.model);
        Console.WriteLine("Год выпуска: " + car.year);
        Console.WriteLine("Цена: " + car.price);

        Console.WriteLine("Цена со скидкой 10%: " + car.Discount(10));
        Console.WriteLine("Цена с НДС 20%: " + car.nalog(20));
    }
}