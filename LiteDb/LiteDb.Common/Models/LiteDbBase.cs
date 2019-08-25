using System;
using System.IO;
using LiteDb.Common.Entities;
using LiteDB;

namespace LiteDb.Common.Models
{
    public abstract class LiteDbBase : IDisposable
    {
        private const string databaseName = "dbLiteDb.db";
        private readonly LiteDatabase db;

        protected LiteDbBase(string dbPath)
        {
            db = new LiteDatabase(dbPath ?? GetDatabasePath());
            Init();
        }

        #region Public Methods

        public void Dispose()
        {
            db.Dispose();
        }

        #endregion

        #region Protected Methods

        protected LiteCollection<Car> Cars { get; private set; }

        #endregion

        #region Private Methods

        private void Init()
        {
            // create table if not exist
            Cars = db.GetCollection<Car>();
        }

        private string GetDatabasePath()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            return Path.Combine(documentsPath, databaseName);
        }

        #endregion
    }
}
