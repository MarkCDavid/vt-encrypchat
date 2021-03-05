using MongoDB.Bson.Serialization.Attributes;

namespace vt_encrypchat.Data.Entity
{
    public abstract class BaseEntity
    {
        [BsonId]
        public int Id { get; }
    }
}