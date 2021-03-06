using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using vt_encrypchat.Domain.Entity;

namespace vt_encrypchat.Data.Contracts.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll(FilterDefinition<T> filter = null);
        Task<T> Get(string id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}