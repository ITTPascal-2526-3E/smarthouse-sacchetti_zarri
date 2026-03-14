using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;
using BlaisePascal.SmartHouse.Domain.Devices.Security.SecurityAbstraction.Repositories;
using System;

namespace BlaisePascal.SmartHouse.Domain.Devices.Security.Command
{
    public class LockDoorCommand
    {
        private readonly ISecurityRepository _securityRepository;


        public LockDoorCommand(ISecurityRepository repository)
        {
            _securityRepository = repository;
        }

        public void Execute(Guid id)
        {
            var door = _securityRepository.GetById(id);
            door.Lock();
            _securityRepository.Update(door);
        }
    }
}