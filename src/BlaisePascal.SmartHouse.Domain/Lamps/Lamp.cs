using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;
namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    
    public class Lamp : LampModel
    {
        public Lamp(Power Power,Name Brand,Brightness Max_brightness)
        {
            brightness_Perc = new Brightness(0);
            is_on = false;
            lastModifiedAtUtc = DateTime.Now;
        }

        public override void turnOn()
        {
            brightness_Perc = new Brightness(90);
            is_on = true;
            lastModifiedAtUtc = DateTime.Now;
        }

        public override void turnOff()
        {
            brightness_Perc = new Brightness(0);
            is_on = false;
            lastModifiedAtUtc = DateTime.Now;
        }

        public override void adjustBrightness(Brightness new_bright_perc)
        {           
            brightness_Perc = new_bright_perc;
            lastModifiedAtUtc = DateTime.Now;
        }

        public void ChangeColor(LampColor newColor)
        {
            if (!is_on)
            {             
                return;
            }
            Color = newColor;
            lastModifiedAtUtc = DateTime.Now;
        }
    }
}
    