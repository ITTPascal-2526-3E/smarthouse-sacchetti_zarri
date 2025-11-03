

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
            lamp.ChangeColor(LampColor.White);
            Console.WriteLine("LampColor: " + lamp.Color);
            lamp.adjustBrightness(23);
            Console.WriteLine("Lamp brightness percentage: " + lamp.brightness_Perc);
            EcoLamp ecolamp = new EcoLamp(50.0, "Osram", 200.0);
            ecolamp.turnOn();
            Console.WriteLine(ecolamp.startTime);
            Console.WriteLine("EcoLamp is on: " + ecolamp.is_on);
            Console.WriteLine("EcoLamp power: " + ecolamp.power + " Watt");
            Console.WriteLine("EcoLamp brand: " + ecolamp.brand);
            Console.WriteLine("EcoLamp brightness percentage: " + ecolamp.brightness_Perc);
            Console.WriteLine("EcoLamp max brightness: " + ecolamp.max_brightness + " Lumen");
            ecolamp.adjustBrightness(99);
            Console.WriteLine("EcoLamp brightness percentage: " + ecolamp.brightness_Perc);
            ecolamp.ecoMode();
            Console.WriteLine("EcoLamp brightness percentage: " + ecolamp.brightness_Perc);
            ecolamp.turnOff();

    }
    }

