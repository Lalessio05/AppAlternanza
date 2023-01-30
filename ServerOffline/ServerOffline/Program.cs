using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerOffline
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string chiavePrivataCriptazione = System.IO.File.ReadAllText(@"C:\Users\Andrea\Desktop\d\privata.txt");
            SocketServer socketServer = new SocketServer("ws://0.0.0.0:4500");
            socketServer.Start(chiavePrivataCriptazione: chiavePrivataCriptazione);
            Console.Read();
        }
    }
}
