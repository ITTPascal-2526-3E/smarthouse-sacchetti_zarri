using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Climate;
namespace BlaisePascal.SmartHouse.Domain.UnitTests.ClimateTest
{
    public class AirConditionerTest
    {
        // Test per verificare l'accensione del condizionatore
        [Fact]
        public void TurnOn_ShouldEnableAirConditioner()
        {
            // Arrange
            var airConditioner = new AirConditioner(18.0, 3);
            // Act
            airConditioner.turnOn();
            // Assert
            Assert.True(airConditioner.air_enabled);
            Assert.Equal(3, airConditioner.air_intensity);
        }
        // Test per verificare lo spegnimento del condizionatore
        [Fact]
        public void TurnOff_ShouldDisableAirConditioner()
        {
            // Arrange
            var airConditioner = new AirConditioner(18.0, 3);
            airConditioner.turnOn();
            // Act
            airConditioner.turnOff();
            // Assert
            Assert.False(airConditioner.air_enabled);
            Assert.Equal(3, airConditioner.last_air_intensity);
        }
        // Test per verificare il cambio di intensità dell'aria
        [Fact]
        public void SwitchIntensity_ShouldChangeAirIntensity()
        {
            // Arrange
            var airConditioner = new AirConditioner(18.0, 3);
            // Act
            airConditioner.switchIntensity(5);
            // Assert
            Assert.Equal(5, airConditioner.air_intensity);
        }
        // Test per verificare il cambio di temperatura target
        [Fact]
        public void SwitchTemperature_ShouldChangeTargetTemperature()
        {
            // Arrange
            var airConditioner = new AirConditioner(18.0, 3);
            // Act
            airConditioner.switchTemperature(22.0);
            // Assert
            Assert.Equal(22.0, airConditioner.target_temperature);
        }
    }
}
