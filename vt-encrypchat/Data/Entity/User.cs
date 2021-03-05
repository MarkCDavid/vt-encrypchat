using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace vt_encrypchat.Data.Entity
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public List<GpgKey> GpgKeys { get; set; }
    }
    
    public class GpgKey
    {
        public string Value { get; set; }
        public DateTime Date { get; set; }
    }
}