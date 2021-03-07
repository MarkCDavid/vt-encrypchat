using vt_encrypchat.Data.Contracts.MongoDB;
using vt_encrypchat.Data.Repository;

namespace vt_encrypchat.IntegrationTests.Repository
{
    public class DummyRepository : BaseRepository<DummyEntity>
    {
        public DummyRepository(IMongoContext mongoContext) : base(mongoContext, "dummyCollection")
        {
                
        }
    }
}