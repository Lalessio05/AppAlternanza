using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerOffline
{
    public partial class Finestra : Form
    {
        (int Width, int Height) DimensioniFinestra;
        int nMuri;
        bool gravità;
        bool completabile;
        bool[,] labirinto;
        bool[,] spaziVisitati;
        public static int labirintiCompletati;
        Label labelObbiettivo;
        Label labelVittoria;
        Label labelPersonaggio;
        ConcurrentQueue<(int, int)> codaOperazioni;

        public Finestra(int width, int height, bool gravità, int nMuri)
        {
            InitializeComponent();
            codaOperazioni = new ConcurrentQueue<(int, int)>();
            Size = new Size(width, height);
            DimensioniFinestra = (width, height);
            this.gravità = gravità;
            this.nMuri = nMuri;
            Task evadi = Task.Run(async () => { await Evadi(); });
            this.CreaCampo();
        }
        private bool IsValidLocation(Point location)
        {
            return location.X <= DimensioniFinestra.Width &&
                   location.X > 0 &&
                   location.Y <= DimensioniFinestra.Height &&
                   location.Y > 0 &&
                   !labirinto[location.X / 10, location.Y / 10];
        }
        private void CreaPartenzaETraguardo()
        {
            completabile = false;
            Controls.Clear();
            labelPersonaggio = new Label();
            labelPersonaggio.Text = "T";
            labelPersonaggio.Location = new Point(200, 200);
            labelPersonaggio.Width = 10;
            labelPersonaggio.Height = 10;
            labelPersonaggio.ForeColor = Color.Red;
            Controls.Add(labelPersonaggio);
            labelObbiettivo = new Label();
            labelObbiettivo.Location = new Point(DimensioniFinestra.Width, DimensioniFinestra.Height);
            //labelObbiettivo.Location = new Point(210, 210);
            labelObbiettivo.Width = 10;
            labelObbiettivo.Height = 10;
            labelObbiettivo.ForeColor = Color.Green;
            labelObbiettivo.Text = "O";
            this.Controls.Add(labelObbiettivo);
        }
        private void AggiungiLabel(int x, int y)
        {
            Label l = new Label();
            l.Location = new Point(x * 10, y * 10);
            l.BackColor = Color.Black;
            l.Width = 10;
            l.Height = 10;
            this.Controls.Add(l);
        }
        private void CreaCampo()
        {
            CreaPartenzaETraguardo();
            nMuri = nMuri + nMuri* (labirintiCompletati + 1 / 5);
            Random rnd = new Random();
            while (!completabile)
            {
                labirinto = new bool[DimensioniFinestra.Width / 10 + 1, DimensioniFinestra.Height / 10 + 1];
                spaziVisitati = new bool[DimensioniFinestra.Width / 10 + 1, DimensioniFinestra.Height / 10 + 1];

                for (int i = 0; i < nMuri; i++)
                {
                    int x = rnd.Next(0, DimensioniFinestra.Width / 10 + 1);
                    int y = rnd.Next(0, DimensioniFinestra.Height / 10 + 1);
                    while (labirinto[x, y] || (x * 10 == 200 && y * 10 == 200) || (x == DimensioniFinestra.Height && y == DimensioniFinestra.Height))
                    {
                        x = rnd.Next(0, DimensioniFinestra.Width / 10 + 1);
                        y = rnd.Next(0, DimensioniFinestra.Height / 10 + 1);
                    }
                    labirinto[x, y] = true;
                    AggiungiLabel(x, y);
                    Console.WriteLine((double)i / nMuri * 100 + "%");
                }
                completabile = Controllo(20, 20);
            }
            Refresh();
        }
        
        public void Muovi(int x, int y)
        {
            var futureLocation = new Point(labelPersonaggio.Location.X + x, labelPersonaggio.Location.Y + y);
            if (!IsValidLocation(futureLocation))
                return;
            Invoke(new MethodInvoker(() => { labelPersonaggio.Location = futureLocation; }));
            if (labelPersonaggio.Location == labelObbiettivo.Location)
            {
                Vittoria();
            }
        }
        private async Task Evadi()
        {
            while (true)
                while (completabile)
                {
                    (int, int) item;

                    if (codaOperazioni.TryDequeue(out item))
                        Muovi(item.Item1, item.Item2);
                    else if (gravità)
                    {
                        await Task.Delay(200);
                        Muovi(0, 10);
                    }
                }
        }
        //public void Metodo(int x, int y)
        //{
        //    codaOperazioni.Enqueue((x, y));
        //}
        private bool Controllo(int x, int y)
        {
            if (x < 0 || x > DimensioniFinestra.Width / 10 || y < 0 || y > DimensioniFinestra.Height / 10)
                return false;

            if (x == DimensioniFinestra.Width / 10 && y == DimensioniFinestra.Height / 10)
                return true;

            if (spaziVisitati[x, y])
                return false;

            if (labirinto[x, y])
                return false;
            spaziVisitati[x, y] = true;
            if (
                Controllo(x + 1, y) ||
                Controllo(x, y + 1) ||
                Controllo(x - 1, y) ||
                Controllo(x, y - 1))
                return true;

            return false;
        }

        private void Vittoria()
        {
            labirintiCompletati++;
            Controls.Clear();
            labelVittoria = new Label();
            labelVittoria.Text = "Hai vinto";
            labelVittoria.Width = DimensioniFinestra.Width / 2;
            labelVittoria.Height = DimensioniFinestra.Height / 2;
            labelVittoria.Font = new Font(labelVittoria.Font.FontFamily, 30);
            labelVittoria.Location = new Point((DimensioniFinestra.Width - labelVittoria.Width) / 2, (DimensioniFinestra.Height - labelVittoria.Height) / 2);
            Controls.Add(labelVittoria);
            Refresh();
            Thread.Sleep(3000);
            CreaCampo();
        }
    }


}