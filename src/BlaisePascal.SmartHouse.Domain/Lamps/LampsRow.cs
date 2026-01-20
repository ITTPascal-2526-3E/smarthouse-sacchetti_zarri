using BlaisePascal.SmartHouse.Domain.AbstractInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public sealed class LampsRow : Device
    {
        public List<Lamp> lamps = new List<Lamp>();
        public void addLamp(double power3,string brand3,double max_brightness3)
        {
            lamps.Add(new Lamp(power3,brand3,max_brightness3));
        }

        public override void turnOn()
        {
            foreach(var lamp in lamps)
            {
                lamp.turnOn();
            }
            lastModifiedAtUtc = DateTime.Now;
        }

        public override void turnOff()
        {
            
            foreach (var lamp in lamps)
            {
               lamp.turnOff();
            }
            lastModifiedAtUtc = DateTime.Now;
        }
    }
}
