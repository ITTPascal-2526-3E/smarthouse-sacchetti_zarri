namespace BlaisePascal.SmartHouse.Domain
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
            
            
        
        }
        public void turnOn()
        {
            brightness_Perc = 100;
            is_on = true;
        }

        public void turnOff()
        {
            brightness_Perc = 0;
            is_on = false;
        }

        public void adjustBrightness(int new_bright_perc)
        {
            if (int.IsPositive(new_bright_perc)&&new_bright_perc<=100)
            {
               
                brightness_Perc = new_bright_perc;
            }

        }

        public void ChangeColor(LampColor newColor)
        {
            if (!is_on)
            {             
                return;
            }
            Color = newColor;
        }


    }
}
    