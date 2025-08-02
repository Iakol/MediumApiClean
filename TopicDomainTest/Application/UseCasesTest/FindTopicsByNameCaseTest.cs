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
    public class FindTopicsByNameCaseTest
    {
        private readonly Mock<ITopicRepository> _ItopicRepositoryMock;
        private readonly Mock<ILogger<FindTopicsByNameCase>> _loggerMock;

        public FindTopicsByNameCaseTest()
        {
            _ItopicRepositoryMock = new Mock<ITopicRepository>();
            _loggerMock = new Mock<ILogger<FindTopicsByNameCase>>();
        }

        [Fact]
        public async Task EmptyTopicName()
        {
            FindTopicsByNameCase Case = new FindTopicsByNameCase(_ItopicRepositoryMock.Object, _loggerMock.Object);
            string Name = string.Empty;

            var result = await Case.HandleAsync(Name);

            Assert.False(result.IsSuccess);
            Assert.Equal("Topic name is empty", result.Error);


            _loggerMock.VerifyLog(l => l.LogError(It.IsAny<string>()), "Topic name is empty");
            _ItopicRepositoryMock.Verify(r => r.FindTopicsByNameAsync(It.IsAny<string>()), Times.Never);


        }

        [Fact]
        public async Task PositiveRightParamsTest() 
        {
            _ItopicRepositoryMock.Setup(t => t.FindTopicsByNameAsync(It.IsAny<string>())).ReturnsAsync(new List<Topic>{ new Topic("test",TopicDomain.Enum.TopicCreatorSourseEnum.Manager)});
            FindTopicsByNameCase Case = new FindTopicsByNameCase(_ItopicRepositoryMock.Object, _loggerMock.Object);
            string Name = "Test1";

            var result = await Case.HandleAsync(Name);

            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);
            Assert.IsType<List<Topic>>(result.Data);

            _loggerMock.VerifyLog(l => l.LogError(It.IsAny<string>()), Times.Never);
            _ItopicRepositoryMock.Verify(r => r.FindTopicsByNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ExceptionTest() 
        {
            string Error = "DataBase error";
            _ItopicRepositoryMock.Setup(r => r.FindTopicsByNameAsync(It.IsAny<string>())).ThrowsAsync(new Exception(Error));

            FindTopicsByNameCase Case = new FindTopicsByNameCase(_ItopicRepositoryMock.Object, _loggerMock.Object);
            string Name = "Test1";


            var result = await Case.HandleAsync(Name);

            Assert.False(result.IsSuccess);
            Assert.Equal(Error,result.Error);
            Assert.NotNull(result.Error);

            _loggerMock.VerifyLog(l => l.LogError(It.IsAny<string>()), Times.Once);
            _ItopicRepositoryMock.Verify(r => r.FindTopicsByNameAsync(It.IsAny<string>()), Times.Once);

        }

    }
}
