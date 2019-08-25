using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Models
{
    public interface IDataStore<T> : IDisposable
    {
        Task AddItem(T item);
        Task UpdateItemAsync(T item);
        Task DeleteItemAsync(T item);
        Task<T> GetItemAsync(T item);
        Task<List<T>> GetItemsAsync();
        Task DeleteAllAsync();
    }
}
