using BlaisePascal.SmartHouse.Domain.Devices.Lamps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.UnitTests.LampsTest
{
    public class LampTest
    {
        [Fact]
        public void Constructor_ValidValues_ShouldInitializeCorrectly()
        {
            Lamp lamp = new Lamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("Philips"), new Abstraction.ValObj.Brightness(800));

            Assert.Equal(new Abstraction.ValObj.Power(10), lamp.power);
            Assert.Equal(new Abstraction.ValObj.Name("Philips"), lamp.brand);
            Assert.Equal(new Abstraction.ValObj.Brightness(800), lamp.max_brightness);
            Assert.False(lamp.is_on);
            Assert.Equal(new Abstraction.ValObj.Brightness(0), lamp.brightness_Perc);
            Assert.NotEqual(Guid.Empty, lamp.deviceId);
        }


        [Fact]
        public void turnOn_ShouldTurnOn()
        {
            Lamp lamp = new Lamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("Xiaomi"), new Abstraction.ValObj.Brightness(750));

            lamp.turnOn();

            Assert.Equal(new Abstraction.ValObj.Brightness(100), lamp.brightness_Perc);
            Assert.Equal(true, lamp.is_on);
        }

        [Fact]
        public void turnOff_ShouldTurnOff()
        {
            Lamp lamp = new Lamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("Xiaomi"), new Abstraction.ValObj.Brightness(750));

            lamp.turnOn();
            lamp.turnOff();

            Assert.Equal(new Abstraction.ValObj.Brightness(0), lamp.brightness_Perc);
            Assert.Equal(false, lamp.is_on);
        }


        [Fact]
        public void AdjustBrightness_PositiveValue_ShouldUpdateBrightness()
        {
            Lamp lamp = new Lamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("Xiaomi"), new Abstraction.ValObj.Brightness(750));

            lamp.adjustBrightness(new Abstraction.ValObj.Brightness(65));

            Assert.Equal(new Abstraction.ValObj.Brightness(65), lamp.brightness_Perc);
        }

        [Fact]
        public void AdjustBrightness_NonPositiveValue_ShouldNotChangeBrightness()
        {
            Lamp lamp = new Lamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("Xiaomi"), new Abstraction.ValObj.Brightness(750));

            // Assumendo che il ValueObject o il metodo lancino l'eccezione
            Assert.Throws<ArgumentException>(() => lamp.adjustBrightness(new Abstraction.ValObj.Brightness(-10)));
        }

        [Fact]
        public void Properties_IdShouldBeDifferent()
        {
            Lamp lamp1 = new Lamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("A"), new Abstraction.ValObj.Brightness(500));
            Lamp lamp2 = new Lamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("A"), new Abstraction.ValObj.Brightness(500));

            Assert.Equal(new Abstraction.ValObj.Name("A"), lamp1.brand);
            Assert.Equal(new Abstraction.ValObj.Name("A"), lamp2.brand);

            Assert.NotEqual(lamp1.deviceId, lamp2.deviceId);
        }

        [Fact]
        public void Properties_isOn_colorShoudBeChanged()
        {
            Lamp lamp1 = new Lamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("A"), new Abstraction.ValObj.Brightness(500));
            lamp1.turnOn();
            lamp1.ChangeColor(LampColor.Blue);

            Assert.Equal(LampColor.Blue, lamp1.Color);
        }

        [Fact]
        public void Properties_isOff_colorShoudNotBeChanged()
        {
            Lamp lamp1 = new Lamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("A"), new Abstraction.ValObj.Brightness(500));
            lamp1.ChangeColor(LampColor.Blue);

            Assert.Equal(LampColor.White, lamp1.Color);
        }
    }
}