using MongoDB.Bson.Serialization.Attributes;

namespace vt_encrypchat.Domain.Entity
{
    public abstract class BaseEntity
    {
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        [BsonId] public int Id { get; }
    }
}