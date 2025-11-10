using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain
{
    public class AutoShutters
    {
        public bool status { get; private set; }
        public bool Control { get; private set; }

        public void Start()
        {
            Control = true;
            while (Control == true)
            {
                ConsoleKeyInfo tasto = Console.ReadKey(true); // legge un tasto senza stamparlo

                if (tasto.Key == ConsoleKey.A)
                {
                    status = true;
                }
                else if (tasto.Key == ConsoleKey.C)
                {
                    status = false;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
