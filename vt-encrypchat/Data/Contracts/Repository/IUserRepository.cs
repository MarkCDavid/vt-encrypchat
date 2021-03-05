using System.Collections.Generic;
using System.Threading.Tasks;
using vt_encrypchat.Data.Entity;

namespace vt_encrypchat.Data.Contracts.Repository
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task<IEnumerable<User>> GetUsers(string name = "");
    }
}