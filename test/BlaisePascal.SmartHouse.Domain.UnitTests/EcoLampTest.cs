using BlaisePascal.SmartHouse.Domain.Lamps;
using System.Security.Cryptography.X509Certificates;

namespace BlaisePascal.SmartHouse.Domain.UnitTests
{
    public class EcoLampTest
    {
        [Fact]
        public void Constructor_ValidValues_ShouldInitializeCorrectly()
        {
            EcoLamp lamp = new EcoLamp(10, "Philips", 800);

            Assert.Equal(10, lamp.power);
            Assert.Equal("Philips", lamp.brand);
            Assert.Equal(800, lamp.max_brightness);
            Assert.False(lamp.is_on);
            Assert.Equal(0, lamp.brightness_Perc);
            Assert.NotEqual(Guid.Empty, lamp.lamp_Id);
        }

        [Fact]
        public void TurnOn_ShouldSetIsOnAndBrightness100()
        {
            EcoLamp lamp = new EcoLamp(10, "Ikea", 600);

            lamp.turnOn();

            Assert.True(lamp.is_on);
            Assert.Equal(100, lamp.brightness_Perc);
            Assert.NotNull(lamp.startTime);
        }

        [Fact]
        public void TurnOff_ShouldSetIsOffAndBrightness0()
        {
            EcoLamp lamp = new EcoLamp(10, "Ikea", 600);

            lamp.turnOff();

            Assert.False(lamp.is_on);
            Assert.Equal(0, lamp.brightness_Perc);
            Assert.Null(lamp.startTime);
        }

        [Fact]
        public void AdjustBrightness_PositiveValue_ShouldUpdateBrightness()
        {
            EcoLamp lamp = new EcoLamp(10, "Xiaomi", 750);

            lamp.adjustBrightness(65);

            Assert.Equal(65, lamp.brightness_Perc);
        }


        [Fact]
        public void Properties_IdShouldBeDifferent()
        {
            EcoLamp lamp1 = new EcoLamp(10, "A", 500);
            EcoLamp lamp2 = new EcoLamp(10, "A", 500);

            Assert.Equal("A", lamp1.brand);
            Assert.Equal("A", lamp2.brand);

            Assert.NotEqual(lamp1.lamp_Id, lamp2.lamp_Id);

        }

        [Fact]
        public void adjustBrightness_intNegative_exception()
        {
            EcoLamp lamp1 = new EcoLamp(10, "A", 500);
            Assert.Throws<ArgumentException>(() => lamp1.adjustBrightness(-20));

        }

        [Fact]
        public void adjustBrightness_brightnessIsGratherThan100_exception()
        {
            EcoLamp lamp1 = new EcoLamp(10, "A", 500);

            Assert.Throws<ArgumentException>(() => lamp1.adjustBrightness(120));

        }
    }
}