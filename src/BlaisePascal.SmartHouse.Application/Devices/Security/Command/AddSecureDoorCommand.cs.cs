using BlaisePascal.SmartHouse.Domain.Devices.Security.SecurityAbstraction.Repositories;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;
using System;

namespace BlaisePascal.SmartHouse.Domain.Devices.Security.Command
{
    public class AddSecureDoorCommand
    {
        private readonly ISecurityRepository _securityRepository;


        public AddSecureDoorCommand(ISecurityRepository repository)
        {
            _securityRepository = repository;
        }

        public void Execute(Name brand, Password password,Email mail)
        {
            var door = new SecureDoor(brand,password,mail);
            _securityRepository.Add(door);
        }
    }
}

    