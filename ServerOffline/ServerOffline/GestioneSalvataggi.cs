using Server;
namespace ServerOffline
{
    internal class GestioneSalvataggi
    {
        public static void Salva(string username, Database db)
        {
            if (!db.RicercaUtente(username))
            {
                db.AggiungiUtente(username, Finestra.labirintiCompletati);
            }
            else
                db.AggiornaUtente(username, Finestra.labirintiCompletati);
        }
        public static void Carica(Database db, string username)
        {
            if (db.RicercaUtente(username))
            {
                Finestra.labirintiCompletati = db.RicercaLivelliCompletati(username);
            }
        }
    }
}
