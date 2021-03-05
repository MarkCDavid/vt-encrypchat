using MongoDB.Bson.Serialization.Attributes;

namespace vt_encrypchat.Data.Entity
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string GpgKey { get; set; }
    }
}