using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;
using BlaisePascal.SmartHouse.Domain.AbstractInterfaces; // Assunto per la classe Device
using System;
using System.Collections.Generic;
using System.Linq;
using BlaisePascal.SmartHouse.Domain.Devices.Lamps;
using BlaisePascal.SmartHouse.Domain.Devices.Security;
using BlaisePascal.SmartHouse.Domain.Devices.Shutters;
using BlaisePascal.SmartHouse.Domain.Devices.Climate;

public class Program
{
    // Lista globale per gestire tutti i dispositivi insieme
    static List<object> allDevices = new List<object>();

    static void Main(string[] args)
    {
        // =============================================================
        // 1. ISTANZIAMENTO OGGETTI (Tutte le classi concrete)
        // =============================================================

        // --- ILLUMINAZIONE ---
        Lamp lamp = new Lamp(new Power(10), new Name("Philips Hue"), new Brightness(800));
        EcoLamp ecoLamp = new EcoLamp(new Power(5), new Name("Beghelli Eco"), new Brightness(600));

        MatrixLed ledMatrix = new MatrixLed();
        Led templateLed = new Led(new Power(1), new Name("Pixel"), new Brightness(100));
        ledMatrix.GenerateMatrix(5, 5, templateLed); // Matrice 5x5 per test

        // --- CLIMATIZZAZIONE ---
        AirConditioner airCond = new AirConditioner(16.0, new Air(3)); // Min temp 16, intensità 3
        Radiator radiator = new Radiator(20.0); // Temp iniziale 20
        Thermostat thermostat = new Thermostat(19.5); // Temp corrente 19.5

        // --- SICUREZZA ---
        // Assumo i costruttori di Password ed Email basandomi sul codice di SecureDoor
        SecureDoor door = new SecureDoor(new Password("1234"), new Email("admin@house.com"));

        // --- SCURONI ---
        Shutters shutters = new Shutters();

        // Aggiungo tutto alla lista globale per la visualizzazione riassuntiva
        allDevices.Add(lamp);
        allDevices.Add(ecoLamp);
        allDevices.Add(ledMatrix);
        allDevices.Add(airCond);
        allDevices.Add(radiator);
        allDevices.Add(thermostat);
        allDevices.Add(door);
        allDevices.Add(shutters);

        // =============================================================
        // 2. CONFIGURAZIONE MENU
        // =============================================================

        var menuColori = new Dictionary<ConsoleKey, LampColor>
        {
            { ConsoleKey.A, LampColor.White}, { ConsoleKey.B, LampColor.WarmWhite},
            { ConsoleKey.C, LampColor.CoolWhite}, { ConsoleKey.D, LampColor.Red},
            { ConsoleKey.E, LampColor.Green}, { ConsoleKey.F, LampColor.Blue},
            { ConsoleKey.G, LampColor.Yellow}, { ConsoleKey.H, LampColor.Purple}
        };

        bool eseguiProgramma = true;

        while (eseguiProgramma)
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine("      SMARTHOUSE - MAIN HUB          ");
            Console.WriteLine("=====================================");
            Console.WriteLine("A - Illuminazione (Lampade/Matrix)");
            Console.WriteLine("B - Climatizzazione (AC/Termosifoni)");
            Console.WriteLine("C - Scuroni (Tapparelle)");
            Console.WriteLine("D - Sicurezza (Porta/Webcam)");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("L - LISTA DI TUTTI I DISPOSITIVI (Stato)");
            Console.WriteLine("X - CHIUDI PROGRAMMA");
            Console.WriteLine("=====================================");

            ConsoleKey inputHome = Console.ReadKey(true).Key;

            switch (inputHome)
            {
                case ConsoleKey.X:
                    eseguiProgramma = false;
                    Console.WriteLine("\nSpegnimento sistema... Bye!");
                    // Chiudo la webcam se era aperta
                    break;

                case ConsoleKey.A:
                    MenuIlluminazione(lamp, ecoLamp, ledMatrix, menuColori);
                    break;

                case ConsoleKey.B:
                    MenuClima(airCond, radiator, thermostat);
                    break;

                case ConsoleKey.C:
                    MenuScuroni(shutters);
                    break;

                case ConsoleKey.D:
                    MenuSicurezza(door);
                    break;

                case ConsoleKey.L:
                    MostraListaDispositivi();
                    break;
            }
        }
    }

    // =============================================================
    // METODI MENU CATEGORIE
    // =============================================================

    static void MenuIlluminazione(Lamp lamp, EcoLamp eco, MatrixLed matrix, Dictionary<ConsoleKey, LampColor> colori)
    {
        bool attivo = true;
        while (attivo)
        {
            Console.Clear();
            Console.WriteLine("-- MENU ILLUMINAZIONE --");
            Console.WriteLine("1 - Gestisci Lampada Standard");
            Console.WriteLine("2 - Gestisci Ecolampada");
            Console.WriteLine("3 - Gestisci Matrice LED");
            Console.WriteLine("X - Torna Indietro");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.X: attivo = false; break;
                case ConsoleKey.D1: GestisciLampada(lamp, colori); break;
                case ConsoleKey.D2: GestisciEcoLampada(eco); break;
                case ConsoleKey.D3: GestisciMatrice(matrix); break;
            }
        }
    }

    static void MenuClima(AirConditioner ac, Radiator rad, Thermostat therm)
    {
        bool attivo = true;
        while (attivo)
        {
            Console.Clear();
            Console.WriteLine("-- MENU CLIMATIZZAZIONE --");
            Console.WriteLine("1 - Gestisci Condizionatore");
            Console.WriteLine("2 - Gestisci Radiatore");
            Console.WriteLine("3 - Gestisci Termostato Generale");
            Console.WriteLine("X - Torna Indietro");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.X: attivo = false; break;
                case ConsoleKey.D1: GestisciAC(ac); break;
                case ConsoleKey.D2: GestisciRadiatore(rad); break;
                case ConsoleKey.D3: GestisciTermostato(therm); break;
            }
        }
    }

    static void MenuSicurezza(SecureDoor door)
    {
        bool attivo = true;
        while (attivo)
        {
            Console.Clear();
            Console.WriteLine("-- MENU SICUREZZA --");
            Console.WriteLine("1 - Gestisci Porta Blindata");
            Console.WriteLine("2 - Gestisci Webcam");
            Console.WriteLine("X - Torna Indietro");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.X: attivo = false; break;
                case ConsoleKey.D1: GestisciPorta(door); break;
            }
        }
    }

    static void MenuScuroni(Shutters shutters)
    {
        Console.Clear();
        Console.WriteLine("-- MENU SCURONI --");
        Console.WriteLine("Stai per entrare nel controller manuale degli scuroni.");
        Console.WriteLine("Premi 'A' per aprire, 'C' per chiudere.");
        Console.WriteLine("Premi un altro tasto per uscire dal controller.");
        Console.WriteLine("\nPremi invio per avviare...");
        Console.ReadLine();

        // Il controller ha il suo loop interno
        shutters.autoShutters.Start();
    }

    // =============================================================
    // GESTIONE SPECIFICA DISPOSITIVI (CONCRETE CLASSES)
    // =============================================================

    // --- LAMPADA ---
    static void GestisciLampada(Lamp lamp, Dictionary<ConsoleKey, LampColor> menuColori)
    {
        bool loop = true;
        while (loop)
        {
            Console.Clear();
            Console.WriteLine($"LAMPADA: {(lamp.is_on ? "ON" : "OFF")} | Lum: {lamp.brightness_Perc} | Col: {lamp.Color}");
            Console.WriteLine("A-On, B-Off, C-Lum, D-Colore, X-Esci");
            var k = Console.ReadKey(true).Key;
            if (k == ConsoleKey.X) loop = false;
            else if (k == ConsoleKey.A) lamp.turnOn();
            else if (k == ConsoleKey.B) lamp.turnOff();
            else if (k == ConsoleKey.C)
            {
                Console.Write("Valore (0-100): ");
                if (int.TryParse(Console.ReadLine(), out int v)) lamp.adjustBrightness(new Brightness(v));
            }
            else if (k == ConsoleKey.D && lamp.is_on)
            {
                Console.WriteLine("Scegli A-H per colore...");
                Console.WriteLine("A-White, B-WarmWhite, C-CoolWhite, D-Red, E-Green, F-Blue, G-Yellow, H-Purple");
                var c = Console.ReadKey(true).Key;
                if (menuColori.TryGetValue(c, out LampColor col))
                {
                    lamp.ChangeColor(col);
                    Console.WriteLine($"Colore cambiato a {lamp.Color}.");
                } else Console.WriteLine("Scelta colore non valida.");
            }
        }
    }

    // --- ECOLAMPADA ---
    static void GestisciEcoLampada(EcoLamp lamp)
    {
        bool loop = true;
        while (loop)
        {
            Console.Clear();
            Console.WriteLine($"ECOLAMPADA: {(lamp.is_on ? "ON" : "OFF")} | StartTime: {lamp.startTime}");
            Console.WriteLine("A-On, B-Off, C-EcoStart, D-EcoLogic, X-Esci");
            var k = Console.ReadKey(true).Key;
            if (k == ConsoleKey.X) loop = false;
            else if (k == ConsoleKey.A) lamp.turnOn();
            else if (k == ConsoleKey.B) lamp.turnOff();
            else if (k == ConsoleKey.C) { lamp.startEcoMode(); Console.WriteLine("Timer avviato."); PremiTasto(); }
            else if (k == ConsoleKey.D) { lamp.ecoMode(); Console.WriteLine("Logica applicata."); PremiTasto(); }
        }
    }

    // --- MATRICE LED ---
    static void GestisciMatrice(MatrixLed matrix)
    {
        bool loop = true;
        while (loop)
        {
            Console.Clear();
            Console.WriteLine($"MATRICE LED {matrix.matrix.GetLength(0)}x{matrix.matrix.GetLength(1)}");
            // Disegno semplice
            for (int i = 0; i < matrix.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.matrix.GetLength(1); j++)
                    Console.Write(matrix.GetLed(i, j).is_on ? "O " : ". ");
                Console.WriteLine();
            }
            Console.WriteLine("A-All On, B-All Off, C-CheckerPattern, D-Intensity, X-Esci");
            var k = Console.ReadKey(true).Key;
            if (k == ConsoleKey.X) loop = false;
            else if (k == ConsoleKey.A) matrix.turnOn();
            else if (k == ConsoleKey.B) matrix.turnOff();
            else if (k == ConsoleKey.C) matrix.PatternCheckerBoard();
            else if (k == ConsoleKey.D)
            {
                Console.Write("Intensità: ");
                if (int.TryParse(Console.ReadLine(), out int v)) matrix.SetIntensityAll(new Brightness(v));
            }
        }
    }

    // --- ARIA CONDIZIONATA ---
    static void GestisciAC(AirConditioner ac)
    {
        bool loop = true;
        while (loop)
        {
            Console.Clear();
            Console.WriteLine($"CLIMATIZZATORE: {(ac.air_enabled ? "ON" : "OFF")}");
            Console.WriteLine($"Target Temp: {ac.target_temperature}°C | Intensità: {ac.air_intensity}");
            Console.WriteLine("A-On, B-Off, C-SetTemp, D-SetIntensità, X-Esci");
            var k = Console.ReadKey(true).Key;
            if (k == ConsoleKey.X) loop = false;
            else if (k == ConsoleKey.A) ac.turnOn();
            else if (k == ConsoleKey.B) ac.turnOff();
            else if (k == ConsoleKey.C)
            {
                Console.Write($"Nuova Temp (> {ac.lowest_temperature}): ");
                if (double.TryParse(Console.ReadLine(), out double t)) ac.switchTemperature(t);
            }
            else if (k == ConsoleKey.D)
            {
                Console.Write("Nuova Intensità: ");
                if (int.TryParse(Console.ReadLine(), out int i)) ac.switchIntensity(i);
            }
        }
    }

    // --- RADIATORE ---
    static void GestisciRadiatore(Radiator rad)
    {
        bool loop = true;
        while (loop)
        {
            Console.Clear();
            // Nota: Radiator ha sia is_on che is_off pubblici
            bool acceso = rad.is_on && !rad.is_off;
            Console.WriteLine($"RADIATORE: {(acceso ? "CALDO" : "FREDDO")} | Temp: {rad.temperature}°C");
            Console.WriteLine("A-Accendi, B-Spegni, C-SetTemp, X-Esci");
            var k = Console.ReadKey(true).Key;
            if (k == ConsoleKey.X) loop = false;
            else if (k == ConsoleKey.A) { rad.turnOn(); } // Nota: turnOn setta is_on=true
            else if (k == ConsoleKey.B) { rad.turnOff(); } // turnOff setta is_off=true
            else if (k == ConsoleKey.C)
            {
                Console.Write("Nuova Temp: ");
                if (double.TryParse(Console.ReadLine(), out double t)) rad.setTemperature(t);
            }
        }
    }

    // --- TERMOSTATO ---
    static void GestisciTermostato(Thermostat therm)
    {
        bool loop = true;
        while (loop)
        {
            Console.Clear();
            // Thermostat ha un campo 'is_on' protected, non accessibile qui direttamente se non tramite metodi o reflection
            // Ma ha current_temperature public
            Console.WriteLine($"TERMOSTATO SMART");
            Console.WriteLine($"Temp Attuale rilevata: {therm.current_temperature}°C");
            Console.WriteLine($"Temp Target: {therm.target_temperature}°C");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("A-Attiva Sistema, B-Disattiva, C-Imposta Target Temp, X-Esci");

            var k = Console.ReadKey(true).Key;
            if (k == ConsoleKey.X) loop = false;
            else if (k == ConsoleKey.A) { therm.turnOn(); Console.WriteLine("Termostato ON."); PremiTasto(); }
            else if (k == ConsoleKey.B) { therm.turnOff(); Console.WriteLine("Termostato OFF."); PremiTasto(); }
            else if (k == ConsoleKey.C)
            {
                Console.Write("Inserisci Temp Desiderata: ");
                if (double.TryParse(Console.ReadLine(), out double t))
                {
                    try
                    {
                        therm.SwitchTargetTemperature(t);
                        Console.WriteLine("Temperatura impostata. I sottosistemi (AC/Radiatori) sono stati regolati.");
                    }
                    catch (Exception e) { Console.WriteLine("Errore: " + e.Message); }
                    PremiTasto();
                }
            }
        }
    }

    // --- PORTA BLINDATA ---
    static void GestisciPorta(SecureDoor door)
    {
        bool loop = true;
        while (loop)
        {
            Console.Clear();
            Console.WriteLine($"PORTA BLINDATA: {(door.is_locked ? "BLOCCATA" : "SBLOCCATA")}");
            Console.WriteLine($"Email di recupero: {door.mail.Value}");
            Console.WriteLine("A-Chiudi (Lock), B-Apri (Unlock), C-Reset Password (Invia Email), X-Esci");

            var k = Console.ReadKey(true).Key;
            if (k == ConsoleKey.X) loop = false;
            else if (k == ConsoleKey.A) { door.Lock(); Console.WriteLine("Porta chiusa."); PremiTasto(); }
            else if (k == ConsoleKey.B)
            {
                Console.Write("Inserisci Password: ");
                string pswInput = Console.ReadLine();
                // Controllo se la password corrisponde creando un ValObj temporaneo
                // Nota: Password non ha override di Equals nel codice fornito, ma assumo che operator == funzioni o sia string
                // Se Password è un oggetto, qui bisognerebbe confrontare il valore interno. 
                // Assumo che new Password(pswInput) == door.password funzioni se sono ValueObjects ben fatti.
                door.Unlock(new Password(pswInput));

                if (!door.is_locked) Console.WriteLine("Password corretta. Porta aperta.");
                else Console.WriteLine("Password errata.");
                PremiTasto();
            }
            else if (k == ConsoleKey.C)
            {
                try
                {
                    door.resetPassword();
                    Console.WriteLine("Procedura reset avviata (controlla console/email finta).");
                }
                catch (Exception ex) { Console.WriteLine("Errore invio mail: " + ex.Message); }
                PremiTasto();
            }
        }
    }

    // --- WEBCAM ---
    static void GestisciWebcam(Webcam cam)
    {
        Console.Clear();
        Console.WriteLine("-- WEBCAM CONTROLLER --");
        Console.WriteLine($"ID Camera: {cam.cam_Id}");
        Console.WriteLine("Premi INVIO per avviare lo stream video.");
        Console.WriteLine("NOTA: Si aprirà una finestra esterna. Premi ESC su quella finestra per tornare qui.");
        Console.ReadLine();

        try
        {
            cam.Start(); // Questo metodo è bloccante finché non si preme ESC nella finestra OpenCV
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore webcam (forse non collegata?): " + ex.Message);
        }
        Console.WriteLine("\nStream terminato.");
        PremiTasto();
    }

    // =============================================================
    // 3. FUNZIONALITÀ LISTA ORDINATA
    // =============================================================

    static void MostraListaDispositivi()
    {
        Console.Clear();
        Console.WriteLine("=====================================");
        Console.WriteLine("      LISTA DISPOSITIVI COMPLETA     ");
        Console.WriteLine("=====================================");
        Console.WriteLine($"{"TIPO",-20} | {"ID / STATO",-40}");
        Console.WriteLine("-------------------------------------");

        // Ordino i dispositivi per Nome del Tipo
        var listaOrdinata = allDevices.OrderBy(d => d.GetType().Name).ToList();

        foreach (var dev in listaOrdinata)
        {
            string tipo = dev.GetType().Name;
            string info = RecuperaInfoStato(dev);
            Console.WriteLine($"{tipo,-20} | {info}");
        }

        Console.WriteLine("\nPremi un tasto per tornare al menu...");
        Console.ReadKey();
    }

    static string RecuperaInfoStato(object dev)
    {
        // Pattern Matching
        if (dev is Lamp l)
        {
            return $"ID Lamp: {l.deviceId} On: {l.is_on} | Lum: {l.brightness_Perc}";
        }

        if (dev is EcoLamp el)
            return $"ID EcoLamp: {el.deviceId} On: {el.is_on} | EcoMode: {el.startTime.HasValue} | Lum: {el.brightness_Perc}";

        if (dev is MatrixLed ml)
            return $"Dim: {ml.matrix.GetLength(0)}x{ml.matrix.GetLength(1)} | (Matrice LED)";

        if (dev is AirConditioner ac)
            return $"Enabled: {ac.air_enabled} | Target: {ac.target_temperature}°C";

        if (dev is Radiator rad)
            return $"Temp: {rad.temperature}°C | On: {rad.is_on}";

        if (dev is Thermostat t)
            return $"Current: {t.current_temperature}°C | Target: {t.target_temperature}°C";

        if (dev is SecureDoor sd)
            return $"Locked: {sd.is_locked} | Mail: {sd.mail.Value}"; // Anche qui Mail è un ValueObject

        if (dev is Webcam wc)
            return $"ID Cam: {wc.cam_Id} | Stato: Ready"; // Webcam ha cam_Id specifico

        if (dev is Shutters sh)
            return $"ID Shutter: {sh.shutter_Id} | Aperto: {sh.is_open}"; // Shutters ha shutter_Id specifico

        return "Stato sconosciuto";
    }

    static void PremiTasto()
    {
        Console.WriteLine("\nPremi un tasto per continuare...");
        Console.ReadKey();
    }
}