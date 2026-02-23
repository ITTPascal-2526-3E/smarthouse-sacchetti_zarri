
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Devices.Shutters.Repositories;

namespace BlaisePascal.SmartHouse.Application.Devices.Shutters.Command
{
    public class AddShutterCommand
    {
        private readonly IShutterRepository _ShutterRepository;

        public AddShutterCommand(IShutterRepository ShutterRepository)
        {
            _ShutterRepository = ShutterRepository;
        }

        public void Execute()
        {
            var shutter = new Domain.Devices.Shutters.Shutter();
            _ShutterRepository.Add(shutter);
        }
    }
}
