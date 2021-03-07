using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using NUnit.Framework;
using vt_encrypchat.Data.Contracts.MongoDB;

namespace vt_encrypchat.IntegrationTests.Repository
{
    [TestFixture]
    public class BaseRepositoryTests: MongoIntegrationTest
    {
        private IMongoContext _mongoContext;
        private DummyRepository _dummyRepository; 
        private IMongoCollection<DummyEntity> _mongoCollection;
        
        [OneTimeSetUp]
        public void ClassInit()
        {
            _mongoContext = CreateMongoContext();
            _dummyRepository = new DummyRepository(_mongoContext);
            _mongoCollection = _dummyRepository.Collection;
        }
        
        [Test]
        public async Task Get_ShouldReturnNull_If_DocumentDoesNotExist()
        {
            var result = await _dummyRepository.Get("_non_existent_");
            Assert.IsNull(result);
        }
        
        [Test]
        public async Task Get_ShouldReturnDocument_If_DocumentDoesExists()
        {
            await _mongoCollection.InsertOneAsync(new DummyEntity { Id = "exists"});
            var result = await _dummyRepository.Get("exists");
            Assert.NotNull(result);
        }
        
        [Test]
        public async Task GetAll_NoFilter_ShouldReturnEmptyDocumentCollection_If_NoDocumentsExist()
        {
            var result = await _dummyRepository.GetAll();
            Assert.False(result.Any());
        }
        
        [Test]
        public async Task GetAll_NoFilter_ShouldReturnDocumentCollection_If_DocumentsExist()
        {
            await _mongoCollection.InsertOneAsync(new DummyEntity { Id = "exists" });
            var result = await _dummyRepository.GetAll();
            Assert.True(result.Any());
        }
        
        [Test]
        public async Task GetAll_WithFilter_ShouldReturnEmptyDocumentCollection_If_NoDocumentsMatchFilter()
        {
            await _mongoCollection.InsertOneAsync(new DummyEntity { Id = "exists" });
            var filter = Builders<DummyEntity>
                .Filter.Eq(entity => entity.Id, "_non_existent_");
            var result = await _dummyRepository.GetAll(filter);
            Assert.False(result.Any());
        }
        
        [Test]
        public async Task GetAll_WithFilter_ShouldReturnDocumentCollection_If_DocumentsMatchFilter()
        {
            await _mongoCollection.InsertOneAsync(new DummyEntity { Id = "exists" });
            var filter = Builders<DummyEntity>
                .Filter.Eq(entity => entity.Id, "exists");
            var result = await _dummyRepository.GetAll(filter);
            Assert.True(result.Any());
        }
        
        [Test]
        public async Task Create_ShouldCreateDocument()
        {
            var entity = new DummyEntity();
            var before = await _mongoCollection.CountDocumentsAsync(FilterDefinition<DummyEntity>.Empty);
            await _dummyRepository.Create(entity);
            var after = await _mongoCollection.CountDocumentsAsync(FilterDefinition<DummyEntity>.Empty);
            
            Assert.Less(before, after);
        }
        
        [Test]
        public async Task Create_ShouldCreateDocumentId()
        {
            var entity = new DummyEntity();
            await _dummyRepository.Create(entity);
            var cursor = await _mongoCollection.FindAsync(FilterDefinition<DummyEntity>.Empty);
            var document = cursor.FirstOrDefault();
            
            Assert.NotNull(document);
            Assert.NotNull(document.Id);
        }
        
        [Test]
        public async Task Update_ShouldUpdateDocument()
        {
            const string initialValue = "initialValue";
            const string updatedValue = "updatedValue";

            var entity = new DummyEntity { Value = initialValue };
            await _dummyRepository.Create(entity);
            
            var initialCursor = await _mongoCollection.FindAsync(FilterDefinition<DummyEntity>.Empty);
            var initialDocument = initialCursor.FirstOrDefault();

            initialDocument.Value = updatedValue;
            await _dummyRepository.Update(initialDocument);
            
            var updatedCursor = await _mongoCollection.FindAsync(FilterDefinition<DummyEntity>.Empty);
            var updatedDocument = updatedCursor.FirstOrDefault();
            
            Assert.NotNull(updatedDocument);
            Assert.AreNotEqual(updatedDocument.Value, initialValue);
            Assert.AreEqual(updatedDocument.Value, updatedValue);
        }
        
        [Test]
        public async Task Delete_ShouldDeleteDocument()
        {
            var entity = new DummyEntity();
            await _dummyRepository.Create(entity);
            
            var cursor = await _mongoCollection.FindAsync(FilterDefinition<DummyEntity>.Empty);
            var document = cursor.FirstOrDefault();
            
            await _dummyRepository.Delete(document);

            var result = await _dummyRepository.GetAll();
            Assert.False(result.Any());
        }
    }
}