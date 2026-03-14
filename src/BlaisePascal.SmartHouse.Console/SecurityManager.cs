using System;
using BlaisePascal.SmartHouse.Domain.Devices.Security;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;

namespace BlaisePascal.SmartHouse.UI
{
    public class SecurityManager
    {
        private SmartHouseHub _hub;
        public SecurityManager(SmartHouseHub hub) { _hub = hub; }

        public void MenuSicurezza()
        {
            bool attivo = true;
            while (attivo)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("╔══════════════════════════════════════════════════╗");
                Console.WriteLine("║                 HUB SICUREZZA                    ║");
                Console.WriteLine("╚══════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine(" [1] Installa Nuova Porta Sicura");
                Console.WriteLine(" [2] Gestisci Porte");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine(" [X] Torna all'Hub Centrale");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.X: attivo = false; break;
                    case ConsoleKey.D1: InstallaPorta(); break;
                    case ConsoleKey.D2: SelezionaPorta(); break;
                }
            }
        }

        private void InstallaPorta()
        {
            Console.Clear();
            Console.WriteLine("--- INSTALLAZIONE PORTA SICURA ---");
            Console.Write("Imposta password: ");
            string pass = Console.ReadLine();
            Console.Write("Email di sicurezza: ");
            string mail = Console.ReadLine();

            _hub.Porte.Add(new SecureDoor(new Password(pass ?? "1234"), new Email(mail ?? "admin@casa.it")));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[+] Porta installata e BLOCCATA di default.");
            Console.ResetColor();
            Console.ReadKey();
        }

        private void SelezionaPorta()
        {
            if (_hub.Porte.Count == 0) return;
            Console.Clear();
            for (int i = 0; i < _hub.Porte.Count; i++)
                Console.WriteLine($"[{i + 1}] Porta (Mail: {_hub.Porte[i].mail.Value})");

            Console.Write("\nScegli numero: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= _hub.Porte.Count)
                GestisciSingolaPorta(_hub.Porte[index - 1]);
        }

        private void GestisciSingolaPorta(SecureDoor porta)
        {
            bool attivo = true;
            while (attivo)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"╔═════ CONTROLLO PORTA ═════╗");
                Console.ResetColor();
                Console.Write("Stato: ");
                if (porta.is_locked) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("🔒 BLOCCATA"); }
                else { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("🔓 SBLOCCATA"); }
                Console.ResetColor();
                Console.WriteLine("-----------------------------");
                Console.WriteLine(" [A] Sblocca (Richiede Password)");
                Console.WriteLine(" [B] Blocca");
                Console.WriteLine(" [X] Indietro");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.A:
                        Console.Write("\nInserisci Password: ");
                        porta.Unlock(new Password(Console.ReadLine()));
                        if (porta.is_locked) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Password Errata!"); Console.ResetColor(); Console.ReadKey(); }
                        break;
                    case ConsoleKey.B: porta.Lock(); break;
                    case ConsoleKey.X: attivo = false; break;
                }
            }
        }
    }
}