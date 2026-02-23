using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Devices.AlarmSystem.AlarmSystemInterface;

namespace BlaisePascal.SmartHouse.Application.Devices.AlarmSystem.Command
{
    public class RemoveAlarmSystemCommand
    {
        private readonly IAlarmSystemRepository _AlarmSystemRepository;
        public RemoveAlarmSystemCommand(IAlarmSystemRepository AlarmSystemRepository)
        {
            _AlarmSystemRepository = AlarmSystemRepository;
        }
        public void Execute(Guid id)
        {
            var alarmSystem = _AlarmSystemRepository.GetById(id);
            if (alarmSystem != null)
            {
                _AlarmSystemRepository.Remove(alarmSystem);
            }
        }
    }
}
