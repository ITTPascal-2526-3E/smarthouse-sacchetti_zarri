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
    public class UnlockDoorCommand
    {
        private readonly ISecurityRepository _securityRepository;

        public UnlockDoorCommand(ISecurityRepository repository)
        {
            _securityRepository = repository;
        }

        public void Execute(Guid id,Password pass)
        {
            var door = _securityRepository.GetById(id);
            door.Unlock(pass);
            _securityRepository.Update(door);
        }
    }
}
