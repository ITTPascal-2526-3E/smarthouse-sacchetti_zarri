using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Devices.Lamps;
using BlaisePascal.SmartHouse.Domain.Devices.Lamps.LampsInterfaces.Repositories;

namespace BlaisePascal.SmartHouse.Application.Devices.Lamps.Queries
{
    public class GetLampQuery
    {
        private readonly ILampRepository _lampRepository;

        public GetLampQuery(ILampRepository lampRepository)
        {
            _lampRepository = lampRepository;
        }

        public Lamp Execute(Guid id)
        {
            return _lampRepository.GetById(id);
        }
    }
}
