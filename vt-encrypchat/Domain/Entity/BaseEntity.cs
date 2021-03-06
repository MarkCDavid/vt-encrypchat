using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace vt_encrypchat.Domain.Entity
{
    public abstract class BaseEntity
    {
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))] public string Id { get; set; }
    }
}