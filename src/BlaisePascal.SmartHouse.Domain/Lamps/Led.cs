using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public class Led : Device
    {
        public double max_brightness { get; protected set; } //brightness is in Lumen
        public int brightness_Perc { get; protected set; } //bright perc
        public double power { get; private set; }//power is in Watt
        public bool is_on { get; protected set; }
        public string brand { get; protected set; }
        public LampColor Color { get; protected set; }


        public Led(double Power, string Brand, double Max_brightness)
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

        public virtual void turnOn()
        {
        brightness_Perc = 100;
        is_on = true;
        }

        public virtual void turnOff()
        {
            brightness_Perc = 0;
            is_on = false;
        }

        public void adjustBrightness(int new_bright_perc)
        {
            if (int.IsPositive(new_bright_perc) && new_bright_perc <= 100)
            {
                brightness_Perc = new_bright_perc;
            }
            else
                throw new ArgumentException("Brightness percentage must be between 0 and 100.");
        }

    }
}
