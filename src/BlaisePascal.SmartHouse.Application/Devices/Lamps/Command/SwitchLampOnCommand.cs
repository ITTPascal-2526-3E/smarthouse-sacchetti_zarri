using BlaisePascal.SmartHouse.Domain.Devices.Lamps.LampsInterfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Lamps.Command
{
    public class SwitchLampOnCommand
    {
        private ILampRepository _lampRepository;

        public SwitchLampOnCommand(ILampRepository lampRepository)
        {
            _lampRepository = lampRepository;
        }

        public void Execute(Guid id)
        {
            var lamp = _lampRepository.GetById(id);
            if (lamp != null)
            {
                lamp.turnOn();
                _lampRepository.Update(lamp);
            }
        }
    }
}
