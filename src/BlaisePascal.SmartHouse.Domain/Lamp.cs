namespace BlaisePascal.SmartHouse.Domain
{
    public class Lamp
    {
        public double brightness {  get; set; } //brightness is in Lumen
        private double power; //power is in Watt
        public bool is_on { get; set; } 
        private string brand;
        public string lamp_id { get; } //lamp idenficator code
        public string color { get; set; }
        
        public Lamp(double Power,string Brand,string Lamp_id)
        {
            if(double.IsPositive(Power))
            {
                power = Power;
            }
            
            if(!string.IsNullOrEmpty(Brand))
            {
                brand = Brand;
            }
            
            //lamp_id = Lamp_id;
           
        }
        public void turnOn()
        {
            is_on=true;
        }

        public void turnOff()
        {
            is_on = false;
        }

    }
}
