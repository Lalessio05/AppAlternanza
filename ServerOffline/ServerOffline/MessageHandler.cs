using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Server
{
    internal class MessageHandler
    {
        public static string HandleOnSubmit(Messaggio messaggioRicevuto, dynamic db, dynamic chiavePubblicaCriptazione)
        {
            if (db == null)
            {
                throw new ArgumentNullException("Il database non esiste");
            }
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
        public static string HandleOnAutoLogin(Messaggio messaggioRicevuto, dynamic chiavePrivataCriptazione)
        {
            if ( messaggioRicevuto.codice != null && (DateTime.Now - DateTime.Parse(Crypt.RSADecrypt(chiavePrivataCriptazione, messaggioRicevuto.codice))).TotalDays < 1 )
                return JsonConvert.SerializeObject(new
                {
                    nomeEvento = "OnAutoLoginResponse",
                    messaggio = true
                });
            return JsonConvert.SerializeObject(new
            {
                nomeEvento = "OnAutoLoginResponse",
                messaggio = false
            });
        }
    }
}

