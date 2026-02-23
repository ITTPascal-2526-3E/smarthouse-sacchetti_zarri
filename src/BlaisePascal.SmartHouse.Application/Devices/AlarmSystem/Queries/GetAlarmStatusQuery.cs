using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Devices.AlarmSystem;
using BlaisePascal.SmartHouse.Domain.Devices.AlarmSystem.AlarmSystemInterface;

namespace BlaisePascal.SmartHouse.Application.Devices.AlarmSystem.Queries
{
    public class GetAlarmStatusQuery
    {
        private readonly IAlarmSystemRepository _AlarmSystemRepository;
        public GetAlarmStatusQuery(IAlarmSystemRepository AlarmSystemRepository)
        {
            _AlarmSystemRepository = AlarmSystemRepository;
        }
        public SmokeDetector Execute(Guid id)
        {
            return _AlarmSystemRepository.GetById(id);
        }
    }
}
