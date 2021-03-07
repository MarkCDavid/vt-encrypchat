using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using vt_encrypchat.Application.Operations.Contracts.Messages;
using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Application.Operations.Messages;
using vt_encrypchat.Application.Operations.User;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Domain.Entity;

namespace vt_encrypchat.UnitTests.Application.Operations.User
{
    [TestFixture]
    public class CheckUserCredentialValidityOperationTests
    {
        [OneTimeSetUp]
        public void ClassInit()
        {
            _logger = new Mock<ILogger<GetUserExistsOperation>>().Object;
        }

        private ILogger<GetUserExistsOperation> _logger;


        [Test]
        public async Task ShouldRespondWithValidIfUserCredentialsMatch()
        {
            const string username = "Username";
            const string password = "Password";
            
            var user = new Domain.Entity.User()
            {
                Username = username,
                Password = password
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repository => repository.GetUserByUsername(It.IsAny<string>()))
                .Returns(() => Task.FromResult(user));

            var operation =
                new CheckUserCredentialValidityOperation(_logger, userRepositoryMock.Object);

            var request = new CheckUserCredentialValidityRequest
            {
                Username = username,
                Password = password
            };

            var response = await operation.Execute(request);
            
            Assert.True(response.Valid);
        }

        [Test]
        public async Task ShouldRespondWithInvalidIfUserCredentialsDoNotMatch()
        {
            const string username = "Username";
            const string password = "Password";
            const string invalidPassword = "InvalidPassword";
            
            var user = new Domain.Entity.User()
            {
                Username = username,
                Password = password
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repository => repository.GetUserByUsername(It.IsAny<string>()))
                .Returns(() => Task.FromResult(user));

            var operation =
                new CheckUserCredentialValidityOperation(_logger, userRepositoryMock.Object);

            var request = new CheckUserCredentialValidityRequest
            {
                Username = username,
                Password = invalidPassword
            };

            var response = await operation.Execute(request);

            Assert.False(response.Valid);
        }
        

        [Test]
        public async Task ShouldRespondWithInvalidIfUserWithThisUsernameDoesNotExist()
        {
            const string username = "Username";
            const string password = "Password";

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repository => repository.GetUserByUsername(It.IsAny<string>()))
                .Returns(() => Task.FromResult(default(Domain.Entity.User)));

            var operation =
                new CheckUserCredentialValidityOperation(_logger, userRepositoryMock.Object);

            var request = new CheckUserCredentialValidityRequest
            {
                Username = username,
                Password = password
            };

            var response = await operation.Execute(request);

            Assert.False(response.Valid);
        }
    }
}