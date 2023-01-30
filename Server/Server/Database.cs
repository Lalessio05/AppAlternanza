using System;
using System.Data.SQLite;

namespace Server
{
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
        public bool Ricerca(string Username,string Password)
        {
            command = new SQLiteCommand("SELECT * FROM Admins WHERE Username = '" + Username + "' AND Password = '" + Password + "';",connection);
            SQLiteDataReader reader = command.ExecuteReader();
            return reader.HasRows;
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


