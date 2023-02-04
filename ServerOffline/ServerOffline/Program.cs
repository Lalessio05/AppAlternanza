using Server;
using System;
namespace ServerOffline
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int windowWidth = 1020;
            int windowHeight = 720;
            int nMuri = 100;
            bool gravità = false;

            Database db = new Database("mydb.db");



            string chiavePrivataCriptazione = System.IO.File.ReadAllText(@"C:\Users\Andrea\Desktop\d\privata.txt");
            SocketServer socketServer = new SocketServer("ws://0.0.0.0:4500");

            Finestra form = new Finestra(width: windowWidth, height: windowHeight, gravità: gravità, nMuri: nMuri);

            socketServer.Start(db: db, chiavePrivataCriptazione: chiavePrivataCriptazione, finestra: form);
            form.ShowDialog();
            Console.Read();


        }
    }
}
