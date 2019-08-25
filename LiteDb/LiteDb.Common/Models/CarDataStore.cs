using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using LiteDb.Common.Entities;
using LiteDB;

namespace LiteDb.Common.Models
{
    public class CarDataStore : LiteDbBase, IDataStore<Car>
    {
        public CarDataStore(string dbPath)
            : base(dbPath)
        { }

        public async Task AddItem(Car item)
        {
            await Task.FromResult(Cars.Insert(item)).ConfigureAwait(false);
        }

        public async Task DeleteItemAsync(Car item)
        {
            await Task.FromResult(Cars.Delete(item.Id)).ConfigureAwait(false);
        }

        public async Task<Car> GetItemAsync(Car item)
        {
            return await Task.FromResult(Cars.FindById(item.Id)).ConfigureAwait(false);
        }

        public async Task<List<Car>> GetItemsAsync()
        {
            return await Task.FromResult(Cars.FindAll().ToList()).ConfigureAwait(false);
        }

        public async Task UpdateItemAsync(Car item)
        {
            await Task.FromResult(Cars.Update(item)).ConfigureAwait(false);
        }

        public async Task DeleteAllAsync()
        {
            await Task.FromResult(Cars.Delete(Query.All())).ConfigureAwait(false);
        }
    }
}
