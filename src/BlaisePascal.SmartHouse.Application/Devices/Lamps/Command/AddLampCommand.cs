using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Devices.Lamps.LampsInterfaces.Repositories;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;

namespace BlaisePascal.SmartHouse.Application.Devices.Lamps.Command
{
    public class AddLampCommand
    {
        private readonly ILampRepository _lampRepository;

        public AddLampCommand(ILampRepository lampRepository)
        {
            _lampRepository = lampRepository;
        }

        public void Execute(string name, int brightness, int power)
        {
            var lamp = new BlaisePascal.SmartHouse.Domain.Devices.Lamps.Lamp(new Power(power),new Name(name), new Brightness(brightness));
            _lampRepository.Add(lamp);
        }

    }
}
