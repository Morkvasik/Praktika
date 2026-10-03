using System;

class Person
{
    private string name;
    private int age;
    private string address;

    public void SetName(string n)
    {
        name = n;
    }

    public string GetName()
    {
        return name;
    }

    public void SetAge(int a)
    {
        age = a;
    }

    public int GetAge()
    {
        return age;
    }

    public void SetAddress(string adr)
    {
        address = adr;
    }

    public string GetAddress()
    {
        return address;
    }
}

class Program
{
    static void Main()
    {
        Person p1 = new Person();
        p1.SetName("Иван");
        p1.SetAge(20);
        p1.SetAddress("Минск");

        Person p2 = new Person();
        p2.SetName("Анна");
        p2.SetAge(19);
        p2.SetAddress("Орша");

        Console.WriteLine(p1.GetName() + ", " + p1.GetAge() + ", " + p1.GetAddress());
        Console.WriteLine(p2.GetName() + ", " + p2.GetAge() + ", " + p2.GetAddress());
    }
}