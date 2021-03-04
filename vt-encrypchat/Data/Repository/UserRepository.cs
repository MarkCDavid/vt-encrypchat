using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using vt_encrypchat.Data.Contracts.MongoDB;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Data.Entity;

namespace vt_encrypchat.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoContext _mongoContext;
        private readonly IMongoCollection<User> _collection;
        
        public UserRepository(IMongoContext mongoContext)
        {
            _mongoContext = mongoContext;
            _collection = mongoContext.DefaultDatabase.GetCollection<User>("users");
        }

        public void SaveUser(User user)
        {
            _collection.InsertOne(user);
        }
        
        public IList<User> GetUsers(string name)
        {
            return _collection.Find(user => true).ToList();
        }

        public User GetUser(int id)
        {
            return _collection.Find(user => user.ID == id).FirstOrDefault();
        }
    }
}