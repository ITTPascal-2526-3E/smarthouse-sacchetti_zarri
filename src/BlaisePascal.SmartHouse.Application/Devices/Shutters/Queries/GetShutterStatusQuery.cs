using BlaisePascal.SmartHouse.Domain.Devices.Shutters;
using BlaisePascal.SmartHouse.Domain.Devices.Shutters.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Application.Devices.Shutters.Queries
{
    public class GetShutterStatusQuery
    {
        private readonly IShutterRepository _ShutterRepository;
        public GetShutterStatusQuery(IShutterRepository ShutterRepository)
        {
            _ShutterRepository = ShutterRepository;
        }
        public Shutter Execute(Guid id)
        {
            return  _ShutterRepository.GetById(id);
        }
    }
}
