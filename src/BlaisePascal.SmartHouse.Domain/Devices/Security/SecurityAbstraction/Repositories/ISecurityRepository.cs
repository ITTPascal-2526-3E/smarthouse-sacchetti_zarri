using BlaisePascal.SmartHouse.Domain.Devices.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Devices.Security.SecurityAbstraction.Repositories
{
    public interface ISecurityRepository
    {
        void Add(SecureDoor secureDoor);
        void Update(SecureDoor secureDoor);
        void Remove(Guid id);
        SecureDoor GetById(Guid id);
        List<SecureDoor> GetAllDoors();
       
        void Add(Webcam webcam);
        void Update(Webcam webcam);
        void RemoveWebcam(Guid id);
        Webcam GetWebcamById(Guid id);
        List<Webcam> GetAllWebcams();

    }
}
