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
        public Guid DoorId { get; set; }
    }

    public class RemoveSecureDoorCommandHandler
    {
        private readonly ISecurityRepository _repository;

        public RemoveSecureDoorCommandHandler(ISecurityRepository repository)
        {
            _repository = repository;
        }

        public void Handle(RemoveSecureDoorCommand command)
        {
            _repository.Remove(command.DoorId);
        }
    }
}
}
