using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Security.SecurityAbstraction.Repositories
{
    public interface ISecurityRepository
    {
        void Add(SecureDoor secureDoor);
        void Update(SecureDoor secureDoor);
        void Remove(Guid id);
        SecureDoor GetById(Guid id);
        List<SecureDoor> GetAll();
    }
}
