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
            
            row.addLamp(10, "Philips", 800);
            row.addLamp(12, "Xiaomi", 700);

            Assert.Equal(2, row.lamps.Count);
            Assert.Equal("Philips", row.lamps[0].brand);
            Assert.Equal("Xiaomi", row.lamps[1].brand);
        }

        [Fact]
        public void SwitchRowOn_AllLampsShouldBeOn()
        {
            LampsRow row = new LampsRow();

            row.addLamp(10, "Philips", 800);
            row.addLamp(12, "Xiaomi", 700);

            row.swithcRowOn();

            Assert.All(row.lamps, lamp => Assert.True(lamp.is_on));
            Assert.All(row.lamps, lamp => Assert.Equal(100, lamp.brightness_Perc));
        }

        [Fact]
        public void SwitchRowOff_AllLampsShouldBeOff()
        {
            LampsRow row = new LampsRow();

            row.addLamp(10, "Philips", 800);
            row.addLamp(12, "Xiaomi", 700);

            row.swithcRowOn();
            row.swithcRowOff();

            Assert.All(row.lamps, lamp => Assert.False(lamp.is_on));
            Assert.All(row.lamps, lamp => Assert.Equal(0, lamp.brightness_Perc));
        }

        [Fact]
        public void SwitchRowOn_EmptyRow_ShouldNotThrow()
        {
            LampsRow row = new LampsRow();

            row.swithcRowOn();

            Assert.Empty(row.lamps);


        }

        [Fact]
        public void SwitchRowOff_EmptyRow_ShouldNotThrow()
        {
            LampsRow row = new LampsRow();

            row.swithcRowOff();

            Assert.Empty(row.lamps);
        }
    }
}

