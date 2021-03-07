using System;
using System.Collections.Generic;

namespace vt_encrypchat.Domain.Entity
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

        public static GpgKey Create(string value)
        {
            return new()
            {
                Value = value,
                Date = DateTime.Now
            };
        }
    }
}