using System;
using BlaisePascal.SmartHouse.Domain.Devices.Climate;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;

namespace BlaisePascal.SmartHouse.UI
{
    public class ClimateManager
    {
        private SmartHouseHub _hub;

        public ClimateManager(SmartHouseHub hub)
        {
            _hub = hub;
        }

        public void MenuClimatizzazione()
        {
            bool attivo = true;
            while (attivo)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔══════════════════════════════════════════════════╗");
                Console.WriteLine("║                HUB CLIMATIZZAZIONE               ║");
                Console.WriteLine("╚══════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine(" [1] Installa Nuovo Termostato (Sistema Completo)");
                Console.WriteLine(" [2] Gestisci Clima (Tramite Termostato)");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine(" [X] Torna all'Hub Centrale");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.X: attivo = false; break;
                    case ConsoleKey.D1: InstallaTermostato(); break;
                    case ConsoleKey.D2: GestisciClima(); break;
                }
            }
        }

        private void InstallaTermostato()
        {
            Console.Clear();
            Console.WriteLine("--- INSTALLAZIONE SISTEMA CLIMATICO ---");
            Console.Write("Brand/Nome Termostato (es. Nest): ");
            string brand = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(brand)) brand = "Generico";

            Console.Write("Temperatura Attuale Rilevata (°C): ");
            if (!double.TryParse(Console.ReadLine(), out double temp)) temp = 20.0;

            var termostato = new Thermostat(new Name(brand), temp);

            // FIX: Inizializzo almeno un radiatore nell'array per evitare 
            // che la tua logica in SwitchTargetTemperature lanci un'Exception!
            termostato.radiators[0] = new Radiator(temp);

            _hub.Termostati.Add(termostato);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[+] Sistema termostatico '{brand}' installato con successo!");
            Console.ResetColor();
            Console.ReadKey();
        }

        private void GestisciClima()
        {
            bool attivo = true;
            while (attivo)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("--- ELENCO TERMOSTATI INSTALLATI ---");
                Console.ResetColor();

                if (_hub.Termostati.Count == 0)
                {
                    Console.WriteLine("\nNessun termostato installato.");
                    Console.ReadKey();
                    return;
                }

                for (int i = 0; i < _hub.Termostati.Count; i++)
                {
                    var term = _hub.Termostati[i];
                    // ORA POSSIAMO LEGGERE LO STATO!
                    string stato = term.is_on ? "ON" : "OFF";
                    Console.WriteLine($" [{i + 1}] Termostato {_hub.EstraiNomeBrand(term)} - Stato: [{stato}] - Temp. Attuale: {term.current_temperature}°C");
                }

                Console.WriteLine("\n----------------------------------------------------");
                Console.Write("Scegli il termostato da gestire (0 per uscire): ");

                if (int.TryParse(Console.ReadLine(), out int scelta))
                {
                    if (scelta == 0) return;

                    if (scelta > 0 && scelta <= _hub.Termostati.Count)
                    {
                        GestioneTermostato(_hub.Termostati[scelta - 1]);
                    }
                    else
                    {
                        Console.WriteLine("Scelta non valida.");
                        Console.ReadKey();
                    }
                }
            }
        }

        private void GestioneTermostato(Thermostat term)
        {
            bool attivo = true;
            while (attivo)
            {
                Console.Clear();
                Console.WriteLine($"╔═════ CONTROLLO: TERMOSTATO ({_hub.EstraiNomeBrand(term)}) ═════╗");
                // STAMPIAMO LO STATO ANCHE QUI
                Console.WriteLine($"Stato Sistema: {(term.is_on ? "● ACCESO (ON)" : "○ SPENTO (OFF)")}");
                Console.WriteLine($"Temp. Attuale Rilevata: {term.current_temperature}°C");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine(" [1] Accendi Termostato");
                Console.WriteLine(" [2] Spegni Termostato");
                Console.WriteLine(" [3] Regola Temperatura Target");
                Console.WriteLine(" [X] Indietro");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                        term.turnOn();
                        break;
                    case ConsoleKey.D2:
                        term.turnOff();
                        break;
                    case ConsoleKey.D3:
                        Console.Write("\nNuova Temperatura Desiderata (°C): ");
                        if (double.TryParse(Console.ReadLine(), out double t))
                        {
                            try
                            {
                                term.SwitchTargetTemperature(t);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"\nOperazione completata! Il termostato ha gestito i dispositivi e la temperatura ora è a {term.current_temperature}°C.");
                            }
                            catch (Exception)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nErrore: I radiatori non hanno risposto correttamente (Array nullo).");
                            }
                            Console.ResetColor();
                            Console.ReadKey();
                        }
                        break;
                    case ConsoleKey.X:
                        attivo = false;
                        break;
                }
            }
        }
    }
}