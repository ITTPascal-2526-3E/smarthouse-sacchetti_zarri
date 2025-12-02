using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Climate
{
    public class AirConditioner : Device
    {
        public double lowest_temperature {get; } // temperature is in degrees celsius
        public int air_intensity { get; set; }
        public bool air_enabled { get; set; }
        public int last_air_intensity { get; private set; }
        public double target_temperature { get; private set;  }

        public AirConditioner(double Lowest_temperature, int Air_intensity)
        {
            if (Air_intensity > 0 && Air_intensity <= 5)
            {
                air_intensity = Air_intensity;
            }

            lowest_temperature = Lowest_temperature;
            last_air_intensity = air_intensity;
        }
        

        public void turnOn(){
            air_enabled = true;
            air_intensity = last_air_intensity;
            lastModifiedAtUtc = DateTime.Now;
        }

        public void turnOff()
        {
            air_enabled = false;
            last_air_intensity = air_intensity;
            lastModifiedAtUtc = DateTime.Now;
        }

        public void switchIntensity(int intensity)
        {
            air_intensity = intensity;
            lastModifiedAtUtc = DateTime.Now;
        }


        public void switchTemperature(double temperature)
        {
            if (temperature > lowest_temperature){
                target_temperature = temperature;
            }
            lastModifiedAtUtc = DateTime.Now;
        }


    }
}
