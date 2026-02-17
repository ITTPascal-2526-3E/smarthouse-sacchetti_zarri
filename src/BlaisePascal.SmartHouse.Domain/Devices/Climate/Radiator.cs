using BlaisePascal.SmartHouse.Domain.AbstractInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Devices.Climate
{
    public sealed class Radiator : Device 
    {
        public double temperature { get; protected set; } // temperature is in degrees celsius
        public bool is_on { get; protected set; }
        public bool is_off { get; protected set; }

        public Radiator(double Temperature)
        {
            temperature = Temperature;
        }

        public void setTemperature(double newTemperature)
        {
            temperature = newTemperature;
            lastModifiedAtUtc = DateTime.Now;
        } 
        
        public override void turnOn()
        {
            is_on=true;
            lastModifiedAtUtc = DateTime.Now;
        }

        public override void turnOff()
        {
            is_off=true;
            lastModifiedAtUtc = DateTime.Now;
        }   
    }
}
