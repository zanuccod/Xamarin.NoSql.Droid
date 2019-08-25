using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;
using LiteDb.Common.Entities;
using LiteDb.Common.Models;

namespace SqLitePcl.Common.Models
{
    public class CarSqLiteDataStore : SqLiteBase, IDataStore<Car>
    {
        public CarSqLiteDataStore(string dbPath = null)
            : base(dbPath)
        {
            // create table if not exist
            db.CreateTableAsync<Car>();
        }

        public async Task AddItem(Car item)
        {
            await db.InsertAsync(item).ConfigureAwait(false);
        }

        public async Task UpdateItemAsync(Car item)
        {
            await db.UpdateAsync(item).ConfigureAwait(false);
        }

        public async Task DeleteItemAsync(Car item)
        {
            await db.DeleteAsync(item).ConfigureAwait(false);
        }

        public async Task<Car> GetItemAsync(Car item)
        {
            return await db.Table<Car>().FirstOrDefaultAsync(x => x.Id == item.Id).ConfigureAwait(false);
        }

        public async Task<List<Car>> GetItemsAsync()
        {
            return await db.Table<Car>().OrderByDescending(x => x.Id).ToListAsync().ConfigureAwait(false);
        }

        public async Task DeleteAllAsync()
        {
            await db.DeleteAllAsync<Car>().ConfigureAwait(false);
        }
    }
}
