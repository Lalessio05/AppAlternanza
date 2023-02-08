using Server;
using System;
using System.Runtime.Serialization;

namespace ServerOffline
{
    internal class GestioneSalvataggi
    {
        public static void Salva(string username, IDataStorage db)
        {
            if (!db.Esiste("Utenti", username, "Username"))
            {

                db.AggiungiRiga("Utenti", new object[] { username, Finestra.labirintiCompletati });
            }
            else
                db.AggiornaElemento("Utenti", "LabirintiCompletati", Finestra.labirintiCompletati, "Username", username); ;
        }
        public static void Carica(IDataStorage db, string username)
        {
            if (db.Esiste("Utenti", "Username",username ))
            {
                var obj = db.OttieniRiga("Utenti", "Username", username);
                Finestra.labirintiCompletati = Convert.ToInt32(obj["LabirintiCompletati"]);
            }
        }
    }
}
