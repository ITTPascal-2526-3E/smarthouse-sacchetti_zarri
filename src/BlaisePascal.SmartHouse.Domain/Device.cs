using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain
{
    public class Device
    {
        public Guid deviceId { get; protected set; } = Guid.NewGuid();
        public DateTime cratedTime { get; protected set; } = DateTime.UtcNow;
        public DateTime lastModifiedAtUtc { get; protected set; }

    }
}
