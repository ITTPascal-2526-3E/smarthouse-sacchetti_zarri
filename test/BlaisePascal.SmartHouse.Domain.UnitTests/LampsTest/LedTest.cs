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
            Led led = new Led(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("Philips"), new Abstraction.ValObj.Brightness(800));

            // Assert
            Assert.Equal(new Abstraction.ValObj.Power(10), led.power);
            Assert.Equal(new Abstraction.ValObj.Name("Philips"), led.brand);
            Assert.Equal(new Abstraction.ValObj.Brightness(800), led.max_brightness);
            Assert.False(led.is_on);
            Assert.Equal(new Abstraction.ValObj.Brightness(0), led.brightness_Perc);
            Assert.NotEqual(Guid.Empty, led.deviceId);
        }

        [Fact]
        public void TurnOn_ShouldSetIsOnAndBrightnessTo100()
        {
            // Arrange
            Led led = new Led(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("Xiaomi"), new Abstraction.ValObj.Brightness(750));

            // Act
            led.turnOn();

            // Assert
            Assert.True(led.is_on);
            Assert.Equal(new Abstraction.ValObj.Brightness(100), led.brightness_Perc);
        }

        [Fact]
        public void TurnOff_ShouldSetIsOnAndBrightnessTo0()
        {
            // Arrange
            Led led = new Led(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("Xiaomi"), new Abstraction.ValObj.Brightness(750));
            led.turnOn();

            // Act
            led.turnOff();

            // Assert
            Assert.False(led.is_on);
            Assert.Equal(new Abstraction.ValObj.Brightness(0), led.brightness_Perc);
        }

        [Fact]
        public void AdjustBrightness_ValidValue_ShouldChangeBrightness()
        {
            // Arrange
            Led led = new Led(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("Xiaomi"), new Abstraction.ValObj.Brightness(750));

            // Act
            led.adjustBrightness(new Abstraction.ValObj.Brightness(60));

            // Assert
            Assert.Equal(new Abstraction.ValObj.Brightness(60), led.brightness_Perc);
        }

        [Fact]
        public void AdjustBrightness_InvalidValue_ShouldThrow()
        {
            // Arrange
            Led led = new Led(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("LG"), new Abstraction.ValObj.Brightness(700));

            // Act & Assert
            Assert.Throws<ArgumentException>(() => led.adjustBrightness(new Abstraction.ValObj.Brightness(-1)));
            Assert.Throws<ArgumentException>(() => led.adjustBrightness(new Abstraction.ValObj.Brightness(101)));
        }
    }
}