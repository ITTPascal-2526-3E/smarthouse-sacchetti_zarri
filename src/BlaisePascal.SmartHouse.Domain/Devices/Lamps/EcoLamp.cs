using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;

namespace BlaisePascal.SmartHouse.Domain.Devices.Lamps
{
    public sealed class EcoLamp : Lamp
    {
        public DateTime? startTime;
        const int brightnessWhenEcoOn = 75;
        const int brightnessWhenNight = 30;

        public EcoLamp(Power Power, Name Brand, Brightness Max_brightness): base(Power, Brand, Max_brightness)
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
                brightness_Perc = new Brightness(brightnessWhenEcoOn);
            }

            // Di notte
            if (now.Hour >= 22 || now.Hour < 6)
            {
                brightness_Perc = new Brightness(brightnessWhenNight);
            }
            lastModifiedAtUtc = DateTime.Now;
        }


    }
}
