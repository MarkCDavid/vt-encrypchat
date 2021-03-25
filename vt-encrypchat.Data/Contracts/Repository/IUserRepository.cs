using System.Collections.Generic;
using System.Threading.Tasks;
using vt_encrypchat.Domain.Entity;

namespace vt_encrypchat.Data.Contracts.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByUsername(string name);
        Task<IEnumerable<User>> GetUsers(string search = "");
    }
}