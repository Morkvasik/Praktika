using System;

// Создаем интерфейс товара
interface IProduct
{
    double GetCost();
    int GetCount();
}

// Класс товара
class Product : IProduct
{
    public string Name;
    public double Price;
    public int Count;

    public Product(string name, double price, int count)
    {
        Name = name;
        Price = price;
        Count = count;
    }

    // Считаем стоимость всех товаров
    public double GetCost()
    {
        return Price * Count;
    }

    // Получаем остаток товара
    public int GetCount()
    {
        return Count;
    }
}

class Задание_2
{
    static void Main()
    {
        Product milk = new Product("Молоко", 2.79, 10);
        Product bread = new Product("Хлеб", 2.00, 20);
        Product apples = new Product("Яблоки", 3.75, 15);

        // Выводим информацию о товарах
        Console.WriteLine("Товар: " + milk.Name);
        Console.WriteLine("Стоимость: " + milk.GetCost().ToString("F2"));
        Console.WriteLine("Остаток: " + milk.GetCount());

        Console.WriteLine("\nТовар: " + bread.Name);
        Console.WriteLine("Стоимость: " + bread.GetCost().ToString("F2"));
        Console.WriteLine("Остаток: " + bread.GetCount());

        Console.WriteLine("\nТовар: " + apples.Name);
        Console.WriteLine("Стоимость: " + apples.GetCost().ToString("F2"));
        Console.WriteLine("Остаток: " + apples.GetCount());
    }
}