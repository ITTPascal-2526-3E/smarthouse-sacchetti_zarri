namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    
    public class Lamp : LampModel
    {
        public Lamp(double Power,string Brand,double Max_brightness)
        {
            if(double.IsPositive(Power))
            {
                power = Power;
            }
            
            if(!string.IsNullOrEmpty(Brand))
            {
                brand = Brand;
            }

            if(double.IsPositive(Max_brightness))
            {
                max_brightness = Max_brightness;
            }

            brightness_Perc = 0;
            is_on = false;
            lastModifiedAtUtc = DateTime.Now;
        }

        public override void turnOn()
        {
            brightness_Perc = 100;
            is_on = true;
            lastModifiedAtUtc = DateTime.Now;
        }

        public override void turnOff()
        {
            brightness_Perc = 0;
            is_on = false;
            lastModifiedAtUtc = DateTime.Now;
        }

        public override void adjustBrightness(int new_bright_perc)
        {
            if (int.IsPositive(new_bright_perc)&&new_bright_perc<=100)
            {
                brightness_Perc = new_bright_perc;
            }else 
                throw new ArgumentException("Brightness percentage must be between 0 and 100.");
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
    