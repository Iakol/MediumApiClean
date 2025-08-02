using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopicDomain.Application.Interfaces;
using TopicDomain.Application.UnitOfWork;
using TopicDomain.Application.UseCases;
using TopicDomain.Domain;
using TopicDomain.Enum;

namespace TopicDomainTest.Application.UseCasesTest
{

    public class CreateTopicCaseTest
    {
        
        private readonly Mock<ICreateTopiсUnitOfWork> _CreateTopiсUnitOfWorkMock;
        private readonly Mock<ILogger<CreateTopicCase>> loggerMock;

        
        public CreateTopicCaseTest() 
        {
            _CreateTopiсUnitOfWorkMock = new Mock<ICreateTopiсUnitOfWork>();
            loggerMock= new Mock<ILogger<CreateTopicCase>>();
        }

        [Fact]
        public async Task EmptyNameOfTopic() 
        {
            CreateTopicCase Case = new CreateTopicCase(_CreateTopiсUnitOfWorkMock.Object, loggerMock.Object);
            Topic topic = new Topic("    ", TopicCreatorSourseEnum.Manager, null, null);

            var result = await Case.HandleAsync(topic);

            Assert.False(result.IsSuccess);
            Assert.Equal("Name is Empty", result.Error);

            loggerMock.VerifyLog(logger => logger.LogError("Name is Empty"), Times.Once());
            _CreateTopiсUnitOfWorkMock.Verify(c => c.CreateTopicAsync(It.IsAny<Topic>()), Times.Never);


        }

        [Fact]
        public async Task RightTopicTest() 
        {
            CreateTopicCase Case = new CreateTopicCase(_CreateTopiсUnitOfWorkMock.Object, loggerMock.Object);
            Topic topic = new Topic("Test", TopicCreatorSourseEnum.Manager);

            var result = await Case.HandleAsync(topic);

            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);

            loggerMock.VerifyLog(l => l.LogError("Name is Empty"), Times.Never);
            _CreateTopiсUnitOfWorkMock.Verify(c => c.CreateTopicAsync(It.IsAny<Topic>()), Times.Once);
        }

        [Fact]
        public async Task UnitErrorTopicTest()
        {
            _CreateTopiсUnitOfWorkMock.Setup(c => c.CreateTopicAsync(It.IsAny<Topic>())).ThrowsAsync(new Exception("Database error"));
            CreateTopicCase Case = new CreateTopicCase(_CreateTopiсUnitOfWorkMock.Object, loggerMock.Object);
            Topic topic = new Topic("Test", TopicCreatorSourseEnum.Manager);

            var result = await Case.HandleAsync(topic);

            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);

            loggerMock.VerifyLog(l => l.LogError("Database error"), Times.Once);
            _CreateTopiсUnitOfWorkMock.Verify(c => c.CreateTopicAsync(It.IsAny<Topic>()), Times.Once);
        }

    }
}
