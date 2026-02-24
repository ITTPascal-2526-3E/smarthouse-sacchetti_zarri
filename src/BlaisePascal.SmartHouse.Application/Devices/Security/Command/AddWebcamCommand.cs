using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;
using BlaisePascal.SmartHouse.Domain.Devices.Lamps.LampsInterfaces.Repositories;
using BlaisePascal.SmartHouse.Domain.Devices.Security;
using BlaisePascal.SmartHouse.Domain.Devices.Security.SecurityAbstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Security.Command
{
    public class AddWebcamCommandId
    {
        public Guid WebcamId { get; set; }
    }
    public class AddWebcamCommand
    {
        private readonly ISecurityRepository _repository;


        public AddWebcamCommand(ISecurityRepository repository)
        {
            _repository = repository;
        }

        public void execute()
        {
            var webcam = new Webcam(10);
            _repository.Add(webcam);
        }
    }
}
