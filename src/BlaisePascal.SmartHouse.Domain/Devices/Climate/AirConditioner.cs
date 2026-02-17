using BlaisePascal.SmartHouse.Domain.AbstractInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;

namespace BlaisePascal.SmartHouse.Domain.Devices.Climate
{
    public sealed class AirConditioner : Device
    {
        public double lowest_temperature { get; protected set; } // temperature is in degrees celsius
        public int air_intensity { get; protected set; }
        public bool air_enabled { get; protected  set; }
        public int last_air_intensity { get; private set; }
        public double target_temperature { get; private set;  }

        public AirConditioner(double Lowest_temperature, Air Air_intensity)
        {
            lowest_temperature = Lowest_temperature;
            last_air_intensity = air_intensity;
        }
        

        public override void turnOn(){
            air_enabled = true;
            air_intensity = last_air_intensity;
            lastModifiedAtUtc = DateTime.Now;
        }

        public override void turnOff()
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
