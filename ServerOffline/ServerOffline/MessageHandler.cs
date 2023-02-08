using Newtonsoft.Json;
using ServerOffline;
using System;
using System.Windows.Forms;

namespace Server
{
    internal static class MessageHandler
    {
        static string chiavePrecendente;

        public static string HandleOnSubmit(Messaggio messaggioRicevuto, dynamic db /*Funziona con la mia classe database*/, string chiavePubblicaCriptazione)
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
        public static string HandleOnAutoLogin(Messaggio messaggioRicevuto, string chiavePrivataCriptazione)
        {
            if (messaggioRicevuto.codice != null && VerificaCodice(messaggioRicevuto, chiavePrivataCriptazione))
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
        public static string HandleMove(Messaggio messaggioRicevuto, string chiavePrivataCriptazione, Finestra finestra)
        {
            var coordinata = (0, 0);
            if (messaggioRicevuto.codice != null && VerificaCodice(messaggioRicevuto, chiavePrivataCriptazione))
            {
                switch (messaggioRicevuto.movimento)
                {
                    case "Su":
                        coordinata = (0, -10);
                        break;
                    case "Giù":
                        coordinata = (0, 10);
                        break;
                    case "Destra":
                        coordinata = (10, 0);
                        break;
                    case "Sinistra":
                        coordinata = (-10, 0);
                        break;
                    default:
                        throw new Exception("Invalid command");

                }
                finestra.Invoke(new MethodInvoker(()=>{ finestra.Muovi(coordinata.Item1,coordinata.Item2); }));
                return JsonConvert.SerializeObject(new
                {
                    nomeEvento = "OnMoveResponse",
                    messaggio = true
                });
            }
            return JsonConvert.SerializeObject(new
            {
                nomeEvento = "OnMoveResponse",
                messaggio = false
            });
        }

        public static bool VerificaCodice(Messaggio messaggioRicevuto, string chiavePrivataCriptazione)
        {
            if (chiavePrecendente == messaggioRicevuto.codice)
                return true;
            if (messaggioRicevuto.codice == null || messaggioRicevuto.codice == string.Empty)
                return false;
            if ((DateTime.Now - DateTime.Parse(Crypt.RSADecrypt(chiavePrivataCriptazione, messaggioRicevuto.codice))).TotalDays < 1)
            { 
                chiavePrecendente = messaggioRicevuto.codice;
                return true;
            }
            return false;
            
        }
    }
}
