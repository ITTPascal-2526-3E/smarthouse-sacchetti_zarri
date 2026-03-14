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
    public class RemoveSecureDoorCommand
    {
        private readonly ISecurityRepository _securityRepository;

        public RemoveSecureDoorCommand(ISecurityRepository repository)
        {
            _securityRepository = repository;
        }


        public void Execute(Guid id)
        {
            var door = _securityRepository.GetById(id);
            _securityRepository.Remove(door.deviceId);
        }
    }
}
