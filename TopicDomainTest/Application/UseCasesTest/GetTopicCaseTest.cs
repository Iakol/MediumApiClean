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
    public class GetTopicCaseTest
    {
        private Mock<ITopicRepository> _topicRepositoryMock;
        private Mock<ILogger<GetTopicCase>> _logger;

        public GetTopicCaseTest()
        {
            _topicRepositoryMock = new Mock<ITopicRepository>();
            _logger = new Mock<ILogger<GetTopicCase>>();
        }

        [Fact]
        public async Task EmptyIdTest() 
        {
            GetTopicCase Case = new GetTopicCase(_topicRepositoryMock.Object, _logger.Object);
            string id = string.Empty;
            var result = await Case.HandleAsync(id);

            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.False(result.IsSuccess);


            _topicRepositoryMock.Verify(t => t.GetEntityAsync(It.IsAny<string>()), Times.Never);
            _logger.VerifyLog(l => l.LogError(It.IsAny<string>()), Times.Once);

        }

        [Fact]
        public async Task PositiveTestCase() 
        {
            _topicRepositoryMock.Setup(t => t.GetEntityAsync(It.IsAny<string>())).ReturnsAsync(new Topic("Test1", TopicDomain.Enum.TopicCreatorSourseEnum.Manager));
            GetTopicCase Case = new GetTopicCase(_topicRepositoryMock.Object, _logger.Object);
            string id = Guid.NewGuid().ToString();
            var result = await Case.HandleAsync(id);

            Assert.Null(result.Error);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);


            _topicRepositoryMock.Verify(t => t.GetEntityAsync(It.IsAny<string>()), Times.Once);
            _logger.VerifyLog(l => l.LogError(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task NotFoundTestCase()
        {

            GetTopicCase Case = new GetTopicCase(_topicRepositoryMock.Object, _logger.Object);
            string id = Guid.NewGuid().ToString();
            var result = await Case.HandleAsync(id);

            Assert.NotNull(result.Error);
            Assert.False(result.IsSuccess);
            Assert.Null(result.Data);

            _topicRepositoryMock.Verify(t => t.GetEntityAsync(It.IsAny<string>()), Times.Once);
            _logger.VerifyLog(l => l.LogError(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task BdExceptionCase()
        {
            string error = "Db Error";
            _topicRepositoryMock.Setup(t => t.GetEntityAsync(It.IsAny<string>())).ThrowsAsync(new Exception(error));
            GetTopicCase Case = new GetTopicCase(_topicRepositoryMock.Object, _logger.Object);
            string id = Guid.NewGuid().ToString();
            var result = await Case.HandleAsync(id);

            Assert.Equal(error, result.Error);
            Assert.False(result.IsSuccess);
            Assert.Null(result.Data);

            _topicRepositoryMock.Verify(t => t.GetEntityAsync(It.IsAny<string>()), Times.Once);
            _logger.VerifyLog(l => l.LogError(It.IsAny<string>()), Times.Once);
        }


    }
}
