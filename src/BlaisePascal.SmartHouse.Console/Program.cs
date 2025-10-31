

using BlaisePascal.SmartHouse.Domain;

public class Program
    {
        static void Main(string[] args)
        {
            Lamp lamp = new Lamp(60.0, "Philips", 800.0);
            lamp.turnOn();
            lamp.turnOff();
            Console.WriteLine("Lamp is on: " + lamp.is_on);
            Console.WriteLine("Lamp power: " + lamp.power + " Watt");
            Console.WriteLine("Lamp brand: " + lamp.brand);
            Console.WriteLine("Lamp brightness percentage: " + lamp.brightness_Perc);
            Console.WriteLine("Lamp max brightness: " + lamp.max_brightness + " Lumen");
        }
    }

