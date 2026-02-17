using BlaisePascal.SmartHouse.Domain.AbstractInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;
using BlaisePascal.SmartHouse.Domain.Devices.Lamps.LampsInterfaces;

namespace BlaisePascal.SmartHouse.Domain.Devices.Lamps
{
    public abstract class LampModel : Device, ILuminouse
    {
        public Brightness max_brightness { get; protected set; } //brightness is in Lumen
        public Brightness brightness_Perc { get; protected set; } //bright perc
        public Power power { get; protected set; }//power is in Watt
        public bool is_on { get; protected set; }
        public Name brand { get; protected set; }
        public LampColor Color { get; protected set; }
        public abstract void adjustBrightness(Brightness new_bright_perc);

    }
}
