using System.Collections.Generic;
using System.Threading.Tasks;

namespace vt_encrypchat.Operations.Contracts.User
{
    public interface ISearchUserByDisplayNameOperation
    {
        Task<SearchUserByDisplayNameResponse> Execute(SearchUserByDisplayNameRequest request);
    }

    public class SearchUserByDisplayNameRequest
    {
        public string DisplayName { get; set; }
    }
    
    public class SearchUserByDisplayNameResponse
    {
        public IEnumerable<User> Users { get; set; }

        public class User
        {
            public string Id { get; set; }
            public string DisplayName { get; set; }
            public string GpgKey { get; set; }
        }
    }
}