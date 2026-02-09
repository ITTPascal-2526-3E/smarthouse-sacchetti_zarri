using BlaisePascal.SmartHouse.Domain.Climate;
using BlaisePascal.SmartHouse.Domain.Lamps;
using BlaisePascal.SmartHouse.Domain.Program;
using BlaisePascal.SmartHouse.Domain.Security;
using BlaisePascal.SmartHouse.Domain.Shutters;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

public class Program{
    static void Main(string[] args)
    {
        Console.WriteLine("--MENU SMARTHOUSE--");
        Console.WriteLine("Benvenuto, cosa desideri fare?");
        Console.WriteLine("A - menù illuminazione");
        Console.WriteLine("B - menù climatizzazione");
        Console.WriteLine("C - menù scuroni");
        Console.WriteLine("D - menù sicurezza intrusi");
        Console.WriteLine("E - menù sicurezza antincendio");
        Console.WriteLine();
        Console.WriteLine();
        var menuOpzioni = new Dictionary<ConsoleKey, string>
        {
            { ConsoleKey.A, "illuminazione" },
            { ConsoleKey.B, "climatizzazione" },
            { ConsoleKey.C, "scuroni" },
            { ConsoleKey.D, "sicurezza intrusi" },
            { ConsoleKey.E, "sicurezza antincendio" }
        };
        ConsoleKey input = Console.ReadKey(true).Key;

        if (menuOpzioni.TryGetValue(input, out string scelta))
        {
            Console.WriteLine($"Hai scelto il menù {scelta}");
            if(scelta == "illuminazione")
            {
                var menuOpzioni2 = new Dictionary<ConsoleKey, string>
                     {
                        { ConsoleKey.A, "Lampada"},
                        { ConsoleKey.B, "Ecolampada"},
                        { ConsoleKey.C, "Gruppo di lampade"},
                        { ConsoleKey.D, "Matrice di LEDs"}
                     };
                Console.WriteLine("--MENU ILLUMINAZIONE--");
                Console.WriteLine("Cosa desideri fare?");
                Console.WriteLine("A - menù Lampada");
                Console.WriteLine("B - menù Ecolampada");
                Console.WriteLine("C - menù Gruppo di lampade");
                Console.WriteLine("D - menù Matrice di LEDs");
                Console.WriteLine();
                Console.WriteLine();
                ConsoleKey input2 = Console.ReadKey(true).Key;

                if (menuOpzioni2.TryGetValue(input, out string scelta2))
                {
                    Console.WriteLine($"Hai scelto il menù {scelta}");
                    if(scelta2 == "Lampada") {
                    
                    }
                    else if (scelta2 == "Ecolampada") {
                    
                    }
                    else if (scelta2 == "Gruppo di lampade") {
                    
                    }
                    else if (scelta2 == "Matrice di LEDs") { 
                    
                    }
                }
            else if (scelta == "climatizzazione")
            {
                Console.WriteLine("--MENU CLIMATIZZAZIONE--");
                Console.WriteLine("Cosa desideri fare?");
                Console.WriteLine("A - menù Termoregolatore");
                Console.WriteLine();
                Console.WriteLine();

            }
            else if (scelta == "scuroni")
            {
                Console.WriteLine("--MENU SCURONI--");
                Console.WriteLine("cosa desideri fare?");
                Console.WriteLine("A - menù Scuroni automatici");
                Console.WriteLine();
                Console.WriteLine();


            }
            else if (scelta == "sicurezza intrusi")
            {
                Console.WriteLine("--MENU SICUREZZA INTRUSI--");
                Console.WriteLine("Cosa desideri fare?");
                Console.WriteLine("A - menù Porta blindata");
                Console.WriteLine();
                Console.WriteLine();


            }
            else if (scelta == "sicurezza antincendio")
            { 
                Console.WriteLine("--MENU SICUREZZA ANTINCENDIO--");
                Console.WriteLine("Cosa desideri fare?");
                Console.WriteLine("A - menù Rilevatore di fumo");
                Console.WriteLine();
                Console.WriteLine();


            }

        }
        else
        {
            Console.WriteLine("Tasto non valido");
        }



        /*
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
        Console.WriteLine(lamp.deviceId);
        Console.WriteLine(lamp.lastModifiedAtUtc);
        Console.WriteLine(lamp2.deviceId);

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

        LampsRow lampsRow = new LampsRow();
        lampsRow.addLamp(23.4,"ciao",100.0);
        lampsRow.turnOn();
        Console.WriteLine("LampsRow first lamp is on: " + lampsRow.lamps[0].is_on);

        Shutters shutters = new Shutters();
        ShuttersController autoShutters = new ShuttersController(shutters);
        autoShutters.Start();
        Console.WriteLine("la persiana è aperta = ");
        Console.WriteLine(shutters.is_open);
        Console.WriteLine("la persiana è chiusa = ");
        Console.WriteLine(shutters.is_closed);
        //var webcam = new Webcam();
        //webcam.Start();
        AirConditioner air = new AirConditioner(10.0, 3);
        air.turnOn();
        Console.WriteLine(air.lastModifiedAtUtc);
        Console.WriteLine(air.cratedTime);
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


        SecureDoor door = new SecureDoor("mypassword", "zarrimako@gmail.com");
        SendBackupCode sender = new SendBackupCode(door);
        door.Lock();

        door.Unlock(Console.ReadLine());
        Console.WriteLine("Door is locked: " + door.is_locked);
        //door.resetPassword();

        door.Unlock(Console.ReadLine());
        Console.WriteLine("Door is locked: " + door.is_locked);
        */
    }
}
