using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopicDomain.Application.Interfaces;
using TopicDomain.Application.UnitOfWork;
using TopicDomain.Application.UseCases.CreateTopic;
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
        public async Task TestTestCase() 
        {
            CreateTopicCase Case = new CreateTopicCase(_CreateTopiсUnitOfWorkMock.Object, loggerMock.Object);
            Topic topic = new Topic("    ", TopicCreatorSourseEnum.Manager, null, null);

            var result = await Case.HandleAsync(topic);

            Assert.False(result.IsSuccess);
            Assert.Equal("Name is Empty", result.Error);

            loggerMock.VerifyLog(logger => logger.LogError("Name is Empty"), Times.Once());
            _CreateTopiсUnitOfWorkMock.Verify(c => c.CreateTopicAsync(It.IsAny<Topic>()), Times.Never);


        }
    }
}
