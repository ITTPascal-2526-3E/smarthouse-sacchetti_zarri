using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain
{
    public class ShuttersController
    {
        private Shutters parent;

        public bool status { get; private set; }
        public bool Control { get; private set; }


        public ShuttersController(Shutters Parent)
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
                    parent.is_open=true;
                    parent.is_closed = false;
                    status = true;
                }
                else if (tasto.Key == ConsoleKey.C)
                {
                    parent.is_closed = true;
                    parent.is_open = false;
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
