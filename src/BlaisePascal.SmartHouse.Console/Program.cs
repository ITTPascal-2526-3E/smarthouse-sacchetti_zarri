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
        // Uso i namespace completi come nel tuo screenshot per sicurezza
        Lamp lamp = new Lamp(new Power(10), new Name("Philips"), new Brightness(800));
        EcoLamp ecoLamp = new EcoLamp(new Power(5), new Name("Beghelli"), new Brightness(600));

        // --- 2. DIZIONARI MENU (CORRETTO SENZA DUPLICATI) ---
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

        // --- CICLO PRINCIPALE (HOME) ---
        while (eseguiProgramma)
        {
            Console.Clear(); // Pulisce sempre all'inizio del ciclo
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

            // Uscita dal programma
            if (inputHome == ConsoleKey.X)
            {
                eseguiProgramma = false;
                Console.WriteLine("\nChiusura in corso... Arrivederci!");
                continue;
            }

            // --- MENU ILLUMINAZIONE ---
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
                    Console.WriteLine("D - Matrice di LEDs");
                    Console.WriteLine("------------------------");
                    Console.WriteLine("X - TORNA INDIETRO");

                    ConsoleKey inputIllum = Console.ReadKey(true).Key;

                    if (inputIllum == ConsoleKey.X)
                    {
                        menuIlluminazioneAttivo = false; // Torna alla Home
                    }
                    // --- GESTIONE LAMPADA STANDARD ---
                    else if (inputIllum == ConsoleKey.A)
                    {
                        bool menuLampadaAttivo = true;
                        while (menuLampadaAttivo)
                        {
                            Console.Clear();
                            string stato = lamp.is_on ? "ON" : "OFF";
                            // NOTA: Qui uso brightness_Perc invece di CurrentBrightness
                            // Assicurati che brightness_Perc sia PUBLIC nella classe padre LampModel
                            Console.WriteLine($"-- LAMPADA PHILIPS ({stato}) --");
                            Console.WriteLine($"Luminosità impostata: {lamp.brightness_Perc}");
                            Console.WriteLine("-----------------------------");
                            Console.WriteLine("A - Accendi");
                            Console.WriteLine("B - Spegni");
                            Console.WriteLine("C - Regola intensità");
                            Console.WriteLine("D - Cambia colore");
                            Console.WriteLine("X - TORNA INDIETRO");

                            ConsoleKey inputLamp = Console.ReadKey(true).Key;

                            if (inputLamp == ConsoleKey.X)
                            {
                                menuLampadaAttivo = false;
                            }
                            else
                            {
                                // PULIZIA CONSOLE PRIMA DELL'AZIONE
                                Console.Clear();
                                bool attendiInput = true; // Serve per far leggere il messaggio all'utente

                                if (inputLamp == ConsoleKey.A)
                                {
                                    if (!lamp.is_on)
                                    {
                                        lamp.turnOn();
                                        Console.WriteLine("-> Lampada accesa con successo!");
                                    }
                                    else Console.WriteLine("-> La lampada è già accesa!");
                                }
                                else if (inputLamp == ConsoleKey.B)
                                {
                                    if (lamp.is_on)
                                    {
                                        lamp.turnOff();
                                        Console.WriteLine("-> Lampada spenta.");
                                    }
                                    else Console.WriteLine("-> La lampada è già spenta!");
                                }
                                else if (inputLamp == ConsoleKey.C)
                                {
                                    if (lamp.is_on)
                                    {
                                        Console.WriteLine("Inserisci il valore della luminosità (es. 50): ");
                                        string inputLum = Console.ReadLine();
                                        if (int.TryParse(inputLum, out int val))
                                        {
                                            lamp.adjustBrightness(new Brightness(val));
                                            Console.Clear(); // Pulisco anche dopo l'input numerico
                                            Console.WriteLine($"-> Luminosità impostata a: {val}");
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Errore: devi inserire un numero valido.");
                                        }
                                    }
                                    else Console.WriteLine("Accendi prima la lampada!");
                                }
                                else if (inputLamp == ConsoleKey.D)
                                {
                                    if (lamp.is_on)
                                    {
                                        Console.WriteLine("-- SCEGLI COLORE --");
                                        Console.WriteLine("A-White, B-Warm, C-Cool, D-Red, E-Green, F-Blue");
                                        ConsoleKey colKey = Console.ReadKey(true).Key;

                                        Console.Clear(); // Pulisco subito dopo la scelta colore

                                        if (menuColori.TryGetValue(colKey, out LampColor col))
                                        {
                                            lamp.ChangeColor(col);
                                            Console.WriteLine($"-> Colore cambiato in: {col}");
                                        }
                                        else Console.WriteLine("Colore non valido o tasto errato.");
                                    }
                                    else Console.WriteLine("Accendi prima la lampada!");
                                }
                                else
                                {
                                    attendiInput = false; // Se preme un tasto a caso non valido
                                }

                                if (attendiInput)
                                {
                                    Console.WriteLine("\nPremi un tasto per continuare...");
                                    Console.ReadKey();
                                }
                            }
                        }
                    }
                    // --- GESTIONE ECOLAMPADA ---
                    else if (inputIllum == ConsoleKey.B)
                    {
                        bool menuEcoAttivo = true;
                        while (menuEcoAttivo)
                        {
                            Console.Clear();
                            string statoEco = ecoLamp.is_on ? "ON" : "OFF";
                            Console.WriteLine($"-- ECOLAMPADA BEGHELLI ({statoEco}) --");
                            // Uso brightness_Perc anche qui
                            Console.WriteLine($"Luminosità Attuale: {ecoLamp.brightness_Perc}");
                            Console.WriteLine($"Data accensione: {(ecoLamp.startTime.HasValue ? ecoLamp.startTime.ToString() : "Non attiva")}");
                            Console.WriteLine("-----------------------------------");
                            Console.WriteLine("A - Accendi");
                            Console.WriteLine("B - Spegni");
                            Console.WriteLine("C - Attiva Modalità Eco (Start)");
                            Console.WriteLine("D - Controlla Modalità Eco (Logica)");
                            Console.WriteLine("X - TORNA INDIETRO");

                            ConsoleKey inputEco = Console.ReadKey(true).Key;

                            if (inputEco == ConsoleKey.X)
                            {
                                menuEcoAttivo = false;
                            }
                            else
                            {
                                Console.Clear();
                                bool attendiInput = true;

                                if (inputEco == ConsoleKey.A)
                                {
                                    if (!ecoLamp.is_on) { ecoLamp.turnOn(); Console.WriteLine("-> Ecolampada Accesa."); }
                                    else Console.WriteLine("-> Già accesa.");
                                }
                                else if (inputEco == ConsoleKey.B)
                                {
                                    if (ecoLamp.is_on) { ecoLamp.turnOff(); Console.WriteLine("-> Ecolampada Spenta."); }
                                    else Console.WriteLine("-> Già spenta.");
                                }
                                else if (inputEco == ConsoleKey.C)
                                {
                                    if (ecoLamp.is_on)
                                    {
                                        ecoLamp.startEcoMode();
                                        Console.WriteLine("-> Timer Eco avviato.");
                                    }
                                    else Console.WriteLine("Accendi prima la lampada.");
                                }
                                else if (inputEco == ConsoleKey.D)
                                {
                                    if (ecoLamp.is_on)
                                    {
                                        ecoLamp.ecoMode();
                                        Console.WriteLine("-> Logica Eco eseguita (luminosità aggiornata se necessario).");
                                    }
                                    else Console.WriteLine("Accendi prima la lampada.");
                                }
                                else
                                {
                                    attendiInput = false;
                                }

                                if (attendiInput)
                                {
                                    Console.WriteLine("\nPremi un tasto per continuare...");
                                    Console.ReadKey();
                                }
                            }
                        }
                    }
                }
            }
            // --- ALTRI MENU ---
            else if (inputHome == ConsoleKey.B || inputHome == ConsoleKey.C || inputHome == ConsoleKey.D || inputHome == ConsoleKey.E)
            {
                Console.WriteLine("\nFunzionalità non ancora implementata.");
                Console.WriteLine("Premi un tasto per tornare alla Home...");
                Console.ReadKey();
            }
        }
    }
}