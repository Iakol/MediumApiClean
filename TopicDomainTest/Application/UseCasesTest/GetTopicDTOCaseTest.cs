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
    public class GetTopicDTOCaseTest
    {
        private readonly Mock<ITopicRepository> _topicRepository;
        private readonly Mock<ILogger<GetTopicDTOCase>> _logger;
        private readonly Mock<IMapper> _mapper;

        public GetTopicDTOCaseTest() 
        {
            _topicRepository = new Mock<ITopicRepository>();
            _logger = new Mock<ILogger<GetTopicDTOCase>>();
            _mapper = new Mock<IMapper>(); 
        }

        [Fact]
        public async Task EmptyTopicIdTest()
        {
            GetTopicDTOCase Case = new GetTopicDTOCase(_topicRepository.Object, _logger.Object, _mapper.Object);
            
            string id = string.Empty;

            var result = await Case.HandleAsync(id);

            Assert.NotNull(result.Error);
            Assert.False(result.IsSuccess);
            Assert.Equal("Topic Id is empty", result.Error);
            Assert.Null(result.Data);

            _logger.VerifyLog(l => l.LogError(It.IsAny<string>()), Times.Once, "Topic Id is empty");
            _topicRepository.Verify(t => t.GetEntityAsync(It.IsAny<string>()), Times.Never);

        }

        [Fact]
        public async Task PositiveTest() 
        {
            _topicRepository.Setup(t => t.GetEntityAsync(It.IsAny<string>())).ReturnsAsync(new Topic("Test", TopicDomain.Enum.TopicCreatorSourseEnum.User));
            _mapper.Setup(m => m.Map<TopicDTO>(It.IsAny<Topic>())).Returns(new TopicDTO());
            GetTopicDTOCase caseTest = new GetTopicDTOCase(_topicRepository.Object, _logger.Object,_mapper.Object);
            string id = "TestId";
            var result = await caseTest.HandleAsync(id);
            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);
            Assert.NotNull(result.Data);
            Assert.IsType<TopicDTO>(result.Data);

            _logger.VerifyLog(l => l.LogError(It.IsAny<string>()), Times.Never);
            _topicRepository.Verify(t => t.GetEntityAsync(It.IsAny<string>()), Times.Once);
            _mapper.Verify(t => t.Map<TopicDTO>(It.IsAny<Topic>()), Times.Once);

        }

        [Fact]
        public async Task DbError()
        {
            _topicRepository.Setup(t => t.GetEntityAsync(It.IsAny<string>())).ThrowsAsync(new Exception("DbError"));
            GetTopicDTOCase Case = new GetTopicDTOCase(_topicRepository.Object, _logger.Object, _mapper.Object);
            string id = "TestId";
            var result = await Case.HandleAsync(id);

            Assert.False(result.IsSuccess);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal("DbError", result.Error);

            _logger.VerifyLog(l => l.LogError(It.IsAny<string>()));
            _topicRepository.Verify(t => t.GetEntityAsync(It.IsAny<string>()),Times.Once, "DbError");

        }


    }
}
