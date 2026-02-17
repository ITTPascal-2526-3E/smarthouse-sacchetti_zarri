using BlaisePascal.SmartHouse.Domain.Devices.Lamps;
using System.Security.Cryptography.X509Certificates;

namespace BlaisePascal.SmartHouse.Domain.UnitTests.LampsTest
{
    public class EcoLampTest
    {
        [Fact]
        public void Constructor_ValidValues_ShouldInitializeCorrectly()
        {
            EcoLamp lamp = new EcoLamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("Philips"), new Abstraction.ValObj.Brightness(800));

            Assert.Equal(new Abstraction.ValObj.Power(10), lamp.power);
            Assert.Equal(new Abstraction.ValObj.Name("Philips"), lamp.brand);
            Assert.Equal(new Abstraction.ValObj.Brightness(800), lamp.max_brightness);
            Assert.False(lamp.is_on);
            Assert.Equal(new Abstraction.ValObj.Brightness(0), lamp.brightness_Perc);
            Assert.NotEqual(Guid.Empty, lamp.deviceId);
        }

        [Fact]
        public void TurnOn_ShouldSetIsOnAndBrightness100()
        {
            EcoLamp lamp = new EcoLamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("Ikea"), new Abstraction.ValObj.Brightness(600));

            lamp.turnOn();

            Assert.True(lamp.is_on);
            Assert.Equal(new Abstraction.ValObj.Brightness(100), lamp.brightness_Perc);
            Assert.NotNull(lamp.startTime);
        }

        [Fact]
        public void TurnOff_ShouldSetIsOffAndBrightness0()
        {
            EcoLamp lamp = new EcoLamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("Ikea"), new Abstraction.ValObj.Brightness(600));

            lamp.turnOff();

            Assert.False(lamp.is_on);
            Assert.Equal(new Abstraction.ValObj.Brightness(0), lamp.brightness_Perc);
            Assert.Null(lamp.startTime);
        }

        [Fact]
        public void AdjustBrightness_PositiveValue_ShouldUpdateBrightness()
        {
            EcoLamp lamp = new EcoLamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("Xiaomi"), new Abstraction.ValObj.Brightness(750));

            lamp.adjustBrightness(new Abstraction.ValObj.Brightness(65));

            Assert.Equal(new Abstraction.ValObj.Brightness(65), lamp.brightness_Perc);
        }


        [Fact]
        public void Properties_IdShouldBeDifferent()
        {
            EcoLamp lamp1 = new EcoLamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("A"), new Abstraction.ValObj.Brightness(600));
            EcoLamp lamp2 = new EcoLamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("A"), new Abstraction.ValObj.Brightness(500));

            Assert.Equal(new Abstraction.ValObj.Name("A"), lamp1.brand);
            Assert.Equal(new Abstraction.ValObj.Name("A"), lamp2.brand);

            Assert.NotEqual(lamp1.deviceId, lamp2.deviceId);

        }

        [Fact]
        public void adjustBrightness_intNegative_exception()
        {
            EcoLamp lamp1 = new EcoLamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("A"), new Abstraction.ValObj.Brightness(500));
            Assert.Throws<ArgumentException>(() => lamp1.adjustBrightness(new Abstraction.ValObj.Brightness(-20)));

        }

        [Fact]
        public void adjustBrightness_brightnessIsGratherThan100_exception()
        {
            EcoLamp lamp1 = new EcoLamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("A"), new Abstraction.ValObj.Brightness(500));

            Assert.Throws<ArgumentException>(() => lamp1.adjustBrightness(new Abstraction.ValObj.Brightness(-20))); 
        }
    }
}