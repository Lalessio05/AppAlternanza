using System.Data;

namespace ServerOffline
{
    internal interface IDataStorage
    {
        void CreaTabella(string nomeTabella, string[] colonne);
        bool Esiste(string nomeTabella, string nomeColonna, object value);
        void AggiungiRiga(string nomeTabella, object[] valori);
         void AggiornaElemento(string nomeTabella, string nomeColonna, object columnValue, string condizioneNomeColonna, object valoreConfronto);
        void DeleteRow(string nomeTabella, string condizioneNomeColonna, object valoreConfronto);
        DataRow OttieniRiga(string nomeTabella, string condizioneNomeColonna, object valoreConfronto);
        DataTable OttieniRighe(string nomeTabella);
        DataTable EseguiQuery(string sql);
    }
}
