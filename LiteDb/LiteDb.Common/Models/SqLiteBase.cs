﻿using System;
using System.IO;
using SQLite;

namespace LiteDb.Common.Models
{
    public class SqLiteBase : IDisposable
    {
        private const string databaseName = "dbSqLiteNetPcl.db";

        public SQLiteAsyncConnection db;

        public SqLiteBase(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath ?? GetDatabasePath());
        }

        public void Dispose()
        {
            db.CloseAsync();
        }

        private string GetDatabasePath()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            return Path.Combine(documentsPath, databaseName);
        }
    }
}
