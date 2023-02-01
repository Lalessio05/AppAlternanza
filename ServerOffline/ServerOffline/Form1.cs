using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ServerOffline
{
    public partial class Form1 : Form
    {
        bool[,] arr;

        private void CreaCampo()
        {
            int nOstacoli = 1000;
            Random rnd = new Random();
            while (!completabile)
            {
                arr = new bool[Dimensioni.Width / 10 + 1, Dimensioni.Height / 10 + 1];
                visitato = new bool[Dimensioni.Width / 10 + 1, Dimensioni.Height / 10 + 1];

                for (int i = 0; i < nOstacoli; i++)
                {
                    int x = rnd.Next(0, Dimensioni.Width / 10);
                    int y = rnd.Next(0, Dimensioni.Height / 10);
                    while (arr[x, y] || (x * 10 == 200 && y * 10 == 200))
                    {
                        x = rnd.Next(0, Dimensioni.Width / 10);
                        y = rnd.Next(0, Dimensioni.Height / 10);
                    }
                    arr[x, y] = true;
                    Label l = new Label();
                    l.Location = new Point(x * 10, y * 10);
                    l.Text = "X";
                    l.Width = 10;
                    l.Height = 10;
                    this.Controls.Add(l);
                    Console.WriteLine((double)i / nOstacoli * 100 + "%");

                }
                //arr[192, 107] = true;
                //arr[191, 107] = true;
                //arr[191, 108] = true;
                //Label l = new Label();
                //l.Text = "X";
                //l.Location = new Point(1920, 1070);
                //l.Width = 10;
                //l.Height = 10;
                //Controls.Add(l);
                //l = new Label();
                //l.Text = "X";
                //l.Location = new Point(1910, 1070);
                //l.Width = 10;
                //l.Height = 10;
                //Controls.Add(l);
                //l = new Label();
                //l.Text = "X";
                //l.Location = new Point(1910, 1080);
                //l.Width = 10;
                //l.Height = 10;
                //Controls.Add(l);
                //Thread t = new Thread(new ThreadStart(() => Console.WriteLine(completabile = Controllo(20, 20)) ), 1000000);
                completabile = Controllo(20, 20); 
                //t.Start();
                //t.Join();
            }
            this.Refresh();

        }
        public Form1(int width, int height)
        {
            InitializeComponent();
            Size = new Size(width, height);
            Dimensioni = (width, height);
            label1.Location = new Point(200, 200);
            label1.Width = 10;
            label1.Height = 10;
            label1.ForeColor = Color.Red;

            Label label2 = new Label();
            label2.Location = new Point(Dimensioni.Width, Dimensioni.Height);
            label2.Width = 10;
            label2.Height = 10;
            label2.ForeColor = Color.Green;
            label2.Text = "O";
            this.Controls.Add(label2);
            visitato = new bool[Dimensioni.Width / 10 + 1, Dimensioni.Height / 10 + 1];
            this.Refresh();
            this.CreaCampo();
        }
        (int Width, int Height) Dimensioni;
        bool completabile;
        public void Muovi((int, int) coordinata)
        {
            var prevLocation = label1.Location;

            label1.Location = new Point(label1.Location.X + coordinata.Item1, label1.Location.Y + coordinata.Item2);
            
            if (label1.Location.X >= Dimensioni.Width)
            {
                label1.Location = new Point(Dimensioni.Width, label1.Location.Y);
            }
            else if (label1.Location.X <= 0)
            {
                label1.Location = new Point(0, label1.Location.Y);
            }
            if (label1.Location.Y >= Dimensioni.Height)
            {
                label1.Location = new Point(label1.Location.X, Dimensioni.Height);
            }
            else if (label1.Location.Y <= 0)
            {
                label1.Location = new Point(label1.Location.X, 0);
            }
            if (arr[label1.Location.X/10, label1.Location.Y/10])
                label1.Location = prevLocation;

        }
        bool[,] visitato;
        private bool Controllo(int x, int y)
        {
            if (x < 0 || x > Dimensioni.Width / 10 || y < 0 || y > Dimensioni.Height / 10)
                return false;

            if (x == Dimensioni.Width / 10 && y == Dimensioni.Height / 10)
                return true;

            if (visitato[x, y])
                return false;

            if (arr[x, y])
                return false;
            Console.WriteLine(x + " " + y);
            visitato[x, y] = true;
            if (
                Controllo(x + 1, y) ||
                Controllo(x, y + 1) ||
                Controllo(x - 1, y) ||
                Controllo(x, y - 1))
                return true;

            return false;


        }
        //Mini campo minato(?), se è una bomba la x diventa rossa, sennò va via






    }
}