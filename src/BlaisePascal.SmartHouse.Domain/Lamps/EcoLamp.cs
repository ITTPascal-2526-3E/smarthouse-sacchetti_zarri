using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public sealed class EcoLamp : Lamp
    {
        public DateTime? startTime;
        const int brightnessWhenEcoOn = 75;
        const int brightnessWhenNight = 30;

        public EcoLamp(double Power, string Brand, double Max_brightness): base(Power, Brand, Max_brightness)
        {
           
        }

        public override void turnOn()
        {
            base.turnOn();
            startTime = DateTime.Now;
        }

        public override void turnOff()
        {
            base.turnOff();
            startTime = null;
            
        }
        public void startEcoMode()
        {
            startTime = DateTime.Now;
            lastModifiedAtUtc = DateTime.Now;
        }

        public void ecoMode()
        {
            if (startTime == null)
                return;

            DateTime now = DateTime.Now;

            // Dopo un’ora dall’attivazione
            if ((now - startTime.Value).TotalHours >= 1)
            {
                brightness_Perc = brightnessWhenEcoOn;
            }

            // Di notte
            if (now.Hour >= 22 || now.Hour < 6)
            {
                brightness_Perc = brightnessWhenNight;
            }
            lastModifiedAtUtc = DateTime.Now;
        }


    }
}
