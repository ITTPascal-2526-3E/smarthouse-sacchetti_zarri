using System;
using BlaisePascal.SmartHouse.UI;

namespace BlaisePascal.SmartHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            SmartHouseHub hub = new SmartHouseHub();
            LightingManager lightUI = new LightingManager(hub);
            ClimateManager climateUI = new ClimateManager(hub);
            SecurityManager securityUI = new SecurityManager(hub);
            ShuttersManager shuttersUI = new ShuttersManager(hub);

            bool esegui = true;
            while (esegui)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("╔══════════════════════════════════════════════════╗");
                Console.WriteLine("║            SMART HOUSE - HUB CENTRALE            ║");
                Console.WriteLine("╚══════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine(" [1] Illuminazione (Lamp, EcoLamp, Led, Matrix)");
                Console.WriteLine(" [2] Climatizzazione");
                Console.WriteLine(" [3] Sicurezza (Porte)");
                Console.WriteLine(" [4] Tapparelle");
                Console.WriteLine("----------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(" [L] Visualizza Tabella Tutti i Dispositivi");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" [X] Spegni Sistema");
                Console.ResetColor();
                Console.WriteLine("====================================================");
                Console.Write("Scegli un'opzione: ");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1: case ConsoleKey.NumPad1: lightUI.MenuIlluminazione(); break;
                    case ConsoleKey.D2: case ConsoleKey.NumPad2: climateUI.MenuClimatizzazione(); break;
                    case ConsoleKey.D3: case ConsoleKey.NumPad3: securityUI.MenuSicurezza(); break;
                    case ConsoleKey.D4: case ConsoleKey.NumPad4: shuttersUI.MenuTapparelle(); break;
                    case ConsoleKey.L: hub.MostraListaDispositivi(); break;
                    case ConsoleKey.X:
                        esegui = false;
                        Console.Clear();
                        Console.WriteLine("Spegnimento del sistema in corso... Arrivederci!");
                        break;
                }
            }
        }
    }
}