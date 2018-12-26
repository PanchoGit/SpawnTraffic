using System.Collections.Generic;
using System.Linq;
using Moq;
using SpawnTraffic.Common.Domains;
using SpawnTraffic.Logger;
using SpawnTraffic.LoggerManager.Interfaces;
using Xunit;
using SpawnTrafficLoggerManager = SpawnTraffic.LoggerManager.LoggerManager;

namespace SpawnTraffic.UnitTest.LoggerManager
{
    public class LoggerManagerTest
    {
        private readonly Mock<ILogger> loggerOneMock;

        private readonly Mock<ILogger> loggerTwoMock;

        private readonly Mock<ILogger> loggerThreeMock;

        private readonly Mock<IPluginLoggerManager> pluginLoggerManagerMock;

        private SpawnTrafficLoggerManager sut;

        public LoggerManagerTest()
        {
            loggerOneMock = new Mock<ILogger>();

            loggerTwoMock = new Mock<ILogger>();

            loggerThreeMock = new Mock<ILogger>();

            loggerOneMock.Setup(s => s.Log(It.IsAny<string>(), It.IsAny<MessageType>()))
                .Returns(new Result());

            loggerTwoMock.Setup(s => s.Log(It.IsAny<string>(), It.IsAny<MessageType>()))
                .Returns(new Result());

            loggerThreeMock.Setup(s => s.Log(It.IsAny<string>(), It.IsAny<MessageType>()))
                .Returns(new Result());

            pluginLoggerManagerMock = new Mock<IPluginLoggerManager>();
            pluginLoggerManagerMock.Setup(s => s.GetAvailableLoggers())
                .Returns(new List<ILogger>
                {
                    loggerOneMock.Object,
                    loggerTwoMock.Object,
                    loggerThreeMock.Object
                });

            sut = new SpawnTrafficLoggerManager(pluginLoggerManagerMock.Object);
        }

        [Fact]
        public void ShouldPassLog()
        {
            pluginLoggerManagerMock.Setup(s => s.GetAvailableLoggers())
                .Returns(new List<ILogger>
                {
                    loggerOneMock.Object,
                    loggerTwoMock.Object
                });

            sut = new SpawnTrafficLoggerManager(pluginLoggerManagerMock.Object);

            var resultActual = sut.LogSuccess("MessageOne");

            Assert.False(resultActual.HasErrors);

            loggerOneMock.Verify(s => s.Log(It.IsAny<string>(), It.IsAny<MessageType>()),
                Times.Once);

            loggerTwoMock.Verify(s => s.Log(It.IsAny<string>(), It.IsAny<MessageType>()),
                Times.Once);

            loggerThreeMock.Verify(s => s.Log(It.IsAny<string>(), It.IsAny<MessageType>()),
                Times.Never);
        }

        [Fact]
        public void ShouldFailLog()
        {
            const string errorMessage = "Error1";

            pluginLoggerManagerMock.Setup(s => s.GetAvailableLoggers())
                .Returns(new List<ILogger>
                {
                    loggerOneMock.Object,
                    loggerTwoMock.Object
                });

            var errorResult = new Result();

            errorResult.AddError(errorMessage);

            loggerTwoMock.Setup(s => s.Log(It.IsAny<string>(), It.IsAny<MessageType>()))
                .Returns(errorResult);

            sut = new SpawnTrafficLoggerManager(pluginLoggerManagerMock.Object);

            var resultActual = sut.LogSuccess("MessageOne");

            Assert.True(resultActual.HasErrors);

            var errorResultActual = resultActual.Messages.First();

            Assert.Equal(ResultMessageType.Error, errorResultActual.type);

            Assert.Equal(errorMessage, errorResultActual.Message);

            loggerOneMock.Verify(s => s.Log(It.IsAny<string>(), It.IsAny<MessageType>()),
                Times.Once);

            loggerTwoMock.Verify(s => s.Log(It.IsAny<string>(), It.IsAny<MessageType>()),
                Times.Once);

            loggerThreeMock.Verify(s => s.Log(It.IsAny<string>(), It.IsAny<MessageType>()),
                Times.Never);
        }
    }
}
