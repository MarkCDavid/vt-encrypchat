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
    public class GetUserByIdOperationTests
    {
        [OneTimeSetUp]
        public void ClassInit()
        {
            _logger = new Mock<ILogger<GetUserByIdOperation>>().Object;
        }

        private ILogger<GetUserByIdOperation> _logger;

        [Test]
        public async Task ShouldReturnUserIfUserWithIdExists()
        {
            var dummyUser = new Domain.Entity.User();

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>()))
                .Returns(() => Task.FromResult(dummyUser));

            var operation = new GetUserByIdOperation(_logger, userRepositoryMock.Object);

            var request = new GetUserByIdRequest() {Id = "id"};

            var response = await operation.Execute(request);
            
            Assert.NotNull(response.User);
        }

        [Test]
        public async Task ShouldReturnNullIfUserWithIdDoesNotExist()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>()))
                .Returns(() => Task.FromResult(default(Domain.Entity.User)));

            var operation = new GetUserByIdOperation(_logger, userRepositoryMock.Object);

            var request = new GetUserByIdRequest { Id = "id" };

            var response = await operation.Execute(request);

            Assert.IsNull(response.User);
        }
    }
}