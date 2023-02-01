namespace Server
{
    internal class Messaggio
    {
        public string nomeEvento;
        public string username;
        public string password;
        public string codice;
        public string movimento;
        public Messaggio(string nomeEvento, string username = null, string password = null, string codice = null, string movimento = null)
        {
            this.nomeEvento = nomeEvento;
            this.username = username;
            this.password = password;
            this.codice = codice;
            this.movimento = movimento;
        }
        public override string ToString()
        {
            return nomeEvento + " " + username + " " + password;
        }
    }
}