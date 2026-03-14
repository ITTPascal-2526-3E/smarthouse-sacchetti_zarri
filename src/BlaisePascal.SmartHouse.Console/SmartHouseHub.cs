using System;
using System.Collections.Generic;
using System.Linq;
using BlaisePascal.SmartHouse.Domain.Devices.Lamps;
using BlaisePascal.SmartHouse.Domain.Devices.Climate;
using BlaisePascal.SmartHouse.Domain.Devices.Security;
using BlaisePascal.SmartHouse.Domain.Devices.Shutters;

namespace BlaisePascal.SmartHouse.UI
{
    public class SmartHouseHub
    {
        // Tutte le liste di dispositivi
        public List<Lamp> Lampade { get; set; } = new List<Lamp>(); // Conterrà Lamp ed EcoLamp
        public List<Led> SingoliLed { get; set; } = new List<Led>();
        public List<MatrixLed> MatriciLed { get; set; } = new List<MatrixLed>();

        public List<AirConditioner> Condizionatori { get; set; } = new List<AirConditioner>();
        public List<SecureDoor> Porte { get; set; } = new List<SecureDoor>();
        public List<Shutter> Tapparelle { get; set; } = new List<Shutter>();

        // Proprietà comodissima per l'interfaccia utente delle luci
        public List<object> TutteLeLuci
        {
            get
            {
                var luci = new List<object>();
                luci.AddRange(Lampade);
                luci.AddRange(SingoliLed);
                luci.AddRange(MatriciLed);
                return luci;
            }
        }

        public List<object> TuttiIDispositivi
        {
            get
            {
                var lista = TutteLeLuci;
                lista.AddRange(Condizionatori);
                lista.AddRange(Porte);
                lista.AddRange(Tapparelle);
                return lista;
            }
        }

        public void MostraListaDispositivi()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                        PANORAMICA DISPOSITIVI DI CASA                        ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine($"{"TIPO",-15} | {"STATO E DETTAGLI"}");
            Console.WriteLine("--------------------------------------------------------------------------------");

            var lista = TuttiIDispositivi.OrderBy(d => d.GetType().Name).ToList();

            if (lista.Count == 0) Console.WriteLine(" Nessun dispositivo registrato nella casa.");
            else foreach (var dev in lista) StampaDettaglio(dev);

            Console.WriteLine("================================================================================");
            Console.WriteLine("Premi un tasto per tornare al menu...");
            Console.ReadKey();
        }

        private void StampaDettaglio(object dev)
        {
            Console.Write($"{dev.GetType().Name,-15} | ");

            if (dev is Lamp lamp) // Vale sia per Lamp che per EcoLamp
            {
                StampaStatoColorato(lamp.is_on, "ON ", "OFF");
                Console.WriteLine($" | Lum: {lamp.brightness_Perc.Value}%");
            }
            else if (dev is Led l)
            {
                StampaStatoColorato(l.is_on, "ON ", "OFF");
                Console.WriteLine($" | Lum: {l.brightness_Perc.Value}%");
            }
            else if (dev is MatrixLed ml)
            {
                bool isMatrixOn = false;
                if (ml.matrix != null) foreach (var led in ml.matrix) { if (led != null && led.is_on) isMatrixOn = true; }
                StampaStatoColorato(isMatrixOn, "ON ", "OFF");
                Console.WriteLine($" | Matrice {ml.matrix?.GetLength(0)}x{ml.matrix?.GetLength(1)}");
            }
            else if (dev is AirConditioner ac)
            {
                StampaStatoColorato(ac.air_enabled, "ON ", "OFF");
                Console.WriteLine($" | Target: {ac.target_temperature}°C");
            }
            else if (dev is SecureDoor sd)
            {
                StampaStatoColorato(!sd.is_locked, "SBLOCCATA", "BLOCCATA ");
                Console.WriteLine();
            }
            else if (dev is Shutter sh)
            {
                StampaStatoColorato(sh.is_open, "APERTA", "CHIUSA");
                Console.WriteLine();
            }
        }

        private void StampaStatoColorato(bool condizione, string vero, string falso)
        {
            if (condizione) { Console.ForegroundColor = ConsoleColor.Green; Console.Write(vero); }
            else { Console.ForegroundColor = ConsoleColor.Red; Console.Write(falso); }
            Console.ResetColor();
        }
    }
}