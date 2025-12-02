using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public class EcoLamp : Lamp
    {
        public EcoLamp(double Power, string Brand, double Max_brightness): base(Power, Brand, Max_brightness)
        {
           
        }
        public void startEcoMode()
        {
            startTime = DateTime.Now;
        }

        public void ecoMode()
        {
            if (startTime == null)
                return;

            DateTime now = DateTime.Now;

            // Dopo un’ora dall’attivazione
            if ((now - startTime.Value).TotalHours >= 1)
            {
                brightness_Perc = 75;
            }

            // Di notte
            if (now.Hour >= 22 || now.Hour < 6)
            {
                brightness_Perc = 30;
            }
        }


    }
}
