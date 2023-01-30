using System;
using System.Drawing;
using System.Windows.Forms;

namespace ServerOffline
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void Muovi((int,int) coordinata)
        {
            label1.Location = new Point(label1.Location.X - coordinata.Item1, label1.Location.Y-coordinata.Item2);
        }
    }
}
