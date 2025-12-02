using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Climate
{
    public class Radiator : Device 
    {
        public double temperature { get; set; } // temperature is in degrees celsius
        public bool is_on { get; set; }
        public bool is_off { get; set; }

        public Radiator(double Temperature)
        {
            temperature = Temperature;
        }

        public void setTemperature(double newTemperature)
        {
            temperature = newTemperature;
            lastModifiedAtUtc = DateTime.Now;
        } 
        
        public void turnOn()
        {
            is_on=true;
            lastModifiedAtUtc = DateTime.Now;
        }
        public void turnOff()
        {
            is_off=true;
            lastModifiedAtUtc = DateTime.Now;
        }   
    }
}
