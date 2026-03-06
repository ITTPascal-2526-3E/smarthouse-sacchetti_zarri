using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;
using BlaisePascal.SmartHouse.Domain.AbstractInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using BlaisePascal.SmartHouse.Domain.Devices.Lamps;
using BlaisePascal.SmartHouse.Domain.Devices.Security;
using BlaisePascal.SmartHouse.Domain.Devices.Shutters;
using BlaisePascal.SmartHouse.Domain.Devices.Climate;
using BlaisePascal.SmartHouse.Domain.Devices.Lamps.LampsInterfaces.Repositories;
using BlaisePascal.SmartHouse.infrastructure.Repositories.Devices.Lamps;

public class Program
{
    static ILampRepository lampRepo = new InMemoryLampRepository();

    static List<object> allDevices = new List<object>();

    static void Main(string[] args)
    {
        // =============================================================
        // 1. ISTANZIAMENTO OGGETTI (Le Lamp standard sono nel Repo!)
        // =============================================================

        EcoLamp ecoLamp = new EcoLamp(new Power(5), new Name("Beghelli Eco"), new Brightness(600));
        MatrixLed ledMatrix = new MatrixLed();
        Led templateLed = new Led(new Power(1), new Name("Pixel"), new Brightness(100));
        ledMatrix.GenerateMatrix(5, 5, templateLed);

        AirConditioner airCond = new AirConditioner(16.0, new Air(3));
        Radiator radiator = new Radiator(20.0);
        Thermostat thermostat = new Thermostat(19.5);

        SecureDoor door = new SecureDoor(new Password("1234"), new Email("admin@house.com"));
        Shutter shutters = new Shutter();

        // Aggiungo gli altri dispositivi ad allDevices (Le Lamp le prenderemo dal repo!)
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
            Console.WriteLine("     SMARTHOUSE - MAIN HUB           ");
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
                    break;

                case ConsoleKey.A:
                    MenuIlluminazione(ecoLamp, ledMatrix, menuColori); // Rimosso "lamp", ora usa il repo
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

    static void MenuIlluminazione(EcoLamp eco, MatrixLed matrix, Dictionary<ConsoleKey, LampColor> colori)
    {
        bool attivo = true;
        while (attivo)
        {
            Console.Clear();
            Console.WriteLine("-- MENU ILLUMINAZIONE --");
            Console.WriteLine("1 - Hub Lampade Standard (Aggiungi/Gestisci via Repository)");
            Console.WriteLine("2 - Gestisci Ecolampada");
            Console.WriteLine("3 - Gestisci Matrice LED");
            Console.WriteLine("X - Torna Indietro");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.X: attivo = false; break;
                case ConsoleKey.D1: MenuRepositoryLampade(colori); break; // Nuovo menu per il repository
                case ConsoleKey.D2: GestisciEcoLampada(eco); break;
                case ConsoleKey.D3: GestisciMatrice(matrix); break;
            }
        }
    }

