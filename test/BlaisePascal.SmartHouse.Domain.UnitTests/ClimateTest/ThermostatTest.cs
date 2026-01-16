using BlaisePascal.SmartHouse.Domain.Climate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.UnitTests.ClimateTest
{
	public class ThermostatTest
	{
		[Fact]
		public void Constructor_ValidValues_ShouldInitializeCorrectly()
		{
			Thermostat thermostat = new Thermostat(10.0);
			Assert.Equal(10.0, thermostat.current_temperature);
		}

		[Fact]
		public void SwitchTargetTemperature_SmallerTemperature_ShouldTurnOnAirConditioner()
		{
			Thermostat thermostat = new Thermostat(10.0);
			thermostat.turnOn();
            thermostat.SwitchTargetTemperature(5.0);
			Assert.Equal(5.0, thermostat.current_temperature);
		}
		
		[Fact]
		public void SwitchTargetTemperature_BiggerTemperature_ShouldTurnOnThermostat()
		{
			Thermostat thermostat = new Thermostat(10.0);
            thermostat.radiators[1] = new Radiator(0);
            thermostat.SwitchTargetTemperature(15.0);
            Assert.Equal(15.0, thermostat.current_temperature);

		}

        [Fact]
        public void SwitchTargetTemperature_NoThermostatAvaible_ShouldThrow()
        {
            Thermostat thermostat = new Thermostat(10.0);

			Assert.Throws<Exception>(() => thermostat.SwitchTargetTemperature(15.0));
        }
    }
}