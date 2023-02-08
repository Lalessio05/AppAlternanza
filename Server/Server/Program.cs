using System;

namespace Server
{
    class ServerD
    {
        public static void Main() 
        {
            Server.Database db = new Server.Database("mydb.db");

            string chiavePubblicaCriptazione = System.IO.File.ReadAllText(@"C:\Users\catri\Documents\py\AppAlternanza\chiavi\pubblica.txt");
            
            SocketServer socketServer = new SocketServer("ws://0.0.0.0:3000");
            
            socketServer.Start(db,chiavePubblicaCriptazione);
            
            
            Console.Read();
        }


    }

}
