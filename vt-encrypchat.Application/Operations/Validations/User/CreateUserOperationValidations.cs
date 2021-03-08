using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Application.Operations.Exceptions;

namespace vt_encrypchat.Application.Operations.Validations.User
{
    public static class CreateUserOperationValidations
    {
        public const int UsernameMinLength = 6;
        public const int UsernameMaxLength = 32;

        public static void ValidateRequest(CreateUserRequest request)
        {
            if (request.Username.Length < UsernameMinLength)
            {
                throw new OperationException(
                    $"Username too short. Expected {UsernameMinLength}, got {request.Username.Length}");
            }
            
            if (request.Username.Length > UsernameMaxLength)
            {
                throw new OperationException(
                    $"Username too long. Expected {UsernameMaxLength}, got {request.Username.Length}");
            }
        }
    }
}