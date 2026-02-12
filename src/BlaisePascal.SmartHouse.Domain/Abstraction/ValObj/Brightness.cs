using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Abstraction.ValObj
{
    public class Brightness
    {
        public int Value { get; private set; }
        public Brightness(int value)
        {
            Value= Math.Clamp(value, 0, 100);
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
