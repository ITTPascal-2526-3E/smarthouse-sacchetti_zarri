using BlaisePascal.SmartHouse.Domain.Devices.Security.SecurityAbstraction.Repositories;
using System;

namespace BlaisePascal.SmartHouse.Domain.Devices.Security.Command
{

    public class LockDoorCommandHandler
    {
        private readonly ISecurityRepository _seucurityRepository;

        public LockDoorCommandHandler(ISecurityRepository seucurityRepository)
        {
            _seucurityRepository = seucurityRepository;
        }

        public void Execute(LockDoorCommand command)
        {
            var door = _seucurityRepository.GetById(command.DoorId);
            if (door != null)
            {
                door.Lock();
                _seucurityRepository.Update(door);
            }
        }
    }
}