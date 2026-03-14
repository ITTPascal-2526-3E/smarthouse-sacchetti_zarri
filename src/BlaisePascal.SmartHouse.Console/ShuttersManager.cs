using System;
using BlaisePascal.SmartHouse.Domain.Devices.Shutters;

namespace BlaisePascal.SmartHouse.UI
{
    public class ShuttersManager
    {
        private SmartHouseHub _hub;
        public ShuttersManager(SmartHouseHub hub) { _hub = hub; }

        public void MenuScuroni()
        {
            bool attivo = true;
            while (attivo)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("╔══════════════════════════════════════════════════╗");
                Console.WriteLine("║                 HUB TAPPARELLE                   ║");
                Console.WriteLine("╚══════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine(" [1] Installa Nuova Tapparella");
                Console.WriteLine(" [2] Gestisci Tapparelle");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine(" [X] Torna all'Hub Centrale");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.X: attivo = false; break;
                    case ConsoleKey.D1:
                        _hub.Tapparelle.Add(new Shutter());
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n[+] Tapparella installata.");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D2: SelezionaTapparella(); break;
                }
            }
        }

        private void SelezionaTapparella()
        {
            if (_hub.Tapparelle.Count == 0) return;
            Console.Clear();
            for (int i = 0; i < _hub.Tapparelle.Count; i++)
                Console.WriteLine($"[{i + 1}] Tapparella");

            Console.Write("\nScegli numero: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= _hub.Tapparelle.Count)
                GestisciSingolaTapparella(_hub.Tapparelle[index - 1]);
        }

        private void GestisciSingolaTapparella(Shutter tapparella)
        {
            bool attivo = true;
            while (attivo)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"╔═════ CONTROLLO TAPPARELLA ═════╗");
                Console.ResetColor();
                Console.Write("Stato: ");
                if (tapparella.is_open) { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("APERTA"); }
                else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("CHIUSA"); }
                Console.ResetColor();
                Console.WriteLine("----------------------------------");
                Console.WriteLine(" [A] Apri");
                Console.WriteLine(" [B] Chiudi");
                Console.WriteLine(" [X] Indietro");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.A: tapparella.Open(); break;
                    case ConsoleKey.B: tapparella.Close(); break;
                    case ConsoleKey.X: attivo = false; break;
                }
            }
        }
    }
}