using System;
using BlaisePascal.SmartHouse.Domain.Devices.Lamps;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;

namespace BlaisePascal.SmartHouse.UI
{
    public class LightingManager
    {
        private SmartHouseHub _hub;
        public LightingManager(SmartHouseHub hub) { _hub = hub; }

        public void MenuIlluminazione()
        {
            bool attivo = true;
            while (attivo)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("╔══════════════════════════════════════════════════╗");
                Console.WriteLine("║                 HUB ILLUMINAZIONE                ║");
                Console.WriteLine("╚══════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine(" [1] Installa Nuovo Punto Luce (Lamp, Eco, Led...)");
                Console.WriteLine(" [2] Gestisci TUTTE le Luci");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine(" [X] Torna all'Hub Centrale");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.X: attivo = false; break;
                    case ConsoleKey.D1: InstallaPuntoLuce(); break;
                    case ConsoleKey.D2: GestisciTutteLeLuci(); break;
                }
            }
        }

        private void InstallaPuntoLuce()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--- NUOVO PUNTO LUCE ---");
            Console.ResetColor();
            Console.WriteLine("1 - Lampada Standard");
            Console.WriteLine("2 - EcoLamp (Risparmio Energetico)");
            Console.WriteLine("3 - Singolo LED");
            Console.WriteLine("4 - Matrice LED (MatrixLed)");

            var tipo = Console.ReadKey(true).Key;

            if (tipo == ConsoleKey.D4)
            {
                Console.Write("\nNumero di Righe: ");
                if (!int.TryParse(Console.ReadLine(), out int righe)) righe = 8;
                Console.Write("Numero di Colonne: ");
                if (!int.TryParse(Console.ReadLine(), out int col)) col = 8;

                MatrixLed nuovaMatrice = new MatrixLed();
                nuovaMatrice.GenerateMatrix(righe, col, new Led(new Power(2), new Name("Pixel"), new Brightness(100)));
                _hub.MatriciLed.Add(nuovaMatrice);
                MessaggioSuccesso($"Matrice LED {righe}x{col}");
            }
            else if (tipo == ConsoleKey.D1 || tipo == ConsoleKey.D2 || tipo == ConsoleKey.D3)
            {
                Console.Write("\nBrand/Nome (es. Salotto, Philips...): ");
                string brand = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(brand)) brand = "Generico";

                Console.Write("Watt: ");
                if (!int.TryParse(Console.ReadLine(), out int watt)) watt = 10;

