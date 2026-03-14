using BlaisePascal.SmartHouse.Domain.Devices.Climate;
using BlaisePascal.SmartHouse.Domain.Devices.Lamps;
using BlaisePascal.SmartHouse.Domain.Devices.Lamps.LampsInterfaces.Repositories;
using BlaisePascal.SmartHouse.Domain.Devices.Security;
using BlaisePascal.SmartHouse.Domain.Devices.Shutters;
using BlaisePascal.SmartHouse.infrastructure.Repositories.Devices.Lamps;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlaisePascal.SmartHouse.UI
{
    public class SmartHouseHub
    {
        public ILampRepository LampRepository { get; } = new InMemoryLampRepository();

        // Liste Dispositivi
        public List<Lamp> Lampade { get; set; } = new List<Lamp>();
        public List<Led> SingoliLed { get; set; } = new List<Led>();
        public List<MatrixLed> MatriciLed { get; set; } = new List<MatrixLed>();

        // Climatizzazione
        public List<AirConditioner> Condizionatori { get; set; } = new List<AirConditioner>();
        public List<Radiator> Radiatori { get; set; } = new List<Radiator>();
        public List<Thermostat> Termostati { get; set; } = new List<Thermostat>();

        // Altri dispositivi
        public List<SecureDoor> Porte { get; set; } = new List<SecureDoor>();
        public List<Shutter> Tapparelle { get; set; } = new List<Shutter>();

        // Proprietà Comode
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
                lista.AddRange(Radiatori);
                lista.AddRange(Termostati);
                lista.AddRange(Porte);
                lista.AddRange(Tapparelle);
                return lista;
            }
        }

        // Metodo universale per estrarre in modo sicuro il nome o brand da QUALSIASI dispositivo
        public string EstraiNomeBrand(object dev)
        {
            if (dev is MatrixLed ml) return $"Pannello {ml.matrix?.GetLength(0)}x{ml.matrix?.GetLength(1)}";

            dynamic dynDev = dev;
            try { return dynDev.brand.Value.ToString(); } catch { }
            try { return dynDev.brand.ToString(); } catch { }
            return "Generico";
        }

        // Metodo universale per estrarre il GUID
        public string EstraiGuid(object dev)
        {
            dynamic dynDev = dev;
            try { return dynDev.deviceId.ToString(); } catch { }

            return "GUID-NON-TROVATO";
        }

        public void MostraListaDispositivi()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                       DASHBOARD GENERALE SMART HOUSE                                    ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            if (TuttiIDispositivi.Count == 0)
            {
                Console.WriteLine("\n Nessun dispositivo registrato nella casa. Aggiungi qualcosa dai vari Hub!");
                Console.WriteLine("\nPremi un tasto per tornare al menu...");
                Console.ReadKey();
                return;
            }

            // --- SEZIONE ILLUMINAZIONE ---
            var luci = TutteLeLuci;
            if (luci.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n>> ILLUMINAZIONE");
                Console.ResetColor();
                StampaIntestazioneTabella();
                foreach (var luce in luci) StampaRigaDispositivo(luce);
            }

            // --- SEZIONE CLIMATIZZAZIONE ---
            if (Condizionatori.Count > 0 || Radiatori.Count > 0 || Termostati.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n>> CLIMATIZZAZIONE");
                Console.ResetColor();
                StampaIntestazioneTabella();
                foreach (var clima in Condizionatori) StampaRigaDispositivo(clima);
                foreach (var rad in Radiatori) StampaRigaDispositivo(rad);
                foreach (var term in Termostati) StampaRigaDispositivo(term);
            }

            // --- SEZIONE SICUREZZA ---
            if (Porte.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n>> SICUREZZA (PORTE)");
                Console.ResetColor();
                StampaIntestazioneTabella();
                foreach (var porta in Porte) StampaRigaDispositivo(porta);
            }

            // --- SEZIONE TAPPARELLE ---
            if (Tapparelle.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n>> TAPPARELLE / OSCURANTI");
                Console.ResetColor();
                StampaIntestazioneTabella();
                foreach (var tapparella in Tapparelle) StampaRigaDispositivo(tapparella);
            }

            Console.WriteLine("\n=========================================================================================================");
            Console.WriteLine("Premi un tasto per tornare al menu...");
            Console.ReadKey();
        }

        private void StampaIntestazioneTabella()
        {
            Console.WriteLine($"{"ID (GUID)",-36} | {"TIPO",-12} | {"BRAND/NOME",-15} | {"STATO",-10} | {"DETTAGLI AGGIUNTIVI"}");
            Console.WriteLine(new string('-', 105));
        }

        private void StampaRigaDispositivo(object dev)
        {
            string tipo = dev.GetType().Name;
            string brand = EstraiNomeBrand(dev);
            string guid = EstraiGuid(dev);

            if (brand.Length > 15) brand = brand.Substring(0, 12) + "...";

            Console.Write($"{guid,-36} | {tipo,-12} | {brand,-15} | ");

            if (dev is Lamp lamp)
            {
                StampaStato(lamp.is_on, "ACCESA    ", "SPENTA    ", ConsoleColor.Green, ConsoleColor.DarkGray);
                Console.WriteLine($" | Lum: {lamp.brightness_Perc.Value}%");
            }
            else if (dev is Led led)
            {
                StampaStato(led.is_on, "ACCESA    ", "SPENTA    ", ConsoleColor.Green, ConsoleColor.DarkGray);
                Console.WriteLine($" | Lum: {led.brightness_Perc.Value}%");
            }
            else if (dev is MatrixLed ml)
            {
                bool isMatrixOn = false;
                if (ml.matrix != null) foreach (var p in ml.matrix) if (p != null && p.is_on) isMatrixOn = true;

                StampaStato(isMatrixOn, "ATTIVA    ", "SPENTA    ", ConsoleColor.Green, ConsoleColor.DarkGray);
                Console.WriteLine($" | Dimensioni: {ml.matrix?.GetLength(0)}x{ml.matrix?.GetLength(1)}");
            }
            else if (dev is AirConditioner ac)
            {
                StampaStato(ac.air_enabled, "ACCESO    ", "SPENTO    ", ConsoleColor.Cyan, ConsoleColor.DarkGray);
                Console.WriteLine($" | Target: {ac.target_temperature}°C");
            }
            else if (dev is Radiator rad)
            {
                StampaStato(rad.is_on, "ACCESO    ", "SPENTO    ", ConsoleColor.Red, ConsoleColor.DarkGray);
                Console.WriteLine($" | Temp. impostata: {rad.temperature}°C");
            }
            else if (dev is Thermostat term)
            {
                // Poiché nel termostato is_on è protected, assumiamo che se esiste è in "MONITORAGGIO"
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("ATTIVO    ");
                Console.ResetColor();
                Console.WriteLine($" | Temp. rilevata: {term.current_temperature}°C");
            }
            else if (dev is SecureDoor sd)
            {
                StampaStato(!sd.is_locked, "SBLOCCATA ", "BLOCCATA  ", ConsoleColor.Yellow, ConsoleColor.Red);
                Console.WriteLine($" | Allarme: {(sd.is_locked ? "Inserito" : "Disinserito")}");
            }
            else if (dev is Shutter sh)
            {
                StampaStato(sh.is_open, "APERTA    ", "CHIUSA    ", ConsoleColor.Green, ConsoleColor.DarkGray);
                Console.WriteLine($" | Stato serranda");
            }
            else
            {
                Console.WriteLine("SCONOSCIUTO| | Dati non disponibili");
            }
        }

        private void StampaStato(bool condizione, string vero, string falso, ConsoleColor colorVero, ConsoleColor colorFalso)
        {
            if (condizione)
            {
                Console.ForegroundColor = colorVero;
                Console.Write(vero);
            }
            else
            {
                Console.ForegroundColor = colorFalso;
                Console.Write(falso);
            }
            Console.ResetColor();
        }
    }
}