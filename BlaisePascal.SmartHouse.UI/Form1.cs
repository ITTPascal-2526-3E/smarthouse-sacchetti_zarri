using BlaisePascal.SmartHouse.Domain.Lamps;

namespace BlaisePascal.SmartHouse.UI
{
    public partial class Form1 : Form
    {
        Lamp miaLampada = new Lamp(10.0, "Philips", 1000.0);
        public Form1()
        {
            InitializeComponent();
            label1.Text = "La luce è spenta";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (Accesa.Checked)
            {
                miaLampada.turnOn();
                label1.Text = "Luce Accesa!";
                label1.ForeColor = Color.Gold;
            }
            else
            {
                miaLampada.turnOff();
                label1.Text = "Luce Spenta";
                label1.ForeColor = Color.Black;
            }

        }
    }
}
