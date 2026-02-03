using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;

namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public sealed class Led : LampModel
    {
        public Led(Power Power, Name Brand, Brightness Max_brightness)
        {
            brightness_Perc = new Brightness(0);
            is_on = false;
        }

        public override void turnOn()
        {
            brightness_Perc = new Brightness(100);
            is_on = true;
            lastModifiedAtUtc = DateTime.Now;
        }

        public override void turnOff()
        {
            brightness_Perc = new Brightness(0);
            is_on = false;
            lastModifiedAtUtc = DateTime.Now;
        }

        public override void adjustBrightness(Brightness new_bright_perc)
        {
            brightness_Perc = new_bright_perc;
            lastModifiedAtUtc = DateTime.Now;
        }

    }
}
