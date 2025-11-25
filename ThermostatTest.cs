using BlaisePascal.SmartHouse.Domain.Thermostat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.UnitTests
{
	public class EcoLampTest
	{
		[Fact]
		public void Constructor_ValidValues_ShouldInitializeCorrectly()
		{
			Thermostat thermostat = new Thermostat(10.0);
			Assert.Equal(10.0, current_temperature);
		}

		[Fact]
		public void SwitchTargetTemperature_SmallerTemperature_ShouldTurnOnAirConditioner()
		{
			Thermostat thermostat = new Thermostat(10.0);
		}

		[Fact]
		public void SwitchTargetTemperature_BiggerTemperature_ShouldTurnOnThermostat()
		{
			Thermostat thermostat = new Thermostat(10.0);
			thermostat
		}
	}
}