using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Devices.Shutters.Repositories
{
    public interface IShutterRepository
    {
        void Add(Shutter shutter);
        void Update(Shutter shutter);
        void Remove(Guid id);
        Shutter GetById(Guid id);
        List<Shutter> GetAll();
    }
}
