using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaisePascal.SmartHouse.Domain.Devices.Lamps.LampsInterfaces.Repositories;
using BlaisePascal.SmartHouse.Domain.Devices.Lamps;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;

namespace BlaisePascal.SmartHouse.infrastructure.Repositories.Devices.Lamps
{
    public class CsvLampRepository
    {
        private readonly string _filePath = "lamps.csv";

        public CsvLampRepository()
        {
            var solutionRoot = LocalPathHelper.GetSolutionRoot();
            var dataFolder = Path.Combine(solutionRoot, "Data");
            Directory.CreateDirectory(dataFolder);

            _filePath = Path.Combine(dataFolder, "lamps.csv");
            if (!File.Exists(_filePath))
            {
                Save(new List<Lamp>());
            }
        }

        public List<Lamp> GetAll()
        {
            return Load();
        }

        public Lamp GetById(Guid id)
        {
            return Load().First(l => l.deviceId == id);
        }

        private void Save(List<Lamp> lamps)
        {
            var dtos = lamps;
            var lines = new List<string>
            {
                "Id,Name,Brand,Color,Brightness,IsOn"
            };

            foreach (var dto in dtos)
            {
                lines.Add(string.Join(",",
                    dto.deviceId,
                    dto.brand.Value,
                    dto.Color,
                    dto.is_on));
            }

            File.WriteAllLines(_filePath, lines);
        }

        private List<Lamp> Load()
        {
            var lines = File.ReadAllLines(_filePath);
            var lamps = new List<Lamp>();
            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(',');
                var dto = new Lamp(
                    new Power(int.Parse(parts[0])),
                    new Name(parts[1]),
                    new Brightness(int.Parse(parts[4]))
                );
                lamps.Add(dto);
            }
            return lamps;
        }

        public void Update(Lamp lamp)
        {
            var lamps = Load();
            var index = lamps.FindIndex(l => l.deviceId == lamp.deviceId);
            if (index >= 0)
            {
                lamps[index] = lamp;
                Save(lamps);
            }
        }

        public void Add(Lamp lamp)
        {
            var lamps = Load();
            lamps.Add(lamp);
            Save(lamps);
        }   


        public void Remove(Lamp lamp)
        {
            var lamps = Load();
            lamps.RemoveAll(l => l.deviceId == lamp.deviceId);
            Save(lamps);
        }
    }
}
