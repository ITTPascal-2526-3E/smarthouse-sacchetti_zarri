using BlaisePascal.SmartHouse.Domain.AbstractInterfaces;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Devices.Climate
{
    public class Thermostat : Device
    {
        public double current_temperature { get; private set; }
        public double target_temperature { get; protected set; }
        protected bool is_on = false;
        public AirConditioner air_conditioner { get; protected set; } = new AirConditioner(20.0, new Air(3));
        public Radiator[] radiators { get; protected set; } = new Radiator[5];
        
        public Thermostat(double Current_temperature) { 
            current_temperature = Current_temperature;
        }


        public override void turnOn()
        {
            is_on = true;
            lastModifiedAtUtc = DateTime.Now;
        }
        public override void turnOff()
        {
            is_on = false;
            lastModifiedAtUtc = DateTime.Now;
        }

        public void SwitchTargetTemperature(double Target_temperature)
        { 
            if (Target_temperature < current_temperature && is_on==true)
            {
                air_conditioner.switchTemperature(Target_temperature);
                air_conditioner.turnOn();
                current_temperature = Target_temperature;
                air_conditioner.turnOff();
                lastModifiedAtUtc = DateTime.Now;
            }
            else
            {
                if (radiators.All(r => r == null))
                {
                    throw new Exception();
                }
                else
                {
                    foreach (var rad in radiators)
                    {

                        if (rad != null)
                        {
                            rad.setTemperature(Target_temperature);
                            rad.turnOn();
                            current_temperature = Target_temperature;
                            rad.turnOff();
                        }

                    }
                    lastModifiedAtUtc = DateTime.Now;
                }
            }

        }


    }
}
