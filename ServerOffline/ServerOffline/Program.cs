using Server;
using System;
using System.Threading;
using System.Windows.Forms;

namespace ServerOffline
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string chiavePrivataCriptazione = System.IO.File.ReadAllText(@"C:\Users\Andrea\Desktop\d\privata.txt");
            SocketServer socketServer = new SocketServer("ws://0.0.0.0:4500");
            Form1 form = new Form1();
            
            socketServer.Start(chiavePrivataCriptazione: chiavePrivataCriptazione, finestra: form);
            form.ShowDialog();
            Console.Read();

            
        }
    }
}
