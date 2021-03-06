using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using vt_encrypchat.Data.Contracts.MongoDB;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Data.Entity;

namespace vt_encrypchat.Data.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T: BaseEntity
    {
        protected IMongoCollection<T> Collection { get; }
        
        protected BaseRepository(IMongoContext mongoContext, string collectionName)
        {
            Collection = mongoContext.DefaultDatabase.GetCollection<T>(collectionName);
        }

        public async Task<IEnumerable<T>> GetAll(FilterDefinition<T> filter = null)
        {
            var cursor = await Collection.FindAsync(filter ?? FilterDefinition<T>.Empty);
            return await cursor.ToListAsync();
        }
        
        public async Task<T> Get(FilterDefinition<T> filter)
        {
            var cursor = await Collection.FindAsync(filter);
            return await cursor.FirstOrDefaultAsync();
        }
        
        public async Task<T> Get(int id)
        {
            var cursor = await Collection.FindAsync(t => t.Id == id);
            return await cursor.FirstOrDefaultAsync();
        }
        
        public async Task Create(T entity)
        {
            await Collection.InsertOneAsync(entity);
        }
        
        public async Task Update(T entity)
        {
            await Collection.FindOneAndReplaceAsync(t => t.Id == entity.Id, entity);
        }

        public async Task Delete(T entity)
        {
            await Collection.DeleteOneAsync(t => t.Id == entity.Id);
        }
        
    }
}