using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopicDomain.Application.DTO;
using TopicDomain.Application.Interfaces;
using TopicDomain.Application.UseCases;
using TopicDomain.Domain;

namespace TopicDomainTest.Application.UseCasesTest
{
    public class GetTopicDTOByIdsCaseTest
    {
        private Mock<ITopicRepository> _topicRepositoryMock;
        private Mock<ILogger<GetTopicDTOByIdsCase>> _logger;
        private Mock<IMapper> mapper;

        public GetTopicDTOByIdsCaseTest()
        {
            _topicRepositoryMock = new Mock<ITopicRepository>();
            _logger = new Mock<ILogger<GetTopicDTOByIdsCase>>();
            mapper = new Mock<IMapper>();
        }


        [Fact]
        public async Task EmptyIdsTest() 
        {
            GetTopicDTOByIdsCase Case = new GetTopicDTOByIdsCase(_topicRepositoryMock.Object, _logger.Object, mapper.Object);
            List<string> ids = new List<string>();

            var result = await Case.HandleAsync(ids);

            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
            Assert.Null(result.Data);
            Assert.Equal("Ids list is empty", result.Error);

            _logger.VerifyLog(l => l.LogError(It.IsAny<string>()),Times.Once, "Ids list is empty");
            _topicRepositoryMock.Verify(t => t.GetEntityAsync(It.IsAny<string>()),Times.Never);
        }

        [Fact]
        public async Task PositiveTest()
        {
            _topicRepositoryMock.Setup(t => t.GetEntityAsync(It.IsAny<string>())).ReturnsAsync(new Topic("Test", TopicDomain.Enum.TopicCreatorSourseEnum.User));
            mapper.Setup(m => m.Map<TopicDTO>(It.IsAny<Topic>())).Returns(new TopicDTO());
            GetTopicDTOByIdsCase Case = new GetTopicDTOByIdsCase(_topicRepositoryMock.Object, _logger.Object, mapper.Object);
            


            List<string> ids = new List<string>() {"TEST1","TEST2" };

            var result = await Case.HandleAsync(ids);


            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Null(result.Error);
            Assert.IsType<List<TopicDTO>>(result.Data);

            _logger.VerifyLog(l => l.LogError(It.IsAny<string>()), Times.Never);
            _topicRepositoryMock.Verify(t => t.GetEntityAsync(It.IsAny<string>()), Times.Exactly(2));

        }

        [Fact]
        public async Task DbError() 
        {
            _topicRepositoryMock.Setup(t => t.GetEntityAsync(It.IsAny<string>())).ThrowsAsync(new Exception("DbError"));
            GetTopicDTOByIdsCase Case = new GetTopicDTOByIdsCase(_topicRepositoryMock.Object, _logger.Object, mapper.Object);
            List<string> ids = new List<string>() { "TEST1", "TEST2" };
            var result = await Case.HandleAsync(ids);

            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
            Assert.Null(result.Data);
            Assert.Equal("DbError", result.Error);


            _logger.VerifyLog(l => l.LogError(It.IsAny<string>()), Times.Once, "Error when take topic list by topic ids");
            _topicRepositoryMock.Verify(t => t.GetEntityAsync(It.IsAny<string>()), Times.AtLeastOnce);
        }
    }
}
