namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    
    public class Lamp
    {
        public double max_brightness {  get; set; } //brightness is in Lumen
        public int brightness_Perc {  get; set; } //bright perc
        public double power { get; private set; }//power is in Watt
        public bool is_on { get; set; }
        public string brand { get; }
        public Guid lamp_Id { get; set; } = Guid.NewGuid(); //lamp idenficator code (il lamp id verra gestito da una classe esterna AssegnaLampId che controllera la univocità degli lamp_id della casa)
        public LampColor Color { get; set; }

        public DateTime? startTime;
        public DateTime lastModifiedAtUtc;


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
        public void turnOn()
        {
            brightness_Perc = 100;
            is_on = true;
            startTime = DateTime.Now;
            lastModifiedAtUtc = DateTime.Now;
        }

        public void turnOff()
        {
            brightness_Perc = 0;
            is_on = false;
            startTime = null;
            lastModifiedAtUtc = DateTime.Now;
        }

        public void adjustBrightness(int new_bright_perc)
        {
            if (int.IsPositive(new_bright_perc)&&new_bright_perc<=100)
            {
                brightness_Perc = new_bright_perc;
            }else throw new ArgumentException("Brightness percentage must be between 0 and 100.");

        }

        public void ChangeColor(LampColor newColor)
        {
            if (!is_on)
            {             
                return;
            }
            Color = newColor;
        }

        public void LastModifiedAtUtc()
        {
            Console.WriteLine($"Last modified at: {lastModifiedAtUtc}");
        }


    }
}
    