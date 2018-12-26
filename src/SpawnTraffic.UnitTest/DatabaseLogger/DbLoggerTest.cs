using System;
using Moq;
using SpawnTraffic.DatabaseLogger;
using SpawnTraffic.DatabaseLogger.Domain;
using SpawnTraffic.DatabaseLogger.Repository.Interfaces;
using SpawnTraffic.Logger;
using Xunit;

namespace SpawnTraffic.UnitTest.DatabaseLogger
{
    public class DbLoggerTest
    {
        private readonly ILogger sut;

        private readonly Mock<ILogRepository> logRepositoryMock;

        public DbLoggerTest()
        {
            logRepositoryMock = new Mock<ILogRepository>();

            logRepositoryMock.Setup(s => s.Save(It.IsAny<Log>()));

            sut = new DbLogger(logRepositoryMock.Object);
        }

        [Fact]
        public void ShouldPassSave()
        {
            var actual = sut.Log("MessageOne", MessageType.Success);

            Assert.False(actual.HasErrors);

            logRepositoryMock.Verify(s => s.Save(It.IsAny<Log>()), Times.Once);
        }

        [Fact]
        public void ShouldFailSave()
        {
            logRepositoryMock.Setup(s => s.Save(It.IsAny<Log>())).Throws(new Exception());

            var actual = sut.Log("MessageOne", MessageType.Success);

            Assert.True(actual.HasErrors);

            logRepositoryMock.Verify(s => s.Save(It.IsAny<Log>()), Times.Once);
        }
    }
}
