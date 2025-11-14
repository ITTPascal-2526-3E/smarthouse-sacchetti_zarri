using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain
{
    public class AlarmSound
    {
        public void playAlarm()
        {
            Console.WriteLine("Alarm! Smoke detected!");
        }
    }
}