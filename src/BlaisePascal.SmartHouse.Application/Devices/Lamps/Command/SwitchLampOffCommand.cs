using BlaisePascal.SmartHouse.Domain.Devices.Lamps.LampsInterfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;
using BlaisePascal.SmartHouse.Domain.Devices.Lamps;

namespace BlaisePascal.SmartHouse.Application.Devices.Lamps.Command
{
    public class SwitchLampOffCommand
    {
        private ILampRepository _lampRepository;

        public SwitchLampOffCommand(ILampRepository lampRepository)
        {
            _lampRepository = lampRepository;
        }

        public void Execute(Guid id)
        {
            var lamp = _lampRepository.GetById(id);
            if (lamp != null)
            {
                lamp.turnOff();
                _lampRepository.Update(lamp);
            }
        }
    }
}
