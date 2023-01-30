using Newtonsoft.Json;
using System;

namespace Server
{
    internal class MessageHandler
    {
        public static string HandleOnSubmit(Messaggio messaggioRicevuto, dynamic db, dynamic chiavePubblicaCriptazione)
        {
            if (db.Ricerca(messaggioRicevuto.username, messaggioRicevuto.password))
            {
                return JsonConvert.SerializeObject(new
                {
                    nomeEvento = "OnSubmitResponse",
                    messaggio = Crypt.RSAEncrypt(chiavePubblicaCriptazione, DateTime.Now.ToString())
                }
                );
            }
            return JsonConvert.SerializeObject(new
            {
                nomeEvento = "OnSubmitResponse",
                messaggio = ""
            }
                );
        }
    }
}

