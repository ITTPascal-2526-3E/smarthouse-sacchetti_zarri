using BlaisePascal.SmartHouse.Domain.Lamps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.UnitTests
{
    public  class LampTest
    {
        [Fact]
        public void Constructor_ValidValues_ShouldInitializeCorrectly()
        {
            Lamp lamp = new Lamp(10, "Philips", 800);

            Assert.Equal(10, lamp.power);
            Assert.Equal("Philips", lamp.brand);
            Assert.Equal(800, lamp.max_brightness);
            Assert.False(lamp.is_on);
            Assert.Equal(0, lamp.brightness_Perc);
            Assert.NotEqual(Guid.Empty, lamp.lamp_Id);
        }


        [Fact]
        public void turnOn_ShouldTurnOn()
        {
            Lamp lamp = new Lamp(10, "Xiaomi", 750);

            lamp.turnOn();

            Assert.Equal(100, lamp.brightness_Perc);
            Assert.Equal(true, lamp.is_on);
        }

        [Fact]
        public void turnOff_ShouldTurnOff()
        {
            Lamp lamp = new Lamp(10, "Xiaomi", 750);

            lamp.turnOn();
            lamp.turnOff();

            Assert.Equal(0, lamp.brightness_Perc);
            Assert.Equal(false, lamp.is_on);
        }


        [Fact]
        public void AdjustBrightness_PositiveValue_ShouldUpdateBrightness()
        {
            Lamp lamp = new Lamp(10, "Xiaomi", 750);

            lamp.adjustBrightness(65);

            Assert.Equal(65, lamp.brightness_Perc);
        }

        [Fact]
        public void AdjustBrightness_NonPositiveValue_ShouldNotChangeBrightness()
        {
            Lamp lamp = new Lamp(10, "Xiaomi", 750);

            Assert.Throws<ArgumentException>(() => lamp.adjustBrightness(-10));


        }

        [Fact]
        public void Properties_IdShouldBeDifferent()
        {
            Lamp lamp1 = new Lamp(10, "A", 500);
            Lamp lamp2 = new Lamp(10, "A", 500);

            Assert.Equal("A", lamp1.brand);
            Assert.Equal("A", lamp2.brand);

            Assert.NotEqual(lamp1.lamp_Id, lamp2.lamp_Id);

        }

        [Fact]
        public void Properties_isOn_colorShoudBeChanged()
        {
            Lamp lamp1 = new Lamp(10, "A", 500);
            lamp1.turnOn();
            lamp1.ChangeColor(LampColor.Blue);

            Assert.Equal(LampColor.Blue, lamp1.Color);

        }

        [Fact]
        public void Properties_isOff_colorShoudNotBeChanged()
        {
            Lamp lamp1 = new Lamp(10, "A", 500);
            lamp1.ChangeColor(LampColor.Blue);

            Assert.Equal(LampColor.White, lamp1.Color);

        }
    }
}
