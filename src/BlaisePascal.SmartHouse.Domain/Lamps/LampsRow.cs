using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public class LampsRow
    {
        List<Lamp> lamps;
        public void addLamp(double power,string brand,double max_brightness)
        {
            lamps.Add(new Lamp(power,brand,max_brightness));
        }

        public void swithcRowOn()
        {
            foreach(var lamp in lamps)
            {
                lamp.turnOn();
            }
        }

        public void swithcRowOff()
        {
            foreach (var lamp in lamps)
            {
                lamp.turnOff();
            }
        }
    }
}
