using BlaisePascal.SmartHouse.Domain.Devices.Lamps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Devices.AlarmSystem.AlarmSystemInterface
{
    public interface IAlarmSystemRepository
    {
        void Add(SmokeDetector alarm);
        void Update(SmokeDetector alarm);
        void Remove(SmokeDetector alarm);
        SmokeDetector GetById(Guid id);
    }
}
