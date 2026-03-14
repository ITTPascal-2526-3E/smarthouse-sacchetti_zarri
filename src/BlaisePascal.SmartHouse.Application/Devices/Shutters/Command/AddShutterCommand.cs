
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Devices.Shutters.Repositories;
using BlaisePascal.SmartHouse.Domain.Devices.Security.SecurityAbstraction.Repositories;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;

namespace BlaisePascal.SmartHouse.Application.Devices.Shutters.Command
{
    public class AddShutterCommand
    {
        private readonly IShutterRepository _ShutterRepository;

        public AddShutterCommand(IShutterRepository ShutterRepository)
        {
            _ShutterRepository = ShutterRepository;
        }

        public void Execute(Name brand)
        {
            var shutter = new Domain.Devices.Shutters.Shutter(brand);
            _ShutterRepository.Add(shutter);
        }
    }
}
