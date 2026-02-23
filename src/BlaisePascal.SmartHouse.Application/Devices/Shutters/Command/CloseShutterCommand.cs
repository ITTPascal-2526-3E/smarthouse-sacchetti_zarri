using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Devices.Shutters.Repositories;

namespace BlaisePascal.SmartHouse.Application.Devices.Shutters.Command
{
    public class CloseShutterCommand
    {
        private readonly IShutterRepository _ShutterRepository;
        public CloseShutterCommand(IShutterRepository ShutterRepository)
        {
            _ShutterRepository = ShutterRepository;
        }
        public void Execute(Guid id)
        {
            var shutter = _ShutterRepository.GetById(id);
            if (shutter != null)
            {
              shutter.Close();
            }
            _ShutterRepository.Update(shutter);
        }
    }
}
