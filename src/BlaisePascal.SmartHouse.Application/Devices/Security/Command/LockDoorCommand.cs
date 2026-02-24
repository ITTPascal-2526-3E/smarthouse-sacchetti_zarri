using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;
using BlaisePascal.SmartHouse.Domain.Devices.Security.SecurityAbstraction.Repositories;
using System;

namespace BlaisePascal.SmartHouse.Domain.Devices.Security.Command
{

    public class LockDoorCommandId
    {
        public Guid DoorId { get; set; }
    }
    public class LockDoorCommand
    {
        private readonly ISecurityRepository _repository;


        public LockDoorCommand(ISecurityRepository repository)
        {
            _repository = repository;
        }

        public void Execute()
        {
            var door = new SecureDoor(new Password("ciaociao"), new Email("mail"));
            door.Lock();
            _repository.Update(door);
        }
    }
}