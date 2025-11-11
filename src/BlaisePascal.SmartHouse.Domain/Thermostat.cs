using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain
{
    public class Thermostat
    {
        public Guid thermostat_Id { get; set; } = new Guid();
        public double current_temperature { get; private set; }
        public double target_temperature { get; set; }
        public AirConditioner air_conditioner { get; set; } = new AirConditioner(20.0, 3);
        
        public Thermostat(double Current_temperature) { 
        current_temperature = Current_temperature;
        }

        public void SwitchTargetTemperature(double Target_temperature)
        {
            air_conditioner.switchTemperature(Target_temperature);
            current_temperature = Target_temperature;
        }


    }
}