    // --- NUOVO MENU: HUB LAMPADE (USA IL REPOSITORY) ---
    static void MenuRepositoryLampade(Dictionary<ConsoleKey, LampColor> colori)
    {
        bool attivo = true;
        while (attivo)
        {
            Console.Clear();
            Console.WriteLine("-- HUB LAMPADE STANDARD (REPOSITORY) --");
            var lamps = lampRepo.GetAll();
            
            if (lamps.Count == 0)
            {
                Console.WriteLine("Nessuna lampada presente.");
            }
            else
            {
                for (int i = 0; i < lamps.Count; i++)
                {
                    // Assumo che 'name' abbia una proprietà 'Value' (o simile) per estrarre la stringa.
                    // Se 'Name' è una classe complessa, potresti dover usare lamps[i].name.ToString()
                    Console.WriteLine($"{i + 1} - {lamps[i].brand2.Value} (ID: {lamps[i].deviceId}) | ON: {lamps[i].is_on} | Lum: {lamps[i].brightness_Perc}");
                }
            }
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("A - Aggiungi Nuova Lampada");
            Console.WriteLine("S - Seleziona Lampada da Gestire");
            Console.WriteLine("X - Torna Indietro");

            var k = Console.ReadKey(true).Key;
            
            if (k == ConsoleKey.X) attivo = false;
            else if (k == ConsoleKey.A)
            {
                Console.Clear();
                Console.WriteLine("--- CREAZIONE NUOVA LAMPADA ---");

                // 1. Richiesta Brand / Nome
                Console.Write("Inserisci il Brand o Nome della lampada: ");
                string brandInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(brandInput)) 
                {
                    brandInput = $"Lampada_Generica_{lamps.Count + 1}"; // Fallback se premi solo invio
                }

                // 2. Richiesta Potenza
                Console.Write("Inserisci la potenza (es. 10): ");
                if (!int.TryParse(Console.ReadLine(), out int p)) 
                {
                    p = 10; // Valore di default se l'utente inserisce lettere per sbaglio
                    Console.WriteLine("Valore non valido. Impostato a 10 di default.");
                }

                // 3. Richiesta Luminosità
                Console.Write("Inserisci la luminosità iniziale (0-100): ");
                if (!int.TryParse(Console.ReadLine(), out int b)) 
                {
                    b = 100; // Valore di default
                    Console.WriteLine("Valore non valido. Impostato a 100 di default.");
                }

                // Creazione e salvataggio nel repository
                Lamp newLamp = new Lamp(new Power(p), new Name(brandInput), new Brightness(b));
                lampRepo.Add(newLamp);
                
                Console.WriteLine($"\nLampada '{brandInput}' aggiunta con successo!");
                PremiTasto();
            }
            else if (k == ConsoleKey.S)
            {
                Console.Write("\nDigita il numero della lampada da gestire (es. 1): ");
                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= lamps.Count)
                {
                    GestisciLampada(lamps[index - 1], colori); 
                }
                else
                {
                    Console.WriteLine("Selezione non valida.");
                    PremiTasto();
                }
            }
        }
    }


    /* ... I MENU CLIMA, SICUREZZA E SCURONI RESTANO IDENTICI, LI OMETTO PER BREVITÀ ... */
    static void MenuClima(AirConditioner ac, Radiator rad, Thermostat therm) { /* Uguale a prima */ }
    static void MenuSicurezza(SecureDoor door) { /* Uguale a prima */ }
    static void MenuScuroni(Shutter shutters) { /* Uguale a prima */ }

    // =============================================================
    // GESTIONE SPECIFICA DISPOSITIVI
    // =============================================================

    static void GestisciLampada(Lamp lamp, Dictionary<ConsoleKey, LampColor> menuColori)
    {
        bool loop = true;
        while (loop)
        {
            Console.Clear();
            Console.WriteLine($"LAMPADA SELEZIONATA: {(lamp.is_on ? "ON" : "OFF")} | Lum: {lamp.brightness_Perc} | Col: {lamp.Color}");
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
                var c = Console.ReadKey(true).Key;
                if (menuColori.TryGetValue(c, out LampColor col))
                {
                    lamp.ChangeColor(col);
                    Console.WriteLine($"Colore cambiato a {lamp.Color}.");
                }
                else Console.WriteLine("Scelta colore non valida.");
                PremiTasto();
            }
        }
    }

    /* ... GESTISCI ECOLAMPADA, MATRICE, AC, ECC. RESTANO IDENTICI ... */
    static void GestisciEcoLampada(EcoLamp lamp) { /* ... */ }
    static void GestisciMatrice(MatrixLed matrix) { /* ... */ }
    static void GestisciAC(AirConditioner ac) { /* ... */ }
    static void GestisciRadiatore(Radiator rad) { /* ... */ }
    static void GestisciTermostato(Thermostat therm) { /* ... */ }
    static void GestisciPorta(SecureDoor door) { /* ... */ }

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

        // Uniamo i dispositivi della lista statica globale con quelli dal Repository
        var tuttiDispositivi = new List<object>(allDevices);
        tuttiDispositivi.AddRange(lampRepo.GetAll()); // <-- Aggiungo le lampade dal repo al volo

        // Ordino i dispositivi per Nome del Tipo
        var listaOrdinata = tuttiDispositivi.OrderBy(d => d.GetType().Name).ToList();

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
        if (dev is Lamp l)
            return $"ID Lamp: {l.deviceId} On: {l.is_on} | Lum: {l.brightness_Perc}";

        if (dev is EcoLamp el)
            return $"ID EcoLamp: {el.deviceId} On: {el.is_on} | EcoMode: {el.startTime.HasValue}";

        if (dev is MatrixLed ml)
            return $"Dim: {ml.matrix.GetLength(0)}x{ml.matrix.GetLength(1)} | (Matrice LED)";

        if (dev is AirConditioner ac)
            return $"Enabled: {ac.air_enabled} | Target: {ac.target_temperature}°C";

        if (dev is Radiator rad)
            return $"Temp: {rad.temperature}°C | On: {rad.is_on}";

        if (dev is Thermostat t)
            return $"Current: {t.current_temperature}°C | Target: {t.target_temperature}°C";

        if (dev is SecureDoor sd)
            return $"Locked: {sd.is_locked} | Mail: {sd.mail.Value}";

        if (dev is Shutter sh)
            return $"ID Shutter: {sh.shutter_Id} | Aperto: {sh.is_open}";

        return "Stato sconosciuto";
    }

    static void PremiTasto()
    {
        Console.WriteLine("\nPremi un tasto per continuare...");
        Console.ReadKey();
    }
}