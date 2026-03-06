using System;
using System.Collections.Generic;
using System.Linq;
using BlaisePascal.SmartHouse.Domain.Devices.Lamps.LampsInterfaces.Repositories;
using BlaisePascal.SmartHouse.Domain.Devices.Lamps;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;

namespace BlaisePascal.SmartHouse.infrastructure.Repositories.Devices.Lamps
{
    public class InMemoryLampRepository : ILampRepository
    {
        private readonly List<Lamp> _lamps;

        public InMemoryLampRepository()
        {
            _lamps = new List<Lamp>
            {
                new Lamp(new Power(10), new Name("Lampada Salotto"), new Brightness(100))
            };
        }

        public void Add(Lamp lamp)
        {
            if (lamp == null)
            {
                throw new ArgumentNullException(nameof(lamp));
            }
            _lamps.Add(lamp);
        }

        public List<Lamp> GetAll()
        {
            return _lamps;
        }

        public Lamp GetById(Guid id)
        {
            return _lamps.FirstOrDefault(l => l.deviceId == id);
        }

        public void Remove(Lamp lamp)
        {
            if (lamp == null)
            {
                throw new ArgumentNullException(nameof(lamp));
            }
            _lamps.Remove(lamp);
        }

        public void Update(Lamp lamp)
        {

        }
    }
}