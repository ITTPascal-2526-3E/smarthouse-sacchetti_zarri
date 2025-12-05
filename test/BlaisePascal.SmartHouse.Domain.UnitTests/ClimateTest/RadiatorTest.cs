using Xunit;
using BlaisePascal.SmartHouse.Domain.Climate;

namespace BlaisePascal.SmartHouse.Domain.UnitTests.ClimateTest
{
    public class RadiatorTest
    {
        private const double InitialTemperature = 20.0;

        [Fact]
        public void Constructor_ShouldSetInitialTemperature()
        {
            // Arrange
            // Act
            var radiator = new Radiator(InitialTemperature);

            // Assert
            Assert.Equal(InitialTemperature, radiator.temperature);
        }

        [Fact]
        public void SetTemperature_ShouldUpdateTemperature()
        {
            // Arrange
            var radiator = new Radiator(InitialTemperature);
            const double newTemperature = 25.5;

            // Act
            radiator.setTemperature(newTemperature);

            // Assert
            Assert.Equal(newTemperature, radiator.temperature);
        }

        [Fact]
        public void TurnOn_ShouldSetIsOnToTrue()
        {
            // Arrange
            var radiator = new Radiator(InitialTemperature);

            // Act
            radiator.turnOn();

            // Assert
            Assert.True(radiator.is_on);
        }

        [Fact]
        public void TurnOff_ShouldSetIsOffToTrue()
        {
            // Arrange
            var radiator = new Radiator(InitialTemperature);
            radiator.turnOn();

            // Act
            radiator.turnOff();

            // Assert
            Assert.True(radiator.is_off);
        }
    }
}