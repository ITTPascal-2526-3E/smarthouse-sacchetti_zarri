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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔══════════════════════════════════════════════════╗");
                Console.WriteLine("║                  HUB SICUREZZA                   ║");
                Console.WriteLine("╚══════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine(" [1] Installa Nuova Porta Blindata (SecureDoor)");
                Console.WriteLine(" [2] Gestisci Porte e Allarmi");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine(" [X] Torna all'Hub Centrale");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.X: attivo = false; break;
                    case ConsoleKey.D1: InstallaPorta(); break;
                    case ConsoleKey.D2: GestisciPorte(); break;
                }
            }
        }

        private void InstallaPorta()
        {
            Console.Clear();
            Console.WriteLine("--- NUOVA PORTA BLINDATA ---");
            Console.Write("Brand/Nome Porta (es. Ingresso Principale): ");
            string brand = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(brand)) brand = "Porta Generica";

            Console.Write("Imposta Password (min 6 caratteri): ");
            string pass = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(pass)) pass = "123456";

            Console.Write("Email per reset password: ");
            string emailStr = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(emailStr)) emailStr = "admin@smarthouse.com";

            // Creazione con il nuovo costruttore aggiornato
            try
            {
                var nuovaPorta = new SecureDoor(new Name(brand), new Password(pass), new Email(emailStr));
                _hub.Porte.Add(nuovaPorta);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n[+] Porta '{brand}' installata e bloccata!");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nErrore durante la creazione: Dati non validi. ({ex.Message})");
            }

            Console.ResetColor();
            Console.ReadKey();
        }

        private void GestisciPorte()
        {
            // ... Tuo codice per sbloccare/bloccare le porte ...
            Console.WriteLine("\n[Funzione GestisciPorte da completare]");
            Console.ReadKey();
        }
    }
}