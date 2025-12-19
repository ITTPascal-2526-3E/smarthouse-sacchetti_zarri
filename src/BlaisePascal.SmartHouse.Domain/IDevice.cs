using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain
{
    public interface IDevice
    {
        public void turnOn();
        public void turnOff();       
    }
}
