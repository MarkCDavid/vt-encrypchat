using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using vt_encrypchat.Application.Operations.Contracts.Messages;
using vt_encrypchat.Application.Operations.Messages;
using vt_encrypchat.Data.Contracts.Repository;
using vt_encrypchat.Domain.Entity;

namespace vt_encrypchat.UnitTests.Application.Operations.Messages
{
    [TestFixture]
    public class SendUserMessagesOperationTests
    {
        [OneTimeSetUp]
        public void ClassInit()
        {
            _logger = new Mock<ILogger<SendUserMessageOperation>>().Object;
        }

        private ILogger<SendUserMessageOperation> _logger;

        [Test]
        public async Task ShouldCreateMessageIfValidRequest()
        {
            var dummyUser = new Domain.Entity.User();

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>()))
                .Returns(() => Task.FromResult(dummyUser));

            var messageRepositoryMock = new Mock<IMessageRepository>();
            messageRepositoryMock.Setup(repository => repository.Create(It.IsAny<Message>()));

            var operation =
                new SendUserMessageOperation(_logger, userRepositoryMock.Object, messageRepositoryMock.Object);

            var request = new SendUserMessageRequest()
            {
                FromValue = string.Empty,
                ToValue = string.Empty,
                Time = DateTime.Now,
                From = string.Empty,
                To = string.Empty,
            };

            await operation.Execute(request);
            
            messageRepositoryMock.Verify(repository => repository.Create(It.IsAny<Message>()), Times.Once);
        }
        
        [Test]
        public async Task ShouldNotCreateMessageIfInvalidRequest()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>()))
                .Returns(() => Task.FromResult(default(Domain.Entity.User)));

            var messageRepositoryMock = new Mock<IMessageRepository>();
            messageRepositoryMock.Setup(repository => repository.Create(It.IsAny<Message>()));

            var operation =
                new SendUserMessageOperation(_logger, userRepositoryMock.Object, messageRepositoryMock.Object);

            var request = new SendUserMessageRequest()
            {
                FromValue = string.Empty,
                ToValue = string.Empty,
                Time = DateTime.Now,
                From = string.Empty,
                To = string.Empty,
            };

            await operation.Execute(request);
            
            messageRepositoryMock.Verify(repository => repository.Create(It.IsAny<Message>()), Times.Never);
        }
    }
}