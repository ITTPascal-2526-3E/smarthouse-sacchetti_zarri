using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Shutters.Repositories
{
    public interface IShutterRepository
    {
        void Add(Shutters shutter);
        void Update(Shutters shutter);
        void Remove(Guid id);
        Shutters GetById(Guid id);
        List<Shutters> GetAll();
    }
}
