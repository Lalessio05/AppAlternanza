using System;

namespace Server
{
    class ServerD
    {
        public static void Main() 
        {
            Server.Database db = new Server.Database("mydb.db");
            string chiavePubblicaCriptazione = System.IO.File.ReadAllText(@"C:\Users\Andrea\Desktop\d\pubblica.txt");
            SocketServer socketServer = new SocketServer("ws://0.0.0.0:3000");
            socketServer.Start(db,chiavePubblicaCriptazione);
            //Thread t = new Thread(new ThreadStart(socketServer.Start(db,chiavePubblicaCriptazione)));

            Console.Read();
        }


    }

}
