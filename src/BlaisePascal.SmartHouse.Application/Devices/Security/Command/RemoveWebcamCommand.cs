using BlaisePascal.SmartHouse.Domain.Devices.Security.SecurityAbstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Security.Command
{
    public class RemoveWebcamCommand
    {
        private readonly ISecurityRepository _securityRepository;

        public RemoveWebcamCommand(ISecurityRepository repository)
        {
            _securityRepository = repository;
        }


        public void Execute(Guid id)
        {
            var webcam = _securityRepository.GetById(id);
            _securityRepository.Remove(webcam.deviceId);
        }
    }
}
