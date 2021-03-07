using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetUserMessagesOperationTests
    {
        [OneTimeSetUp]
        public void ClassInit()
        {
            _logger = new Mock<ILogger<GetUserMessagesOperation>>().Object;
        }

        private ILogger<GetUserMessagesOperation> _logger;

        [Test]
        public async Task ShouldReturnNoMessagesWhenNoMessagesAvailable()
        {
            var messageRepositoryMock = new Mock<IMessageRepository>();

            messageRepositoryMock.Setup(
                repository => repository.GetMessages(It.IsAny<string>(), It.IsAny<int>())
            ).Returns(() => Task.FromResult(Array.Empty<Message>().AsEnumerable()));

            var operation = new GetUserMessagesOperation(_logger, messageRepositoryMock.Object);

            var request = new GetUserMessagesRequest {Id = string.Empty, Count = 10};
            var response = await operation.Execute(request);

            Assert.False(response.Messages.Any());
        }

        [Test]
        public async Task ShouldReturnOneMessageWithMappedProperties()
        {
            var id = "id";
            var message = "hello world";

            MessageUser to = new MessageUser()
            {
                Id = id,
                DisplayName = "me",
                GpgKey = null,
            };
            
            MessageUser from = new MessageUser()
            {
                Id = "someone",
                DisplayName = "someone",
                GpgKey = null,
            };
            
            DateTime time = DateTime.Now;
         
            var messageRepositoryMock = new Mock<IMessageRepository>();

            messageRepositoryMock.Setup(
                repository => repository.GetMessages(
                    It.Is(id, StringComparer.InvariantCultureIgnoreCase),
                    It.IsAny<int>())
            ).Returns(() => Task.FromResult(
                new List<Message>
                {
                    new()
                    {
                        Value = message,
                        Time = time,
                        From = from,
                        To = to,
                    }
                }.AsEnumerable()));

            var operation = new GetUserMessagesOperation(_logger, messageRepositoryMock.Object);
            var request = new GetUserMessagesRequest {Id = id, Count = 10};
            var response = await operation.Execute(request);
            
            Assert.AreEqual(response.Messages.Count(), 1);

            var responseMessage = response.Messages.First();
            
            Assert.AreEqual(responseMessage.Value, message);

            Assert.AreEqual(responseMessage.Time, time);
            
            Assert.AreEqual(responseMessage.From.Id, from.Id);
            Assert.AreEqual(responseMessage.From.DisplayName, from.DisplayName);
            Assert.AreEqual(responseMessage.From.GpgKey, from.GpgKey);
            
            Assert.AreEqual(responseMessage.To.Id, to.Id);
            Assert.AreEqual(responseMessage.To.DisplayName, to.DisplayName);
            Assert.AreEqual(responseMessage.To.GpgKey, to.GpgKey);
        }
    }
}