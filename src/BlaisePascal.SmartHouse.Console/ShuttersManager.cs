using System;
using BlaisePascal.SmartHouse.Domain.Devices.Shutters;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;

namespace BlaisePascal.SmartHouse.UI
{
    public class ShuttersManager
    {
        private SmartHouseHub _hub;
        public ShuttersManager(SmartHouseHub hub) { _hub = hub; }

        public void MenuTapparelle()
        {
            bool attivo = true;
            while (attivo)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("╔══════════════════════════════════════════════════╗");
                Console.WriteLine("║                  HUB TAPPARELLE                  ║");
                Console.WriteLine("╚══════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine(" [1] Installa Nuova Tapparella");
                Console.WriteLine(" [2] Gestisci Tapparelle");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine(" [X] Torna all'Hub Centrale");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.X: attivo = false; break;
                    case ConsoleKey.D1: InstallaTapparella(); break;
                    case ConsoleKey.D2: GestisciTapparelle(); break;
                }
            }
        }

        private void InstallaTapparella()
        {
            Console.Clear();
            Console.WriteLine("--- NUOVA TAPPARELLA ---");
            Console.Write("Brand/Nome (es. Tapparella Camera Letto): ");
            string brand = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(brand)) brand = "Finestra Generica";

            _hub.Tapparelle.Add(new Shutter(new Name(brand)));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[+] Tapparella '{brand}' installata con successo!");
            Console.ResetColor();
            Console.ReadKey();
        }

        private void GestisciTapparelle()
        {
            // ... Tuo codice per aprire/chiudere ...
            Console.WriteLine("\n[Funzione GestisciTapparelle da completare]");
            Console.ReadKey();
        }
    }
}