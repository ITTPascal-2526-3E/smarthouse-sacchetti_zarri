using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain
{
    public class SmokeDetector : Device
    {
        public bool smoke_detectet { get; private set; }
        public bool smoke { get; set; }
        AlarmSound alarm2 = new AlarmSound();



        public override void turnOn()
        {
            smoke = false;
            smoke_detectet = false;
            lastModifiedAtUtc = DateTime.Now;
        }   
        public override void turnOff()
        {
            smoke = false;
            smoke_detectet = false;
            lastModifiedAtUtc = DateTime.Now;
        }

        public void smokeDetector()
        {
            if (smoke == true)
            {
                smoke_detectet = true;
            }
            lastModifiedAtUtc = DateTime.Now;
        }

        public void alarm()
        {
            if (smoke_detectet == true)
            {
                for (int i = 0; i < 2; i++)
                {
                    Thread.Sleep(1000); //simulate delay before alarm
                    alarm2.playAlarm();
                }
            }

            smoke_detectet = false;
        }

    }
}
