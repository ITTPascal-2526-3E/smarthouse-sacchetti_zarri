using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Abstraction.ValObj
{
    public class Power
    {
        public double Value { get; private set; }
        public Power(double value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Power must be a positive number.");
            Value = value;
        }
    }
}
