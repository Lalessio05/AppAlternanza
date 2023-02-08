using Fleck;
using Newtonsoft.Json;
using ServerOffline;
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

        public void Start(IDataStorage db, string chiavePubblicaCriptazione = null, string chiavePrivataCriptazione = null, Finestra finestra = null)
        {
            //Riceve un interfaccia di dataStorage che ha una serie di metodi base, che la classe database estende
            //In questo modo non dipende più dal db ma dipende dall'interfaccia. Se ne frega di come sono implementati i metodi, finché fanno quello che vuoi
            server.Start((s) =>
            {
                s.OnOpen = () =>
                {
                    Console.WriteLine(DefaultOnOpen());
                };
                s.OnClose = () =>
                {
                    GestioneSalvataggi.Salva("Gianni",db);
                    Finestra.labirintiCompletati = 0;
                };
                //Gestire le OnMessage... con funzioni fatte e definite
                s.OnMessage = message =>
                {

                    Messaggio messaggioRicevuto = ReceiveJson(s, message);

                    switch (messaggioRicevuto.nomeEvento)
                    {
                        case "OnSubmit":
                            s.Send(MessageHandler.HandleOnSubmit(messaggioRicevuto, db, chiavePubblicaCriptazione));
                            break;
                        case "OnAutoLogin":
                            s.Send(MessageHandler.HandleOnAutoLogin(messaggioRicevuto, chiavePrivataCriptazione));

                            break;
                        case "OnMove":
                            s.Send(MessageHandler.HandleMove(messaggioRicevuto, chiavePrivataCriptazione, finestra));
                            break;
                        case "OnStart":
                            GestioneSalvataggi.Carica(db,messaggioRicevuto.username);
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