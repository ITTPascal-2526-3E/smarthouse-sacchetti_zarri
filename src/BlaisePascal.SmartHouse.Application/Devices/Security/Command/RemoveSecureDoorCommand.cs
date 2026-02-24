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
    public class RemoveSecureDoorCommandId
    {
        public Guid WebcamId { get; set; }
    }
    public class RemoveSecureDoorCommand
    {
        private readonly ISecurityRepository _repository;

        public RemoveSecureDoorCommand(ISecurityRepository repository)
        {
            _repository = repository;
        }


        public void execute(RemoveWebcamCommandId command)
        {

            _repository.Remove(command.WebcamId);
        }
    }
}
