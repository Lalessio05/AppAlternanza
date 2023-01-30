using Fleck;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace Server
{
    internal class SocketServer
    {
        WebSocketServer server;
        public SocketServer(string indirizzo) 
        {
            FleckLog.Level = LogLevel.Debug;
            server = new WebSocketServer(indirizzo);
            
        }
        public string DefaultOnOpen()
        {
            return "An user connected to your channel";
        }
        public void Start(Server.Database db = null, string chiavePubblicaCriptazione = null)
        {
            server.Start((s) =>
            {
                s.OnOpen = () =>
                {
                    Console.WriteLine(DefaultOnOpen());
                };
                s.OnClose = () =>
                {
                    Console.WriteLine(DefaultOnClose());
                };
                s.OnMessage = message =>
                {

                    Messaggio messaggioRicevuto = ReceiveJson(s, message);
                    switch (messaggioRicevuto.nomeEvento)
                    {
                        case "OnSubmit":
                            var risposta = MessageHandler.HandleOnSubmit(messaggioRicevuto, db, chiavePubblicaCriptazione);
                            s.Send(risposta);
                            break;
                        default:
                            break;
                    }
                };
            });
            
        }

        public string DefaultOnClose()
        {
            return "A user disconnected";
        }
        public Messaggio ReceiveJson(IWebSocketConnection s, string message)    
        {
            return JsonConvert.DeserializeObject<Messaggio>(message);
        }
        public void DefaultSend(string messaggio, IWebSocketConnection s)
        {
            s.Send(messaggio);
        }

    }
}