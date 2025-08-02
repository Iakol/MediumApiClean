using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopicDomain.Application.Interfaces;
using TopicDomain.Application.UseCases;
using TopicDomain.Domain;

namespace TopicDomainTest.Application.UseCasesTest
{
    public class GetLast100TopicsCaseTest
    {
        private readonly Mock<ITopicRepository> _topicRepositoryMock;
        private readonly Mock<ILogger<GetLast100TopicsCase>> loggerMock;

        public GetLast100TopicsCaseTest()
        {
            _topicRepositoryMock = new Mock<ITopicRepository>();
            loggerMock = new Mock<ILogger<GetLast100TopicsCase>>();
        }

        [Fact]
        public async Task PositiveCase() 
        {
            _topicRepositoryMock.Setup(r => r.GetLats100Topic()).ReturnsAsync(new List<Topic>() {new Topic("Test1",TopicDomain.Enum.TopicCreatorSourseEnum.Manager) });
            
            GetLast100TopicsCase Case = new GetLast100TopicsCase(_topicRepositoryMock.Object, loggerMock.Object);

            var result = await Case.HandleAsync();

            Assert.NotNull(result.Data);
            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);

            loggerMock.VerifyLog(l => l.LogError(It.IsAny<string>()), Times.Never);
            _topicRepositoryMock.Verify(r => r.GetLats100Topic(), Times.Once);
        }

        

        [Fact]
        public async Task RepositoryError() 
        {
            string error = "Db Error";
            _topicRepositoryMock.Setup(r => r.GetLats100Topic()).ThrowsAsync(new Exception(error));
            GetLast100TopicsCase Case = new GetLast100TopicsCase(_topicRepositoryMock.Object, loggerMock.Object);
            var result = await Case.HandleAsync();

            Assert.False(result.IsSuccess);
            Assert.Equal(error,result.Error);
            Assert.Null(result.Data);

            loggerMock.VerifyLog(l => l.LogError(It.IsAny<string>()), Times.Once);

            _topicRepositoryMock.Verify(r => r.GetLats100Topic(), Times.Once);
        }
    }
}
