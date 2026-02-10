using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;
using BlaisePascal.SmartHouse.Domain.Climate;
using BlaisePascal.SmartHouse.Domain.Lamps;
using BlaisePascal.SmartHouse.Domain.Program;
using BlaisePascal.SmartHouse.Domain.Security;
using BlaisePascal.SmartHouse.Domain.Shutters;
using System;
using System.Collections.Generic;

public class Program
{
    static void Main(string[] args)
    {
        // --- 1. ISTANZIAMENTO OGGETTI ---
        Lamp lamp = new Lamp(new Power(10), new Name("Philips"), new Brightness(800));
        EcoLamp ecoLamp = new EcoLamp(new Power(5), new Name("Beghelli"), new Brightness(600));

        List<Lamp> gardenGroup = new List<Lamp>();
        gardenGroup.Add(new Lamp(new Power(10), new Name("Giardino 1"), new Brightness(500)));
        gardenGroup.Add(new Lamp(new Power(10), new Name("Giardino 2"), new Brightness(500)));
        gardenGroup.Add(new Lamp(new Power(10), new Name("Giardino 3"), new Brightness(500)));

        // --- MODIFICA QUI: Matrice 10x10 ---
        MatrixLed ledMatrix = new MatrixLed();
        Led templateLed = new Led(new Power(1), new Name("Pixel"), new Brightness(100));

        // Genero una matrice 10 righe x 10 colonne
        ledMatrix.GenerateMatrix(10, 10, templateLed);


        // --- 2. DIZIONARI MENU ---
        var menuColori = new Dictionary<ConsoleKey, LampColor>
        {
            { ConsoleKey.A, LampColor.White},
            { ConsoleKey.B, LampColor.WarmWhite},
            { ConsoleKey.C, LampColor.CoolWhite},
            { ConsoleKey.D, LampColor.Red},
            { ConsoleKey.E, LampColor.Green},
            { ConsoleKey.F, LampColor.Blue},
            { ConsoleKey.G, LampColor.Yellow},
            { ConsoleKey.H, LampColor.Purple}
        };

        bool eseguiProgramma = true;

        while (eseguiProgramma)
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine("        SMARTHOUSE - HOME            ");
            Console.WriteLine("=====================================");
            Console.WriteLine("A - Illuminazione");
            Console.WriteLine("B - Climatizzazione");
            Console.WriteLine("C - Scuroni");
            Console.WriteLine("D - Sicurezza Intrusi");
            Console.WriteLine("E - Sicurezza Antincendio");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("X - CHIUDI PROGRAMMA");
            Console.WriteLine("=====================================");

            ConsoleKey inputHome = Console.ReadKey(true).Key;

            if (inputHome == ConsoleKey.X)
            {
                eseguiProgramma = false;
                Console.WriteLine("\nChiusura in corso... Arrivederci!");
                continue;
            }

            if (inputHome == ConsoleKey.A)
            {
                bool menuIlluminazioneAttivo = true;
                while (menuIlluminazioneAttivo)
                {
                    Console.Clear();
                    Console.WriteLine("-- MENU ILLUMINAZIONE --");
                    Console.WriteLine("A - Lampada Standard");
                    Console.WriteLine("B - Ecolampada");
                    Console.WriteLine("C - Gruppo di lampade");
                    Console.WriteLine("D - Matrice di LEDs (10x10)");
                    Console.WriteLine("------------------------");
                    Console.WriteLine("X - TORNA INDIETRO");

                    ConsoleKey inputIllum = Console.ReadKey(true).Key;

                    if (inputIllum == ConsoleKey.X) menuIlluminazioneAttivo = false;
                    else if (inputIllum == ConsoleKey.A) GestisciLampadaSingola(lamp, menuColori);
                    else if (inputIllum == ConsoleKey.B) GestisciEcoLampada(ecoLamp);
                    else if (inputIllum == ConsoleKey.C) GestisciGruppo(gardenGroup);
                    else if (inputIllum == ConsoleKey.D) GestisciMatrice(ledMatrix);
                }
            }
            else if (inputHome == ConsoleKey.B || inputHome == ConsoleKey.C || inputHome == ConsoleKey.D || inputHome == ConsoleKey.E)
            {
                Console.WriteLine("\nFunzionalità non ancora implementata.");
                PremiTasto();
            }
        }
    }

    // ================= METODI DI GESTIONE =================

    static void GestisciLampadaSingola(Lamp lamp, Dictionary<ConsoleKey, LampColor> menuColori)
    {
        bool attivo = true;
        while (attivo)
        {
            Console.Clear();
            Console.WriteLine($"-- LAMPADA STANDARD ({(lamp.is_on ? "ON" : "OFF")}) --");
            Console.WriteLine($"Luminosità: {lamp.brightness_Perc}");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("A - Accendi / B - Spegni");
            Console.WriteLine("C - Regola intensità");
            Console.WriteLine("D - Cambia colore");
            Console.WriteLine("X - Indietro");

            ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.X) return;

            Console.Clear();
            if (key == ConsoleKey.A) { lamp.turnOn(); Console.WriteLine("Accesa."); }
            else if (key == ConsoleKey.B) { lamp.turnOff(); Console.WriteLine("Spenta."); }
            else if (key == ConsoleKey.C)
            {
                Console.Write("Valore (0-100): ");
                if (int.TryParse(Console.ReadLine(), out int val))
                    lamp.adjustBrightness(new Brightness(val));
            }
            else if (key == ConsoleKey.D)
            {
                if (lamp.is_on)
                {
                    Console.WriteLine("Scegli: A-White, B-Warm, C-Cool, D-Red, E-Green, F-Blue");
                    ConsoleKey colKey = Console.ReadKey(true).Key;
                    if (menuColori.TryGetValue(colKey, out LampColor col))
                        lamp.ChangeColor(col);
                }
                else Console.WriteLine("Accendi prima.");
            }
            PremiTasto();
        }
    }

    static void GestisciEcoLampada(EcoLamp ecoLamp)
    {
        bool attivo = true;
        while (attivo)
        {
            Console.Clear();
            Console.WriteLine($"-- ECOLAMPADA ({(ecoLamp.is_on ? "ON" : "OFF")}) --");
            Console.WriteLine($"Luminosità: {ecoLamp.brightness_Perc}");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("A - Accendi / B - Spegni");
            Console.WriteLine("C - Start Timer Eco / D - Logica Eco");
            Console.WriteLine("X - Indietro");

            ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.X) return;

            Console.Clear();
            if (key == ConsoleKey.A) { ecoLamp.turnOn(); Console.WriteLine("Eco Accesa."); }
            else if (key == ConsoleKey.B) { ecoLamp.turnOff(); Console.WriteLine("Eco Spenta."); }
            else if (key == ConsoleKey.C) { ecoLamp.startEcoMode(); Console.WriteLine("Timer start."); }
            else if (key == ConsoleKey.D) { ecoLamp.ecoMode(); Console.WriteLine("Logica eseguita."); }
            PremiTasto();
        }
    }

    static void GestisciGruppo(List<Lamp> gruppo)
    {
        bool attivo = true;
        while (attivo)
        {
            Console.Clear();
            int accese = 0; foreach (var l in gruppo) if (l.is_on) accese++;
            Console.WriteLine($"-- GRUPPO GIARDINO ({accese}/{gruppo.Count} ON) --");
            Console.WriteLine("A - Accendi TUTTE / B - Spegni TUTTE");
            Console.WriteLine("C - Imposta Luminosità TUTTE");
            Console.WriteLine("X - Indietro");

            ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.X) return;

            Console.Clear();
            if (key == ConsoleKey.A) { foreach (var l in gruppo) l.turnOn(); Console.WriteLine("Tutte ON."); }
            else if (key == ConsoleKey.B) { foreach (var l in gruppo) l.turnOff(); Console.WriteLine("Tutte OFF."); }
            else if (key == ConsoleKey.C)
            {
                Console.Write("Luminosità gruppo: ");
                if (int.TryParse(Console.ReadLine(), out int val))
                {
                    Brightness b = new Brightness(val);
                    foreach (var l in gruppo) l.adjustBrightness(b);
                }
            }
            PremiTasto();
        }
    }

    static void GestisciMatrice(MatrixLed matrix)
    {
        bool attivo = true;
        while (attivo)
        {
            Console.Clear();
            Console.WriteLine($"-- MATRICE LED {matrix.matrix.GetLength(0)}x{matrix.matrix.GetLength(1)} --");

            Console.WriteLine("\n[ STATO VISIVO (O=Acceso, .=Spento) ]");
            // Disegno la matrice
            for (int i = 0; i < matrix.matrix.GetLength(0); i++)
            {
                Console.Write(" "); // Margine sinistro
                for (int j = 0; j < matrix.matrix.GetLength(1); j++)
                {
                    var led = matrix.GetLed(i, j);
                    // Uso icone compatte per farla stare bene a schermo
                    Console.Write(led.is_on ? "O " : ". ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("------------------------");
            Console.WriteLine("A - Accendi Tutto");
            Console.WriteLine("B - Spegni Tutto");
            Console.WriteLine("C - Attiva Pattern Scacchiera");
            Console.WriteLine("D - Imposta Intensità Globale");
            Console.WriteLine("X - Indietro");

            ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.X) return;

            Console.Clear();
            if (key == ConsoleKey.A) { matrix.turnOn(); Console.WriteLine("Matrix ON."); }
            else if (key == ConsoleKey.B) { matrix.turnOff(); Console.WriteLine("Matrix OFF."); }
            else if (key == ConsoleKey.C)
            {
                matrix.PatternCheckerBoard();
                Console.WriteLine("Pattern Scacchiera applicato (Controlla il disegno!).");
            }
            else if (key == ConsoleKey.D)
            {
                Console.Write("Intensità (0-100): ");
                if (int.TryParse(Console.ReadLine(), out int val))
                    matrix.SetIntensityAll(new Brightness(val));
            }
            PremiTasto();
        }
    }

    static void PremiTasto()
    {
        Console.WriteLine("\nPremi un tasto per continuare...");
        Console.ReadKey();
    }
}