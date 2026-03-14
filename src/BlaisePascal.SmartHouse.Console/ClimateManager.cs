using System;
using BlaisePascal.SmartHouse.Domain.Devices.Climate;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;

namespace BlaisePascal.SmartHouse.UI
{
    public class ClimateManager
    {
        private SmartHouseHub _hub;
        public ClimateManager(SmartHouseHub hub) { _hub = hub; }

        public void MenuClima()
        {
            bool attivo = true;
            while (attivo)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("╔══════════════════════════════════════════════════╗");
                Console.WriteLine("║               HUB CLIMATIZZAZIONE                ║");
                Console.WriteLine("╚══════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine(" [1] Installa Condizionatore");
                Console.WriteLine(" [2] Gestisci Condizionatori");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine(" [X] Torna all'Hub Centrale");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.X: attivo = false; break;
                    case ConsoleKey.D1:
                        _hub.Condizionatori.Add(new AirConditioner(20.0, new Air(3)));
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n[+] Condizionatore installato!");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D2: SelezionaAC(); break;
                }
            }
        }

        private void SelezionaAC()
        {
            if (_hub.Condizionatori.Count == 0) return;
            Console.Clear();
            for (int i = 0; i < _hub.Condizionatori.Count; i++)
            {
                var ac = _hub.Condizionatori[i];
                Console.Write($"[{i + 1}] Condizionatore -> ");
                if (ac.air_enabled) { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("ON"); }
                else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("OFF"); }
                Console.ResetColor();
            }
            Console.Write("\nScegli numero (0 per uscire): ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= _hub.Condizionatori.Count)
                GestisciSingoloAC(_hub.Condizionatori[index - 1]);
        }

        private void GestisciSingoloAC(AirConditioner ac)
        {
            bool attivo = true;
            while (attivo)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"╔═════ CONDIZIONATORE ═════╗");
                Console.ResetColor();
                Console.Write("Stato:       ");
                if (ac.air_enabled) { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("❄ ACCESO"); }
                else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("○ SPENTO"); }
                Console.ResetColor();

                Console.WriteLine($"Temp Target: {ac.target_temperature}°C");
                Console.WriteLine($"Intensità:   {ac.air_intensity}/5");
                Console.WriteLine("----------------------------");
                Console.WriteLine(" [A] Accendi  | [B] Spegni");
                Console.WriteLine(" [C] Modifica Temperatura");
                Console.WriteLine(" [D] Modifica Intensità");
                Console.WriteLine(" [X] Indietro");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.A: ac.turnOn(); break;
                    case ConsoleKey.B: ac.turnOff(); break;
                    case ConsoleKey.C:
                        Console.Write("\nNuova Temp (°C): ");
                        if (double.TryParse(Console.ReadLine(), out double t)) ac.switchTemperature(t);
                        break;
                    case ConsoleKey.D:
                        Console.Write("\nNuova Intensità (1-5): ");
                        if (int.TryParse(Console.ReadLine(), out int i) && i >= 1 && i <= 5) ac.switchIntensity(i);
                        break;
                    case ConsoleKey.X: attivo = false; break;
                }
            }
        }
    }
}