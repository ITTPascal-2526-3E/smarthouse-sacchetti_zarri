using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Shutters
{
    public sealed class Shutters
    {
        public bool is_open { get; set; } 
        public bool is_closed { get; set; }
        public Guid shutter_Id { get; set; } = Guid.NewGuid();
        public ShuttersController autoShutters { get; }

        public Shutters()
        {
            autoShutters = new ShuttersController(this); 
        }
    }
    
}
