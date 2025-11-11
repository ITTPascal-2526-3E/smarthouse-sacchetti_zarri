

using BlaisePascal.SmartHouse.Domain;

public class Program{
    static void Main(string[] args)
    {
        Lamp lamp = new Lamp(60.0, "Philips", 800.0);
        Lamp lamp2 = new Lamp(60.0, "Philips", 800.0);
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
        Console.WriteLine(lamp.lamp_Id);
        Console.WriteLine(lamp2.lamp_Id);

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

        Shutters shutters = new Shutters();
        ShuttersController autoShutters = new ShuttersController(shutters);
        autoShutters.Start();
        Console.WriteLine("la persiana è aperta = ");
        Console.WriteLine(shutters.is_open);
        Console.WriteLine("la persiana è chiusa = ");
        Console.WriteLine(shutters.is_closed);
        //var webcam = new Webcam();
        // webcam.start();
        AirConditioner air = new AirConditioner(10.0, 3);
        air.turnOn();
        Console.WriteLine(air.air_enabled);
        Console.WriteLine(air.air_intensity);
        air.switchIntensity(2);
        Console.WriteLine(air.air_intensity);
        air.turnOff();
        air.turnOn();
        Console.WriteLine(air.air_intensity);
        Console.WriteLine(air.lowest_temperature);
        air.switchTemperature(15.0);
        Console.WriteLine(air.target_temperature);


        Thermostat thermostat = new Thermostat(18.0);
        thermostat.radiators[0]=new Radiator(0);
        thermostat.radiators[1] = new Radiator(0);
        thermostat.SwitchTargetTemperature(22.0);
        Console.WriteLine("Thermostat target temperature: " + thermostat.current_temperature);
    }
}

