using ServerOffline;
using System;
using System.Data;
using System.Data.SQLite;

namespace Server
{
    internal class Database : IDataStorage
    {
        private SQLiteConnection connection;
        private SQLiteCommand command;
        public Database(string path)
        {
            try
            {
                connection = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                connection.Open();
            }
            catch
            {
                throw new Exception("Impossibile connettersi al database");
            }
        }

        public void CreaTabella(string nomeTabella, string[] colonne)
        {
            var columnDefinitions = string.Join(",", colonne);
            var sql = $"CREATE TABLE IF NOT EXISTS {nomeTabella} ({columnDefinitions})";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public bool Esiste(string nomeTabella, string nomeColonna, object value)
        {
            var sql = $"SELECT COUNT(*) FROM {nomeTabella} WHERE {nomeColonna} = @valore";
            command.CommandText = sql;
            command.Parameters.AddWithValue("@value", value);
            var count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }

        public void AggiungiRiga(string nomeTabella, object[] valori)
        {
            var valuePlaceholders = string.Join(",", new string[valori.Length]);
            var sql = $"INSERT INTO {nomeTabella} VALUES ({valuePlaceholders})";
            command.CommandText = sql;
            for (int i = 0; i < valori.Length; i++)
            {
                command.Parameters.AddWithValue($"@p{i}", valori[i]);
            }
            command.ExecuteNonQuery();
        }
        public void AggiornaElemento(string nomeTabella, string nomeColonna, object columnValue, string condizioneNomeColonna, object valoreConfronto)
        {

            var sql = $"UPDATE {nomeTabella} SET {nomeColonna} = @columnValue WHERE {condizioneNomeColonna} = @condition";
            command.CommandText = sql;
            command.Parameters.AddWithValue("@columnValue", columnValue);
            command.Parameters.AddWithValue("@condition", valoreConfronto);
            command.ExecuteNonQuery();
        }
        public void DeleteRow(string nomeTabella, string condizioneNomeColonna, object valoreConfronto)
        {
            var sql = $"DELETE FROM {nomeTabella} WHERE {condizioneNomeColonna} = @condition";
            command.CommandText = sql;
            command.Parameters.AddWithValue("@condition", valoreConfronto);
            command.ExecuteNonQuery();
        }
        public DataRow OttieniRiga(string nomeTabella, string condizioneNomeColonna, object valoreConfronto)
        {
            var dataTable = new DataTable();
            var sql = $"SELECT * FROM {nomeTabella} WHERE {condizioneNomeColonna} = @condition";
            command.CommandText = sql;
            command.Parameters.AddWithValue("@condition", valoreConfronto);
            using (var reader = command.ExecuteReader())
            {
                dataTable.Load(reader);
            }
            return dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null;
        }

        public DataTable OttieniRighe(string nomeTabella)
        {
            var dataTable = new DataTable();
            var sql = $"SELECT * FROM {nomeTabella}";
            command.CommandText = sql;
            using (var reader = command.ExecuteReader())
            {
                dataTable.Load(reader);
            }
            return dataTable;
        }
        public DataTable EseguiQuery(string sql)
        {
            var dataTable = new DataTable();
            command.CommandText = sql;
            using (var reader = command.ExecuteReader())
            {
                dataTable.Load(reader);
            }
            return dataTable;
        }

        ~Database()
        {
            connection.Close();
        }
    }
}
