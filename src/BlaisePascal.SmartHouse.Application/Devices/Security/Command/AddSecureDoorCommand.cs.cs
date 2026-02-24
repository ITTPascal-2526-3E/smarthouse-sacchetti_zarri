using BlaisePascal.SmartHouse.Domain.Devices.Security.SecurityAbstraction.Repositories;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;
using System;

namespace BlaisePascal.SmartHouse.Domain.Devices.Security.Command
{
    public class AddSecureDoorCommandId
    {
        public Guid DoorId { get; set; }
    }
    public class AddSecureDoorCommand
    {
        private readonly ISecurityRepository _repository;

        
        public AddSecureDoorCommand(ISecurityRepository repository)
        {
            _repository = repository;
        }

        public void execute()
        {
            var door = new SecureDoor(new Password("ciaociao"), new Email("mail"));
            _repository.Add(door);
        }
    }
}

    