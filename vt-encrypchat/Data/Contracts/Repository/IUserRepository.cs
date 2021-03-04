using System.Collections.Generic;
using vt_encrypchat.Data.Entity;

namespace vt_encrypchat.Data.Contracts.Repository
{
    public interface IUserRepository
    {
        void SaveUser(User user);
        
        IList<User> GetUsers(string name = "");
        User GetUser(int id);
    }
}