using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using vt_encrypchat.Application.Operations.Contracts.Messages;
using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Application.Operations.Exceptions;
using vt_encrypchat.Application.Operations.Messages;
using vt_encrypchat.Application.Operations.User;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Domain.Entity;

namespace vt_encrypchat.UnitTests.Application.Operations.User
{
    [TestFixture]
    public class GetUserByUsernameOperationTests
    {
        [OneTimeSetUp]
        public void ClassInit()
        {
            _logger = new Mock<ILogger<GetUserByUsernameOperation>>().Object;
        }

        private ILogger<GetUserByUsernameOperation> _logger;

        [Test]
        public async Task ShouldReturnUserIfUserWithUsernameExists()
        {
            var dummyUser = new Domain.Entity.User();

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repository => repository.GetUserByUsername(It.IsAny<string>()))
                .Returns(() => Task.FromResult(dummyUser));

            var operation = new GetUserByUsernameOperation(_logger, userRepositoryMock.Object);

            var request = new GetUserByUsernameRequest() { Username = "username" };

            var response = await operation.Execute(request);
            
            Assert.NotNull(response.User);
        }

        [Test]
        public async Task ShouldReturnNullIfUserWithUsernameDoesNotExist()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repository => repository.GetUserByUsername(It.IsAny<string>()))
                .Returns(() => Task.FromResult(default(Domain.Entity.User)));

            var operation = new GetUserByUsernameOperation(_logger, userRepositoryMock.Object);

            var request = new GetUserByUsernameRequest() { Username = "username" };

            var response = await operation.Execute(request);

            Assert.IsNull(response.User);
        }
    }
}