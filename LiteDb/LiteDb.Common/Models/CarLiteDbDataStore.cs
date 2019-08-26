using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using LiteDb.Common.Entities;
using LiteDB;

namespace LiteDb.Common.Models
{
    public class CarLiteDbDataStore : LiteDbBase, IDataStore<Car>
    {
        private readonly LiteCollection<Car> cars;

        public CarLiteDbDataStore(string dbPath)
            : base(dbPath)
        {
            // create table if not exist
            cars = db.GetCollection<Car>();

            // create index on Id key
            cars.EnsureIndex("Id");
        }

        public async Task AddItem(Car item)
        {
            await Task.FromResult(cars.Insert(item)).ConfigureAwait(false);
        }

        public async Task DeleteItemAsync(Car item)
        {
            await Task.FromResult(cars.Delete(item.Id)).ConfigureAwait(false);
        }

        public async Task<Car> GetItemAsync(Car item)
        {
            return await Task.FromResult(cars.FindById(item.Id)).ConfigureAwait(false);
        }

        public async Task<List<Car>> GetItemsAsync()
        {
            return await Task.FromResult(cars.FindAll().ToList()).ConfigureAwait(false);
        }

        public async Task UpdateItemAsync(Car item)
        {
            await Task.FromResult(cars.Update(item)).ConfigureAwait(false);
        }

        public async Task DeleteAllAsync()
        {
            await Task.FromResult(cars.Delete(Query.All())).ConfigureAwait(false);
        }
    }
}
