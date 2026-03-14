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
    public class AuthenticateCommand
    {
        private readonly ISecurityRepository _securityRepository;


        public AuthenticateCommand(ISecurityRepository repository)
        {
            _securityRepository = repository;
        }

        public void Execute(Guid id)
        {
            var door = _securityRepository.GetById(id); 
            door.Authentication.Authenticate("Admin");
            _securityRepository.Update(door);
        }
    }
}
