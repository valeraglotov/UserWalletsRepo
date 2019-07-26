using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(string id);
        Task<bool> Create(T item, string password);
        Task<bool> Update(T item);
        Task<bool> Remove(string id);
        //Task<bool> IsUserWallet(Guid idId, int idUserId);
    }
}
