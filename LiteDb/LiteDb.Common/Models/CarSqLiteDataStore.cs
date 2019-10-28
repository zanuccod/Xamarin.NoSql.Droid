using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;
using LiteDb.Common.Entities;
using LiteDb.Common.Models;

namespace SqLitePcl.Common.Models
{
    public class CarSqLiteDataStore : IDataStore<Car>
    {
        private readonly string dbPath;

        public CarSqLiteDataStore(string dbPath = null)
        {
            this.dbPath = dbPath;

            using (var conn = new SqLiteBase(dbPath))
            {
                // create table if not exist
                conn.db.CreateTableAsync<Car>();
            }
        }

        public async Task AddItem(Car item)
        {
            using (var conn = new SqLiteBase(dbPath))
            {
                await conn.db.InsertAsync(item).ConfigureAwait(false);
            }
        }

        public async Task UpdateItemAsync(Car item)
        {
            using (var conn = new SqLiteBase(dbPath))
            {
                await conn.db.UpdateAsync(item).ConfigureAwait(false);
            }
        }

        public async Task DeleteItemAsync(Car item)
        {
            using (var conn = new SqLiteBase(dbPath))
            {
                await conn.db.DeleteAsync(item).ConfigureAwait(false);
            }
        }

        public async Task<Car> GetItemAsync(Car item)
        {
            using (var conn = new SqLiteBase(dbPath))
            {
                return await conn.db.Table<Car>().FirstOrDefaultAsync(x => x.Id == item.Id).ConfigureAwait(false);
            }
        }

        public async Task<List<Car>> GetItemsAsync()
        {
            using (var conn = new SqLiteBase(dbPath))
            {
                return await conn.db.Table<Car>().OrderByDescending(x => x.Id).ToListAsync().ConfigureAwait(false);
            }
        }

        public async Task DeleteAllAsync()
        {
            using (var conn = new SqLiteBase(dbPath))
            {
                await conn.db.DeleteAllAsync<Car>().ConfigureAwait(false);
            }
        }
    }
}
