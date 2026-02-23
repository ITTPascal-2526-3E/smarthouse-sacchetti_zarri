using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Devices.AlarmSystem.AlarmSystemInterface;

namespace BlaisePascal.SmartHouse.Application.Devices.AlarmSystem.Command
{
    public class RegisterAlarmSystemCommand
    {
        private readonly IAlarmSystemRepository _AlarmSystemRepository;
        public RegisterAlarmSystemCommand(IAlarmSystemRepository AlarmSystemRepository)
        {
            _AlarmSystemRepository = AlarmSystemRepository;
        }

        public void Execute()
        {
            var alarmSystem = new Domain.Devices.AlarmSystem.SmokeDetector();
            _AlarmSystemRepository.Add(alarmSystem);
        }
    }
}
