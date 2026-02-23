using BlaisePascal.SmartHouse.Domain.Devices.Shutters.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Shutters.Command
{
    public class RemoveShutterCommand
    {
        private readonly IShutterRepository _ShutterRepository;
        public RemoveShutterCommand(IShutterRepository ShutterRepository)
        {
            _ShutterRepository = ShutterRepository;
        }
        public void Execute(Guid id)
        {
            var shutter = _ShutterRepository.GetById(id);
            if (shutter != null)
            {
                _ShutterRepository.Remove(id);
            }
        }
    }
}
