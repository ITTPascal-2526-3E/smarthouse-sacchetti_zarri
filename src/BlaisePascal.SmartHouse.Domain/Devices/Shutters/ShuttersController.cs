using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Devices.Shutters
{
    public class ShuttersController
    {
        private Shutter parent;

        public bool status { get; private set; }
        public bool Control { get; private set; }


        public ShuttersController(Shutter Parent)
        {
            parent = Parent;
        }

        public void Start()
        {
            Control = true;
            while (Control == true)
            {
                ConsoleKeyInfo tasto = Console.ReadKey(true); // legge un tasto senza stamparlo

                if (tasto.Key == ConsoleKey.A)
                {
                    parent.Open();
                }
                else if (tasto.Key == ConsoleKey.C)
                {
                    parent.Close();
                }
                else
                {
                    break;
                }
            }
        }
    }
}
