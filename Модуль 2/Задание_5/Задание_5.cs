using System;

class TemperatureSensor
{
    public event Action<int> TemperatureChanged;

    public void SetTemperature(int temperature)
    {
        Console.WriteLine("Температура: " + temperature);
        TemperatureChanged?.Invoke(temperature);
    }
}

class Thermostat
{
    public void OnTemperatureChanged(int temperature)
    {
        if (temperature < 20)
        {
            Console.WriteLine("Отопление вкл");
        }
        else
        {
            Console.WriteLine("Отопление выкл");
        }
    }
}

class Program
{
    static void Main()
    {
        TemperatureSensor sensor = new TemperatureSensor();
        Thermostat thermostat = new Thermostat();

        sensor.TemperatureChanged += thermostat.OnTemperatureChanged;

        sensor.SetTemperature(15);
        sensor.SetTemperature(21);
    }
}