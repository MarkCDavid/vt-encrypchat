using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Domain.Entity;

namespace vt_encrypchat.Operations.Contracts.User
{
    class UpdateUserOperation : IUpdateUserOperation
    {
        private readonly ILogger<UpdateUserOperation> _logger;
        private readonly IUserRepository _userRepository;

        public UpdateUserOperation(
            ILogger<UpdateUserOperation> logger,
            IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }
        
        public async Task Execute(UpdateUserRequest request)
        {
            Domain.Entity.User user = await _userRepository.Get(request.Id);

            if (request.DisplayName != null)
            {
                user.DisplayName = request.DisplayName;
            }

            if (request.GpgKey != null)
            {
                user.GpgKeys.Add(GpgKey.Create(request.GpgKey));
            }

            await _userRepository.Update(user);
        }
    }
}