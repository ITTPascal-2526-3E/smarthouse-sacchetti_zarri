using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Climate
{
    public class Radiator
    {
        public double temperature { get; set; } // temperature is in degrees celsius
        public Guid radiator_Id { get; set; } = Guid.NewGuid();
        public bool is_on { get; set; }
        public bool is_off { get; set; }

        public Radiator(double Temperature)
        {
            temperature = Temperature;
        }

        public void setTemperature(double newTemperature)
        {
            temperature = newTemperature;
        } 
        
        public void turnOn()
        {
            is_on=true;
        }
        public void turnOff()
        {
            is_off=true;    
        }   
    }
}
