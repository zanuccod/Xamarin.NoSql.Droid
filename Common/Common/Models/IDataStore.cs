using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Models
{
    public interface IDataStore<T> : IDisposable
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(T item);
        Task<T> GetItemAsync(long item);
        Task<List<T>> GetItemsAsync();
        Task<bool> DeleteAllAsync();
    }
}
