using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using vt_encrypchat.Application.Operations.Contracts.User;
using vt_encrypchat.Application.Operations.User;
using vt_encrypchat.Data.Contracts.Repository;

namespace vt_encrypchat.UnitTests.Application.Operations.User
{
    [TestFixture]
    public class UpdateUserOperationTests
    {
        [OneTimeSetUp]
        public void ClassInit()
        {
            _logger = new Mock<ILogger<UpdateUserSettingsSettingsOperation>>().Object;
        }

        private ILogger<UpdateUserSettingsSettingsOperation> _logger;

        [Test]
        public async Task ShouldUpdateUserValuesIfRequestUpdateValuesPresent()
        {
            const string displayName = "DisplayName";
            const string newDisplayName = "NewDisplayName";
            const string newGpgKey = "NewGpgKey";

            var user = new Domain.Entity.User
            {
                DisplayName = displayName,
                GpgKeys = null
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>()))
                .Returns(() => Task.FromResult(user));

            var operation = new UpdateUserSettingsSettingsOperation(_logger, userRepositoryMock.Object);

            var request = new UpdateUseSettingsRequest
            {
                Id = "id",
                DisplayName = newDisplayName,
                GpgKey = newGpgKey
            };

            await operation.Execute(request);

            Assert.NotNull(user.GpgKeys);
            Assert.AreEqual(newDisplayName, user.DisplayName);
            Assert.AreEqual(newGpgKey, user.GpgKeys.OrderByDescending(key => key.Date).First().Value);
        }

        [Test]
        public async Task ShouldNotUpdateUserValuesIfRequestUpdateValuesAreNotPresent()
        {
            const string displayName = "DisplayName";

            var user = new Domain.Entity.User
            {
                DisplayName = displayName,
                GpgKeys = null
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>()))
                .Returns(() => Task.FromResult(user));

            var operation = new UpdateUserSettingsSettingsOperation(_logger, userRepositoryMock.Object);

            var request = new UpdateUseSettingsRequest {Id = "id"};

            await operation.Execute(request);

            Assert.IsNull(user.GpgKeys);
            Assert.AreEqual(displayName, user.DisplayName);
        }
    }
}