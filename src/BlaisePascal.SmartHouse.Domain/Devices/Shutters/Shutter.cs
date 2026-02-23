using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Devices.Shutters
{
    public sealed class Shutter
    {
        public bool is_open { get; set; }
        public Guid shutter_Id { get; set; } = Guid.NewGuid();
        public ShuttersController autoShutters { get; }

        public Shutter()
        {
            autoShutters = new ShuttersController(this);
        }

        public void Open()
        {
            is_open = true;
        }
        public void Close()
        {

            is_open = false;
        }
    }
    
}
