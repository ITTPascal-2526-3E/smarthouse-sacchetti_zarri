using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;

namespace BlaisePascal.SmartHouse.Domain.Lamps.LampsInterfaces
{
    public interface ILuminouse
    {
        public void adjustBrightness(Brightness new_bright_perc);
    }
}
