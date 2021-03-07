using Microsoft.Extensions.Options;
using Mongo2Go;
using Moq;
using vt_encrypchat.Data.Configuration;
using vt_encrypchat.Data.Contracts.MongoDB;
using vt_encrypchat.Data.MongoDB;

namespace vt_encrypchat.IntegrationTests
{
    public class MongoIntegrationTest
    {
        private static MongoDbRunner _runner;

        internal static IMongoContext CreateMongoContext()
        {
            _runner = MongoDbRunner.Start();

            var optionsMonitorMock = new Mock<IOptionsMonitor<MongoDbConfig>>();
            optionsMonitorMock.SetupGet(monitor => monitor.CurrentValue).Returns(() =>
                new MongoDbConfig
                {
                    ConnectionString = _runner.ConnectionString,
                    DefaultDatabase = "IntegrationTest"
                });

            return new MongoContext(optionsMonitorMock.Object);
        }
    }
}