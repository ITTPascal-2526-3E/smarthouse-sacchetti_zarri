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
            ecoMode();
            brightness_Perc = 100;
            is_on = true;
        }

        public void turnOff()
        {
            brightness_Perc = 0;
            is_on = false;

        }

        public void adjustBrightness(int new_bright_perc)
        {
            if (int.IsPositive(new_bright_perc))
            {
                brightness_Perc = new_bright_perc;
            }

        }
            
        public void ecoMode()
        {
            DateTime dateTime = DateTime.Now;
            int ecoTimeH = dateTime.Hour;
            int ecoTimeM = dateTime.Minute;
            
            if(dateTime.Hour == ecoTimeH +1)
            {
                if(dateTime.Minute == ecoTimeM)
                {
                    brightness_Perc = 70;
                }
            }

            if (dateTime.Hour >= 22 || dateTime.Hour < 6)
            {
                brightness_Perc = 30;
            }

            


        }



    }
}
