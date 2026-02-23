using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Devices.AlarmSystem.AlarmSystemInterface;

namespace BlaisePascal.SmartHouse.Application.Devices.AlarmSystem.Command
{
    public class ArmAlarmCommand
    {
        private readonly IAlarmSystemRepository _AlarmSystemRepository;
        public ArmAlarmCommand(IAlarmSystemRepository AlarmSystemRepository)
        {
            _AlarmSystemRepository = AlarmSystemRepository;
        }
        public void Execute(Guid id)
        {
            var alarmSystem = _AlarmSystemRepository.GetById(id);
            if (alarmSystem != null)
            {
                alarmSystem.turnOn();
                _AlarmSystemRepository.Update(alarmSystem);
            }
        }
    }
}
