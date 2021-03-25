using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SearchUserByDisplayNameOperationTests
    {
        [OneTimeSetUp]
        public void ClassInit()
        {
            _logger = new Mock<ILogger<SearchUserByDisplayNameOperation>>().Object;
        }

        private ILogger<SearchUserByDisplayNameOperation> _logger;

        [Test]
        public async Task ShouldReturnEmptyCollectionIfNoUsersMatch()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repository => repository.GetUsers(It.IsAny<string>()))
                .ReturnsAsync(new List<Domain.Entity.User>());

            var operation = new SearchUserByDisplayNameOperation(_logger, userRepositoryMock.Object);

            var request = new SearchUserByDisplayNameRequest { Search = "displayName" };

            var response = await operation.Execute(request);

            Assert.False(response.Users.Any());
        }

        [Test]
        public async Task ShouldReturnNonEmptyCollectionIfSomeUsersMatch()
        {
            var dummyUser = new Domain.Entity.User();

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repository => repository.GetUsers(It.IsAny<string>()))
                .ReturnsAsync(new List<Domain.Entity.User> { dummyUser });

            var operation = new SearchUserByDisplayNameOperation(_logger, userRepositoryMock.Object);

            var request = new SearchUserByDisplayNameRequest { Search = "displayName" };

            var response = await operation.Execute(request);

            Assert.True(response.Users.Any());
        }

       
    }
}