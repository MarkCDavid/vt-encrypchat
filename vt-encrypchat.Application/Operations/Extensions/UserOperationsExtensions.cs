namespace vt_encrypchat.Application.Operations.User.Extensions
{
    public static class UserOperationsExtensions
    {
        public static string GenerateSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(10);
        }
        
        public static string Hash(string key, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(key, salt);
        }
    }
}