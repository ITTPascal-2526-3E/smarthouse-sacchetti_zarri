using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Lamps;
using Xunit;

namespace BlaisePascal.SmartHouse.Domain.UnitTests.LampsTest
{
    public class LampsRowTest
    {
        [Fact]
        public void AddLamp_ShouldIncreaseLampCount()
        {
            LampsRow row = new LampsRow();

            row.addLamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("Philips"), new Abstraction.ValObj.Brightness(800));
            row.addLamp(new Abstraction.ValObj.Power(12), new Abstraction.ValObj.Name("Xiaomi"), new Abstraction.ValObj.Brightness(700));

            Assert.Equal(2, row.lamps.Count);
            Assert.Equal(new Abstraction.ValObj.Name("Philips"), row.lamps[0].brand);
            Assert.Equal(new Abstraction.ValObj.Name("Xiaomi"), row.lamps[1].brand);
        }

        [Fact]
        public void SwitchRowOn_AllLampsShouldBeOn()
        {
            LampsRow row = new LampsRow();

            row.addLamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("Philips"), new Abstraction.ValObj.Brightness(800));
            row.addLamp(new Abstraction.ValObj.Power(12), new Abstraction.ValObj.Name("Xiaomi"), new Abstraction.ValObj.Brightness(700));

            row.turnOn();

            Assert.All(row.lamps, lamp => Assert.True(lamp.is_on));
            Assert.All(row.lamps, lamp => Assert.Equal(new Abstraction.ValObj.Brightness(100), lamp.brightness_Perc));
        }

        [Fact]
        public void SwitchRowOff_AllLampsShouldBeOff()
        {
            LampsRow row = new LampsRow();

            row.addLamp(new Abstraction.ValObj.Power(10), new Abstraction.ValObj.Name("Philips"), new Abstraction.ValObj.Brightness(800));
            row.addLamp(new Abstraction.ValObj.Power(12), new Abstraction.ValObj.Name("Xiaomi"), new Abstraction.ValObj.Brightness(700));

            row.turnOn();
            row.turnOff();

            Assert.All(row.lamps, lamp => Assert.False(lamp.is_on));
            Assert.All(row.lamps, lamp => Assert.Equal(new Abstraction.ValObj.Brightness(0), lamp.brightness_Perc));
        }

        [Fact]
        public void SwitchRowOn_EmptyRow_ShouldNotThrow()
        {
            LampsRow row = new LampsRow();

            row.turnOn();

            Assert.Empty(row.lamps);
        }

        [Fact]
        public void SwitchRowOff_EmptyRow_ShouldNotThrow()
        {
            LampsRow row = new LampsRow();

            row.turnOff();

            Assert.Empty(row.lamps);
        }
    }
}