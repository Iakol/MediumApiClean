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
    public class FindTopicsDTOByNameCaseTest
    {
        private readonly Mock<ITopicRepository> _ItopicRepositoryMock;
        private readonly Mock<ILogger<FindTopicsDTOByNameCase>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;


        public FindTopicsDTOByNameCaseTest()
        {
            _ItopicRepositoryMock = new Mock<ITopicRepository>();
            _loggerMock = new Mock<ILogger<FindTopicsDTOByNameCase>>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task EmptyTopicName()
        {
            FindTopicsDTOByNameCase Case = new FindTopicsDTOByNameCase(_ItopicRepositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
            string Name = string.Empty;

            var result = await Case.HandleAsync(Name);

            Assert.False(result.IsSuccess);
            Assert.Equal("Topic name is empty", result.Error);


            _loggerMock.VerifyLog(l => l.LogError(It.IsAny<string>()), "Topic name is empty");
            _ItopicRepositoryMock.Verify(r => r.FindTopicsByNameAsync(It.IsAny<string>()), Times.Never);
            _mapperMock.Verify(m => m.Map<TopicDTO>(It.IsAny<Topic>), Times.Never);

        }

        [Fact]
        public async Task PositiveRightParamsTest()
        {
            _ItopicRepositoryMock.Setup(r => r.FindTopicsByNameAsync(It.IsAny<string>())).ReturnsAsync(new List<Topic> { new Topic("Test1",TopicDomain.Enum.TopicCreatorSourseEnum.User,null,null)});
            _mapperMock.Setup(m => m.Map<TopicDTO>(It.IsAny<Topic>())).Returns(new TopicDTO());
            FindTopicsDTOByNameCase Case = new FindTopicsDTOByNameCase(_ItopicRepositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
            string Name = "Test1";

            var result = await Case.HandleAsync(Name);

            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);
            Assert.IsType<List<TopicDTO>>(result.Data);

            _loggerMock.VerifyLog(l => l.LogError(It.IsAny<string>()), Times.Never);
            _ItopicRepositoryMock.Verify(r => r.FindTopicsByNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ExceptionTest()
        {
            string Error = "DataBase error";
            _ItopicRepositoryMock.Setup(r => r.FindTopicsByNameAsync(It.IsAny<string>())).ThrowsAsync(new Exception(Error));

            FindTopicsDTOByNameCase Case = new FindTopicsDTOByNameCase(_ItopicRepositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
            string Name = "Test1";


            var result = await Case.HandleAsync(Name);

            Assert.False(result.IsSuccess);
            Assert.Equal(Error, result.Error);
            Assert.NotNull(result.Error);

            _loggerMock.VerifyLog(l => l.LogError(It.IsAny<string>()), Times.Once);
            _ItopicRepositoryMock.Verify(r => r.FindTopicsByNameAsync(It.IsAny<string>()), Times.Once);

        }
    }
}
