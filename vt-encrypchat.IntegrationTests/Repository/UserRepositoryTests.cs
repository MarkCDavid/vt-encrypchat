using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using NUnit.Framework;
using vt_encrypchat.Data.Contracts.MongoDB;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Data.Repository;
using vt_encrypchat.Domain.Entity;

namespace vt_encrypchat.IntegrationTests.Repository
{
    [TestFixture]
    public class UserRepositoryTests: MongoIntegrationTest
    {
        private IMongoContext _mongoContext;
        private IUserRepository _userRepository; 
        
        [OneTimeSetUp]
        public void ClassInit()
        {
            _mongoContext = CreateMongoContext();
            _userRepository = new UserRepository(_mongoContext);
        }
        
        [Test]
        public async Task ShouldHaveSomething()
        {
            var collection = _mongoContext.DefaultDatabase.GetCollection<User>("users");
            collection.InsertOne(new User()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Username = "aaaaaa",
                Password = "aaaaaa",
                DisplayName = "aaaaaa",
                GpgKeys = new List<GpgKey>()
            });
            var response = await _userRepository.GetAll();
            Assert.True(response.Any());
        }
       
        
    }
}