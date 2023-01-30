using Fleck;
using Newtonsoft.Json;
using System;

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
        public void Start(dynamic db = null /*Funziona con la mia classe database*/,  string chiavePubblicaCriptazione = null, string chiavePrivataCriptazione = null)
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
                            s.Send(MessageHandler.HandleOnSubmit(messaggioRicevuto, db, chiavePubblicaCriptazione));
                            break;
                        case "OnAutoLogin":
                                s.Send(MessageHandler.HandleOnAutoLogin(messaggioRicevuto,chiavePrivataCriptazione));
                            break;
                        default:
                            break;
                    }
                };
            });
            
        }

        public string DefaultOnClose()
        {
            return "A user disconnected from your channel";
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