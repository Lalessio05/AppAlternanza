using System;
using System.Data.SQLite;

namespace Server
{
    //Diventa una interfaccia chiamata tipo datastorage ed estenderla
    internal class Database
    {
        SQLiteConnection connection;
        SQLiteCommand command;
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
        public void CreaTabella()
        {
            string sql = "CREATE TABLE Utenti (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, " +
                         "Username TEXT NOT NULL UNIQUE, " +
                         "LivelliCompletati INTEGER NOT NULL);";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public bool RicercaAdmin(string Username,string Password)
        {
            command = new SQLiteCommand("SELECT * FROM Admins WHERE Username = '" + Username + "' AND Password = '" + Password + "';",connection);
            SQLiteDataReader reader = command.ExecuteReader();
            return reader.HasRows;
        }
        public void AggiungiUtente(string Username,int nLivelli)
        {
            if (!RicercaUtente(Username))
            {
            command = new SQLiteCommand($"INSERT INTO Utenti (Username, LivelliCompletati) VALUES (\"" + Username +"\", "+nLivelli+");\r\n",connection);
            command.ExecuteNonQuery();

            }
        }
        public void AggiornaUtente(string Username,int nLivelli)
        {
            command = new SQLiteCommand($"UPDATE Utenti SET LivelliCompletati = {nLivelli} WHERE Username = \"{Username}\";\r\n",connection);
            command.ExecuteNonQuery();
        }
        public bool RicercaUtente(string Username)
        {
            command = new SQLiteCommand("SELECT * FROM Utenti WHERE Username = '" + Username + "';",connection);
            SQLiteDataReader reader = command.ExecuteReader();
            //while (reader.Read())
            //{
            //    return reader["LivelliCompletati"];
            //}
            return reader.HasRows;
        }
        public int RicercaLivelliCompletati(string Username)
        {
            command = new SQLiteCommand("SELECT LivelliCompletati FROM Utenti WHERE Username ='" + Username + "';",connection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0);
        }
        ~Database()
        {
            connection.Close();
        }
    }
}

// Crea una nuova tabella 
//string sql = "CREATE TABLE Admins (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, " +
//             "Username TEXT NOT NULL UNIQUE, " +
//             "Password TEXT NOT NULL);";
//SQLiteCommand command = new SQLiteCommand(sql, connection);
//command.ExecuteNonQuery();


