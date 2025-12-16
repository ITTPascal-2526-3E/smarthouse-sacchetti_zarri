using BlaisePascal.SmartHouse.Domain.Lamps;
using System;
using Xunit;

namespace BlaisePascal.SmartHouse.Domain.UnitTests.LampsTest
{
    public class LedTest
    {
        [Fact]
        public void Constructor_ValidValues_ShouldInitializeCorrectly()
        {
            // Arrange & Act
            Led led = new Led(10, "Philips", 800);

            // Assert
            Assert.Equal(10, led.power);
            Assert.Equal("Philips", led.brand);
            Assert.Equal(800, led.max_brightness);
            Assert.False(led.is_on);
            Assert.Equal(0, led.brightness_Perc);
            Assert.NotEqual(Guid.Empty, led.deviceId);
        }

        [Fact]
        public void TurnOn_ShouldSetIsOnAndBrightnessTo100()
        {
            // Arrange
            Led led = new Led(10, "Xiaomi", 750);

            // Act
            led.turnOn();
         
            // Assert
            Assert.True(led.is_on);
            Assert.Equal(100, led.brightness_Perc);
        }

        [Fact]
        public void TurnOff_ShouldSetIsOnAndBrightnessTo0()
        {
            // Arrange
            Led led = new Led(10, "Xiaomi", 750);
            led.turnOn();

            // Act
            led.turnOff();

            // Assert
            Assert.False(led.is_on);
            Assert.Equal(0, led.brightness_Perc);
        }

        [Fact]
        public void AdjustBrightness_ValidValue_ShouldChangeBrightness()
        {
            // Arrange
            Led led = new Led(10, "Xiaomi", 750);

            // Act
            led.adjustBrightness(60);

            // Assert
            Assert.Equal(60, led.brightness_Perc);
        }

        [Fact]
        public void AdjustBrightness_InvalidValue_ShouldThrow()
        {
            // Arrange
            Led led = new Led(10, "LG", 700);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => led.adjustBrightness(-1));
            Assert.Throws<ArgumentException>(() => led.adjustBrightness(101));
        }
    }
}

