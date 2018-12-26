using System;
using System.Collections.Generic;
using AutoMapper;
using Moq;
using SpawnTraffic.Common.Domains;
using SpawnTraffic.DataCache.Interfaces;
using SpawnTraffic.Logger;
using SpawnTraffic.Model;
using SpawnTraffic.Workflow;
using SpawnTraffic.Workflow.Interfaces;
using Xunit;

namespace SpawnTraffic.UnitTest.Workflow
{
    public class SkaterWorkflowTest
    {
        private readonly AddSkaterModel validAddSkaterModel;

        private ISkaterWorkflow sut;

        private readonly Mock<ILoggerManager> loggerManagerMock;

        private readonly Mock<ISkaterData> skaterDataMock;

        private readonly Mock<IMapper> mapperMock;

        public SkaterWorkflowTest()
        {
            loggerManagerMock = new Mock<ILoggerManager>();

            skaterDataMock = new Mock<ISkaterData>();

            skaterDataMock.Setup(s => s.Get()).Returns(new List<SkaterModel>
            {
                new SkaterModel
                {
                    Id = Guid.NewGuid(), Name = "SkaterOne", Brand = "BrandOne", Created = DateTimeOffset.Now
                }
            });

            mapperMock = new Mock<IMapper>();

            sut = new SkaterWorkflow(loggerManagerMock.Object, 
                skaterDataMock.Object,
                mapperMock.Object);

            validAddSkaterModel = new AddSkaterModel
            {
                Name = "One",
                Brand = "BrandOne"
            };

            mapperMock.Setup(s => s.Map<AddSkaterModel, SkaterModel>(validAddSkaterModel))
                .Returns(new SkaterModel
                {
                    Id = Guid.NewGuid(),
                    Name = validAddSkaterModel.Name,
                    Brand = validAddSkaterModel.Brand,
                    Created = DateTimeOffset.Now
                });
        }

        [Fact]
        public void ShouldPassAdd()
        {
            loggerManagerMock.Setup(s => s.LogSuccess(It.IsAny<string>())).Returns(new Result());

            var actual = sut.Add(validAddSkaterModel);

            Assert.False(actual.HasErrors);
        }

        [Fact]
        public void ShouldFailAdd()
        {
            var errorResult = new Result();

            errorResult.AddError("Error1");

            loggerManagerMock.Setup(s => s.LogSuccess(It.IsAny<string>())).Returns(errorResult);

            var addSkaterModel = new AddSkaterModel();

            var actual = sut.Add(addSkaterModel);

            Assert.True(actual.HasErrors);
        }

        [Fact]
        public void ShouldPassGet()
        {
            loggerManagerMock.Setup(s => s.LogSuccess(It.IsAny<string>())).Returns(new Result());

            var actual = sut.Get();

            Assert.False(actual.HasErrors);

            var listCountActual = actual.Data.Count;

            Assert.Equal(1, listCountActual);
        }

        [Fact]
        public void ShouldFailGet()
        {
            var errorResult = new Result();

            errorResult.AddError("Error1");

            loggerManagerMock.Setup(s => s.LogSuccess(It.IsAny<string>())).Returns(errorResult);

            var actual = sut.Get();

            Assert.True(actual.HasErrors);
        }

        [Fact]
        public void ShouldPassInitSkaterModels()
        {
            skaterDataMock.Setup(s => s.Get()).Returns(new List<SkaterModel>());

            loggerManagerMock.Setup(s => s.LogSuccess(It.IsAny<string>())).Returns(new Result());

            sut = new SkaterWorkflow(loggerManagerMock.Object,
                skaterDataMock.Object,
                mapperMock.Object);

            var actual = sut.Get();

            Assert.False(actual.HasErrors);

            skaterDataMock.Verify(s => s.Set(It.IsAny<List<SkaterModel>>()), Times.Once);
        }

        [Fact]
        public void ShouldFailAddWithException()
        {
            var errorResult = new Result();

            errorResult.AddError("Error1");

            skaterDataMock.Setup(s => s.Get()).Throws(new Exception());

            loggerManagerMock.Setup(s => s.LogError(It.IsAny<string>())).Returns(errorResult);

            var actual = sut.Add(validAddSkaterModel);

            Assert.True(actual.HasErrors);
        }

        [Fact]
        public void ShouldFailGetWithException()
        {
            var errorResult = new Result();

            errorResult.AddError("Error1");

            skaterDataMock.Setup(s => s.Get()).Throws(new Exception());

            loggerManagerMock.Setup(s => s.LogError(It.IsAny<string>())).Returns(errorResult);

            var actual = sut.Get();

            Assert.True(actual.HasErrors);
        }
    }
}
