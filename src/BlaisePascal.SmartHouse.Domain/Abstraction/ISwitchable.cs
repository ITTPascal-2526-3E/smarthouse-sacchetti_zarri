using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.AbstractInterfaces
{
    public interface ISwitchable
    {
        public void turnOn();
        public void turnOff();       
    }
}
