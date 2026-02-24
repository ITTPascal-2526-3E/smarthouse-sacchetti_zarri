using BlaisePascal.SmartHouse.Domain.Devices.Security.SecurityAbstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Security.Command
{
    public class RemoveWebcamCommandId
    {
        public Guid WebcamId { get; set; }
    }
    public class RemoveWebcamCommand
    {
        private readonly ISecurityRepository _repository;

        public RemoveWebcamCommand(ISecurityRepository repository)
        {
            _repository = repository;
        }


        public void execute(RemoveWebcamCommandId command)
        {

            _repository.Remove(command.WebcamId);
        }
    }
}
