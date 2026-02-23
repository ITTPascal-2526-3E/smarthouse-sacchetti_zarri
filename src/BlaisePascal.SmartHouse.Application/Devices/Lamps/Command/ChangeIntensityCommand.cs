using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;
using BlaisePascal.SmartHouse.Domain.Devices.Lamps.LampsInterfaces.Repositories;
using System;

namespace BlaisePascal.SmartHouse.Application.Devices.Lamps.Command
{
    public class ChangeIntensityCommand
    {
        private readonly ILampRepository _lampRepository;

        public ChangeIntensityCommand(ILampRepository lampRepository)
        {
            _lampRepository = lampRepository;
        }

        public void Execute(Guid lampId, int newBrightness)
        {
            var lamp = _lampRepository.GetById(lampId);

            if (lamp != null)
            {
                lamp.adjustBrightness(new Brightness(newBrightness));
                _lampRepository.Update(lamp);
            }
        }
    }
}