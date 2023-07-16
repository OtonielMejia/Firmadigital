using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FirmaDigitalSqLite.models
{
    public class DatabaseService
    {
        private SQLiteConnection connection;

        public DatabaseService()
        {
            string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FirmaDigital.db");
            connection = new SQLiteConnection(databasePath);
            connection.CreateTable<FirmaDigital>();
        }

        public List<FirmaDigital> GetFirmas()
        {
            return connection.Table<FirmaDigital>().ToList();
        }

        public void InsertFirma(FirmaDigital firma)
        {
            connection.Insert(firma);
        }
    }
}
