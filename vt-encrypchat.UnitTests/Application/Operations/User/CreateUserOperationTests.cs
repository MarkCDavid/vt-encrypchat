using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Application.Operations.Exceptions;
using vt_encrypchat.Application.Operations.User;
using vt_encrypchat.Data.Contracts.Repository;

namespace vt_encrypchat.UnitTests.Application.Operations.User
{
    [TestFixture]
    public class CreateUserOperationTests
    {
        [OneTimeSetUp]
        public void ClassInit()
        {
            _logger = new Mock<ILogger<CreateUserOperation>>().Object;
        }

        private ILogger<CreateUserOperation> _logger;

        [Test]
        public async Task ShouldCreateUserIfValidRequest()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(repository => repository.GetUserByUsername(It.IsAny<string>()))
                .Returns(() => Task.FromResult(default(Domain.Entity.User)));
            userRepositoryMock.Setup(repository => repository.Create(It.IsAny<Domain.Entity.User>()));

            var operation =
                new CreateUserOperation(_logger, userRepositoryMock.Object);

            var request = new CreateUserRequest
            {
                Username = "Username",
                Password = "Password"
            };

            await operation.Execute(request);

            userRepositoryMock.Verify(repository => repository.Create(It.IsAny<Domain.Entity.User>()), Times.Once);
        }

        [Test]
        public void ShouldThrowIfInvalidRequest()
        {
            const string username = "Username";
            const string password = "Password";

            var user = new Domain.Entity.User
            {
                Username = username,
                Password = password
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repository => repository.GetUserByUsername(It.IsAny<string>()))
                .Returns(() => Task.FromResult(user));
            userRepositoryMock.Setup(repository => repository.Create(It.IsAny<Domain.Entity.User>()));

            var operation =
                new CreateUserOperation(_logger, userRepositoryMock.Object);

            var request = new CreateUserRequest
            {
                Username = username,
                Password = password
            };

            Assert.ThrowsAsync<OperationException>(async () => await operation.Execute(request));
        }
    }
}