                if (tipo == ConsoleKey.D1) { _hub.Lampade.Add(new Lamp(new Power(watt), new Name(brand), new Brightness(100))); MessaggioSuccesso("Lampada Standard"); }
                if (tipo == ConsoleKey.D2) { _hub.Lampade.Add(new EcoLamp(new Power(watt), new Name(brand), new Brightness(100))); MessaggioSuccesso("EcoLamp"); }
                if (tipo == ConsoleKey.D3) { _hub.SingoliLed.Add(new Led(new Power(watt), new Name(brand), new Brightness(100))); MessaggioSuccesso("Singolo LED"); }
            }
        }

        private void MessaggioSuccesso(string tipoLuce)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[+] {tipoLuce} installata/o con successo!");
            Console.ResetColor();
            Console.ReadKey();
        }

        private void GestisciTutteLeLuci()
        {
            var tutteLeLuci = _hub.TutteLeLuci;
            if (tutteLeLuci.Count == 0)
            {
                Console.WriteLine("\nNessuna luce installata. Aggiungine una prima!");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--- ELENCO LUCI DI CASA ---");
            Console.ResetColor();

            for (int i = 0; i < tutteLeLuci.Count; i++)
            {
                var luce = tutteLeLuci[i];
                string tipo = luce.GetType().Name;
                string nome = "Sconosciuto";
                bool isAccesa = false;

                // Estraiamo il nome e lo stato in base al tipo di luce
                if (luce is Lamp l)
                {
                    isAccesa = l.is_on;
                    nome = l.brand2.Value; // Le Lamp usano brand2
                }
                else if (luce is Led led)
                {
                    isAccesa = led.is_on;
                    // Uso dynamic per leggere .brand in modo sicuro senza errori di compilazione
                    try { nome = ((dynamic)led).brand.Value; } catch { nome = "Led"; }
                }
                else if (luce is MatrixLed ml)
                {
                    if (ml.matrix != null) foreach (var p in ml.matrix) if (p != null && p.is_on) isAccesa = true;
                    nome = $"Pannello {ml.matrix?.GetLength(0)}x{ml.matrix?.GetLength(1)}";
                }

                // Formattiamo la stringa in modo che sia allineata e bella da vedere
                string infoRiga = $"[{i + 1}] {tipo} \"{nome}\"";
                Console.Write($"{infoRiga,-35} -> ");

                if (isAccesa) { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("ACCESA"); }
                else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("SPENTA"); }
                Console.ResetColor();
            }

            Console.Write("\nScegli numero (0 per uscire): ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= tutteLeLuci.Count)
            {
                var luceScelta = tutteLeLuci[index - 1];

                // Passo il nome personalizzato anche al titolo del menù!
                string titoloMenu = "LUCE";
                try { titoloMenu = ((dynamic)luceScelta).brand2.Value; } catch { }
                try { titoloMenu = ((dynamic)luceScelta).brand.Value; } catch { }

                if (luceScelta is Lamp lampada) GestioneBase(lampada, $"LAMPADA ({titoloMenu})");
                else if (luceScelta is Led singoloLed) GestioneBase(singoloLed, $"LED ({titoloMenu})");
                else if (luceScelta is MatrixLed matrice) GestioneMatrice(matrice);
            }
        }

        private void GestioneBase(dynamic luce, string titolo)
        {
            bool attivo = true;
            while (attivo)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"╔═════ CONTROLLO: {titolo.ToUpper()} ═════╗");
                Console.ResetColor();
                Console.Write("Stato:      ");
                if (luce.is_on) { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("● ACCESA"); }
                else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("○ SPENTA"); }
                Console.ResetColor();
                Console.WriteLine($"Luminosità: {luce.brightness_Perc.Value}%");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine(" [A] Accendi  | [B] Spegni");
                Console.WriteLine(" [C] Regola Luminosità");
                Console.WriteLine(" [X] Indietro");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.A: luce.turnOn(); break;
                    case ConsoleKey.B: luce.turnOff(); break;
                    case ConsoleKey.C:
                        if (!luce.is_on) break;
                        Console.Write("\nNuova luminosità (1-100): ");
                        if (int.TryParse(Console.ReadLine(), out int lum) && lum >= 1 && lum <= 100)
                            luce.adjustBrightness(new Brightness(lum));
                        break;
                    case ConsoleKey.X: attivo = false; break;
                }
            }
        }

        private void GestioneMatrice(MatrixLed ml)
        {
            bool attivo = true;
            while (attivo)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"╔═════ CONTROLLO MATRICE LED ═════╗");
                Console.ResetColor();
                Console.WriteLine($"Dimensioni: {ml.matrix.GetLength(0)}x{ml.matrix.GetLength(1)}");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine(" [A] Accendi Tutta la Matrice");
                Console.WriteLine(" [B] Spegni Tutta la Matrice");
                Console.WriteLine(" [C] Imposta Intensità Globale");
                Console.WriteLine(" [D] Effetto Scacchiera (CheckerBoard)");
                Console.WriteLine(" [X] Indietro");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.A: ml.turnOn(); break;
                    case ConsoleKey.B: ml.turnOff(); break;
                    case ConsoleKey.C:
                        Console.Write("\nNuova luminosità globale (0-100): ");
                        if (int.TryParse(Console.ReadLine(), out int lum)) ml.SetIntensityAll(new Brightness(lum));
                        break;
                    case ConsoleKey.D: ml.PatternCheckerBoard(); break;
                    case ConsoleKey.X: attivo = false; break;
                }
            }
        }
    }
}