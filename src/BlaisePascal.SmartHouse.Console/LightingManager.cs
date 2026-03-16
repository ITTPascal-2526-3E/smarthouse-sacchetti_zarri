using System;
using System.Collections.Generic;
using BlaisePascal.SmartHouse.Domain.Devices.Lamps;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;
// Namespace Application Layer
using BlaisePascal.SmartHouse.Application.Devices.Lamps.Command;
using BlaisePascal.SmartHouse.Application.Devices.Lamps.Queries;

namespace BlaisePascal.SmartHouse.UI
{
    public class LightingManager
    {
        private SmartHouseHub _hub;

        // L'hub contiene l'istanza del tuo InMemoryLampRepository
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

                Console.Write("Luminosità Massima (es. 100): ");
                if (!int.TryParse(Console.ReadLine(), out int maxBrightness) || maxBrightness < 1) maxBrightness = 100;

                if (tipo == ConsoleKey.D1)
                {
                    // ===> MODIFICA 1: CREAZIONE TRAMITE APPLICATION LAYER <===
                    // Qui usiamo il comando AddLampCommand. Questo comando si occuperà di creare 
                    // l'entità di dominio e salvarla fisicamente nell'InMemoryLampRepository.
                    new AddLampCommand(_hub.LampRepository).Execute(brand, maxBrightness, watt);
                    MessaggioSuccesso("Lampada Standard (Salvata nel Repository)");
                }
                if (tipo == ConsoleKey.D2) { _hub.Lampade.Add(new EcoLamp(new Power(watt), new Name(brand), new Brightness(maxBrightness))); MessaggioSuccesso("EcoLamp"); }
                if (tipo == ConsoleKey.D3) { _hub.SingoliLed.Add(new Led(new Power(watt), new Name(brand), new Brightness(maxBrightness))); MessaggioSuccesso("Singolo LED"); }
            }
        }

        private void MessaggioSuccesso(string tipoLuce)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[+] {tipoLuce} installata/o con successo!");
            Console.ResetColor();
            Console.ReadKey();
        }

        private string EstraiNomeBrand(object luce)
        {
            if (luce is MatrixLed ml) return $"Pannello {ml.matrix?.GetLength(0)}x{ml.matrix?.GetLength(1)}";

            dynamic dynLuce = luce;
            try { return dynLuce.brand.Value.ToString(); } catch { }
            try { return dynLuce.brand.ToString(); } catch { }
            try { return dynLuce.brand2.Value.ToString(); } catch { }
            try { return dynLuce.brand2.ToString(); } catch { }
            try { return dynLuce.Brand.Value.ToString(); } catch { }
            try { return dynLuce.Brand.ToString(); } catch { }
            try { return dynLuce.Nome.Value.ToString(); } catch { }

            return "Generico";
        }

        private void GestisciTutteLeLuci()
        {
            var tutteLeLuci = new List<dynamic>();

            // ===> MODIFICA 2: LETTURA DAL REPOSITORY (QUERY) <===
            // Usiamo la Query GetAllLampsQuery per prendere le lampade standard.
            var lampadeStandard = new GetAllLampsQuery(_hub.LampRepository).Execute();
            if (lampadeStandard != null) tutteLeLuci.AddRange(lampadeStandard);

            tutteLeLuci.AddRange(_hub.Lampade);     // EcoLamp
            tutteLeLuci.AddRange(_hub.SingoliLed);  // Led singoli
            tutteLeLuci.AddRange(_hub.MatriciLed);  // Matrici

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
                string nome = EstraiNomeBrand(luce);
                bool isAccesa = false;

                if (luce is Lamp l) isAccesa = l.is_on;
                else if (luce is Led led) isAccesa = led.is_on;
                else if (luce is MatrixLed ml && ml.matrix != null)
                {
                    foreach (var p in ml.matrix) if (p != null && p.is_on) isAccesa = true;
                }

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
                string titoloMenu = EstraiNomeBrand(luceScelta);

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
                if (luce.is_on) { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("ACCESA"); }
                else { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("SPENTA"); }
                Console.ResetColor();

                int luminositaAttuale = 0;
                try { luminositaAttuale = luce.brightness_Perc.Value; } catch { }
                Console.WriteLine($"Luminosità: {luminositaAttuale}%");

                Console.WriteLine("-----------------------------------");
                Console.WriteLine(" [A] Accendi  | [B] Spegni");
                Console.WriteLine(" [C] Regola Luminosità");

                // Opzione visibile solo se è una Lampada Standard (per testare RemoveLampCommand)
                if (luce is Lamp) Console.WriteLine(" [D] Rimuovi Lampada dal Repository");

                Console.WriteLine(" [X] Indietro");

                var tastoPremuto = Console.ReadKey(true).Key;

                switch (tastoPremuto)
                {
                    case ConsoleKey.A:
                        if (luce is Lamp)
                        {
                            // ===> MODIFICA 3: CHIAMATA COMMAND ACCENSIONE <===
                            // Delega l'azione di accensione all'Application Layer passando il Repository
                            new SwitchLampOnCommand(_hub.LampRepository).Execute(luce.deviceId);
                        }
                        else luce.turnOn();
                        break;

                    case ConsoleKey.B:
                        if (luce is Lamp)
                        {
                            // ===> MODIFICA 4: CHIAMATA COMMAND SPEGNIMENTO <===
                            // Delega l'azione di spegnimento all'Application Layer
                            new SwitchLampOffCommand(_hub.LampRepository).Execute(luce.deviceId);
                        }
                        else luce.turnOff();
                        break;

                    case ConsoleKey.C:
                        if (!luce.is_on) break;
                        Console.Write("\nNuova luminosità (1-100): ");
                        if (int.TryParse(Console.ReadLine(), out int lum) && lum >= 1 && lum <= 100)
                        {
                            if (luce is Lamp)
                            {
                                // ===> MODIFICA 5: CHIAMATA COMMAND LUMINOSITÀ <===
                                // L'Application Layer recupererà la lampada e applicherà la nuova luminosità
                                new ChangeIntensityCommand(_hub.LampRepository).Execute(luce.deviceId, lum);
                            }
                            else luce.adjustBrightness(new Brightness(lum));
                        }
                        break;

                    case ConsoleKey.D:
                        if (luce is Lamp)
                        {
                            // ===> MODIFICA 6: CHIAMATA COMMAND ELIMINAZIONE <===
                            // Visto che hai il file RemoveLampCommand.cs, lo invochiamo qui!
                            new RemoveLampCommand(_hub.LampRepository).Execute(luce.deviceId);
                            attivo = false; // Usciamo dal menu perché la lampada non esiste più
                        }
                        break;

                    case ConsoleKey.X:
                        attivo = false;
                        break;
                }

                // ===> MODIFICA 7: RECUPERO STATO AGGIORNATO (QUERY) <===
                // Dopo aver eseguito un comando (A, B o C), ricarichiamo la lampada fresca dal Repository
                // Questo dimostra che il DB In-Memory è stato effettivamente modificato!
                if (luce is Lamp && tastoPremuto != ConsoleKey.X && tastoPremuto != ConsoleKey.D)
                {
                    luce = new GetLampQuery(_hub.LampRepository).Execute(luce.deviceId);
                }
            }
        }

        private void GestioneMatrice(MatrixLed ml)
        {
            // ... (il codice della matrice resta invariato in quanto non passa dal repository)
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