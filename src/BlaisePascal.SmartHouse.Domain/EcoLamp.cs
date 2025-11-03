using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain
{
    public class EcoLamp
    {
        public double max_brightness { get; set; } //brightness is in Lumen
        public int brightness_Perc { get; set; } //bright perc
        public double power; //power is in Watt
        public bool is_on { get; set; }
        public string brand { get; }
        public Guid lamp_Id { get; set; } //lamp idenficator code (il lamp id verra gestito da una classe esterna AssegnaLampId che controllera la univocità degli lamp_id della casa)
        public DateTime? startTime;

        public EcoLamp(double Power, string Brand, double Max_brightness)
        {
            if (double.IsPositive(Power))
            {
                power = Power;
            }

            if (!string.IsNullOrEmpty(Brand))
            {
                brand = Brand;
            }

            if (double.IsPositive(Max_brightness))
            {
                max_brightness = Max_brightness;
            }

            brightness_Perc = 0;
            is_on = false;



        }
        public void turnOn()
        {
            startEcoMode();
            brightness_Perc = 100;
            is_on = true;
        }

        public void turnOff()
        {
            brightness_Perc = 0;
            is_on = false;
            startTime = null;

        }

        public void adjustBrightness(int new_bright_perc)
        {
            if (int.IsPositive(new_bright_perc))
            {
                brightness_Perc = new_bright_perc;
            }

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
                brightness_Perc = 70;
            }

            // Di notte
            if (now.Hour >= 22 || now.Hour < 6)
            {
                brightness_Perc = 30;
            }
        }



    }
}
