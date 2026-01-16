using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public abstract class LampModel : Device, ILamp
    {
        public double max_brightness { get; protected set; } //brightness is in Lumen
        public int brightness_Perc { get; protected set; } //bright perc
        public double power { get; protected set; }//power is in Watt
        public bool is_on { get; protected set; }
        public string brand { get; protected set; }
        public LampColor Color { get; protected set; }
        public void adjustBrightness(int new_bright_perc){}

    }
}
