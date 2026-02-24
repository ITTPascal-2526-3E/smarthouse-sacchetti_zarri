using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Security.Queries
{
    public class GetDoorCommandId
    {
        public Guid DoorId { get; set; }
    }
    public sealed record GetDoorQuery(Guid DoorId)
    {
        public static GetDoorQuery FromCommandId(GetDoorCommandId command)
        {
            return new GetDoorQuery(command.DoorId);
        }
    }
}
