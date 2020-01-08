using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiteDb.Common.Models
{
    public interface IDataStore<T>
    {
        Task AddItemAsync(T item);
        Task UpdateItemAsync(T item);
        Task DeleteItemAsync(T item);
        Task<T> GetItemAsync(T item);
        Task<List<T>> GetItemsAsync();
        Task DeleteAllAsync();
    }
}
