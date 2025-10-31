namespace BlaisePascal.SmartHouse.Domain
{
    public class Lamp
    {
        public double max_brightness {  get; set; } //brightness is in Lumen
        public int brightness_Perc {  get; set; } //bright perc
        private double power; //power is in Watt
        public bool is_on { get; set; } 
        private string brand;
        public string lamp_id { get; } //lamp idenficator code
        public string color { get; set; }
        
        public Lamp(double Power,string Brand,string Lamp_id,double Max_brightness)
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
            
            //lamp_id = Lamp_id;
           
        }
        public void turnOn()
        {
            brightness_Perc = 100;
            is_on =true;
        }

        public void turnOff()
        {
            brightness_Perc = 0;
            is_on = false;
        }

    }
}
