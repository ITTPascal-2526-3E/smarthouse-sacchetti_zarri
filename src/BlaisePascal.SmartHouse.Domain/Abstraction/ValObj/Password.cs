using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Abstraction.ValObj
{
    public class Password
    {
        public string Value { get; private set; }
        public Password(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Password must be at least 6 characters long.", nameof(value));
            Value = value;
        }
    }
}
