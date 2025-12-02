using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public class LampsRow
    {
        public List<Lamp> lamps = new List<Lamp>();
        public void addLamp(double power3,string brand3,double max_brightness3)
        {
            lamps.Add(new Lamp(power3,brand3,max_brightness3));
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
