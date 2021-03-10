using Microsoft.Extensions.DependencyInjection;
using vt_encrypchat.Application.Operations.Contracts.Messages;
using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Application.Operations.Messages;
using vt_encrypchat.Application.Operations.User;

namespace vt_encrypchat.Infrastructure.IoC
{
    public static class Operations
    {
        public static void AddOperations(this IServiceCollection services)
        {
            services.AddScoped<ICheckUserCredentialValidityOperation, CheckUserCredentialValidityOperation>();
            services.AddScoped<ICreateUserOperation, CreateUserOperation>();
            services.AddScoped<ISearchUserByDisplayNameOperation, SearchUserByDisplayNameOperation>();
            services.AddScoped<IGetUserByIdOperation, GetUserByIdOperation>();
            services.AddScoped<IUpdateUserSettingsOperation, UpdateUserSettingsSettingsOperation>();
            services.AddScoped<IGetUserByUsernameOperation, GetUserByUsernameOperation>();
            services.AddScoped<IGetUserMessagesOperation, GetUserMessagesOperation>();
            services.AddScoped<ISendUserMessageOperation, SendUserMessageOperation>();
            services.AddScoped<IGetUserSettingsOperation, GetUserSettingsSettingsOperation>();
        }
    }
}