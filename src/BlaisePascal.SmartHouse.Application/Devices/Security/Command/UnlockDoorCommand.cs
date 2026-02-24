using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;
using BlaisePascal.SmartHouse.Domain.Devices.Security;
using BlaisePascal.SmartHouse.Domain.Devices.Security.SecurityAbstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Security.Command
{
    public class UnlockDoorCommandId
    {
        public Guid DoorId { get; set; }
    }
    public class UnlockDoorCommand
    {
        private readonly ISecurityRepository _securityRepository;

        public UnlockDoorCommand(ISecurityRepository repository)
        {
            _securityRepository = repository;
        }

        public void execute()
        {
            var door = new SecureDoor(new Password("ciaociao"), new Email("mail"));
            door.Unlock(new Password("ciaociao"));
            _securityRepository.Update(door);
        }
    }
}
