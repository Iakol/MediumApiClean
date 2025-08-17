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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TopicDomainTest.Application.UseCasesTest
{
    public class GetTopicsByIdsCaseTest
    {
        private readonly Mock<ITopicRepository> _topicRepository;
        private readonly Mock<ILogger<GetTopicsByIdsCase>> _logger;

        public GetTopicsByIdsCaseTest() 
        {
            _topicRepository = new Mock<ITopicRepository>();
            _logger = new Mock<ILogger<GetTopicsByIdsCase>>();
        }


        [Fact]
        public async Task EmptyIdsList() 
        {
            GetTopicsByIdsCase Case = new GetTopicsByIdsCase(_topicRepository.Object, _logger.Object);
            List<string> ids = new List<string>();

            var result = await Case.HandleAsync(ids);

            Assert.NotNull(result.Error);
            Assert.Null(result.Data);
            Assert.False(result.IsSuccess);
            Assert.Equal("Ids list is empty", result.Error);

            _topicRepository.Verify(t => t.GetEntityAsync(It.IsAny<string>()),Times.Never);
            _logger.VerifyLog(l => l.LogError(It.IsAny<string>()), Times.Once, "Ids list is empty");

        }

        [Fact]
        public async Task PositiveTest()
        {
            _topicRepository.Setup(t => t.GetEntityAsync(It.IsAny<string>())).ReturnsAsync(new Topic("Test", TopicDomain.Enum.TopicCreatorSourseEnum.User));
            GetTopicsByIdsCase Case = new GetTopicsByIdsCase(_topicRepository.Object, _logger.Object);
            List<string> ids = new List<string>(){"Test1","Test2"};

            var result = await Case.HandleAsync(ids);

            Assert.NotNull(result.Data);
            Assert.Null(result.Error);
            Assert.True(result.IsSuccess);
            Assert.IsType<List<Topic>>(result.Data);

            _topicRepository.Verify(t => t.GetEntityAsync(It.IsAny<string>()), Times.Exactly(2));
            _logger.VerifyLog(l => l.LogError(It.IsAny<string>()),Times.Never);
        }

        [Fact]
        public async Task BDError()
        {
            _topicRepository.Setup(t => t.GetEntityAsync(It.IsAny<string>())).ThrowsAsync(new Exception("DbError"));
            GetTopicsByIdsCase Case = new GetTopicsByIdsCase(_topicRepository.Object, _logger.Object);
            List<string> ids = new List<string>() { "Test1", "Test2" };
            var result = await Case.HandleAsync(ids);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.False(result.IsSuccess);
            Assert.Equal("DbError", result.Error);

            _topicRepository.Verify(t => t.GetEntityAsync(It.IsAny<string>()), Times.AtLeastOnce());
            _logger.VerifyLog(l => l.LogError(It.IsAny<string>()), Times.Once, "DbError");

        }
    }
}
