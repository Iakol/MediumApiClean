using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopicDomain.Application.AutoMapper;
using TopicDomain.Domain;
using TopicDomain.Infrastructure.Database.DBContext;
using TopicDomain.Infrastructure.Database.Repositories;

namespace TopicDomainTest.Infrastructure.Repository
{
    public class TopicRepositoryTest
    {
        private readonly TopicRepository _topicrepository;
        private readonly AppDBContext _db;
        private readonly IMapper _mapper;
        public TopicRepositoryTest() 
        {
            var options = new DbContextOptionsBuilder<AppDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _db = new AppDBContext(options);
            _mapper = CreateMapper();

            _topicrepository = new TopicRepository(_db, _mapper);
        }
        private IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
        [Fact]
        public async Task RepositoryAddAsyncEntityTest()
        {
            Topic testTopic = new Topic("Test", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await _topicrepository.AddAsync(testTopic);
            await _db.SaveChangesAsync();
            var result = await _topicrepository.GetEntityAsync(testTopic.Id);
            Assert.NotNull(result);
            result.Name.Should().Be(testTopic.Name);
            result.Id.Should().Be(testTopic.Id);
        }
        [Fact]
        public async Task RepositoryUpdateAsyncEntityTest() 
        {
            Topic testTopic = new Topic("Test", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await _topicrepository.AddAsync(testTopic);
            await _db.SaveChangesAsync();
            Topic SavedTopic = await _topicrepository.GetEntityAsync(testTopic.Id);
            Assert.NotNull(SavedTopic);
            SavedTopic.Name.Should().Be(testTopic.Name);
            SavedTopic.Id.Should().Be(testTopic.Id);
            SavedTopic.Name = "Test2";
            await _topicrepository.UpdateAsync(SavedTopic);
            await _db.SaveChangesAsync();
            Topic UpdatedSavedTopic = await _topicrepository.GetEntityAsync(testTopic.Id);
            Assert.NotNull(UpdatedSavedTopic);
            UpdatedSavedTopic.Name.Should().Be(SavedTopic.Name);
            UpdatedSavedTopic.Id.Should().Be(SavedTopic.Id);
        }

        [Fact]
        public async Task RepositoryDeleteAsyncEntityTest()
        {
            Topic testTopic = new Topic("Test", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await _topicrepository.AddAsync(testTopic);
            await _db.SaveChangesAsync();
            Topic SavedTopic = await _topicrepository.GetEntityAsync(testTopic.Id);
            Assert.NotNull(SavedTopic);
            await _topicrepository.DeleteAsync(testTopic.Id);
            await _db.SaveChangesAsync();
            Topic DeleteSavedTopic = await _topicrepository.GetEntityAsync(testTopic.Id);
            Assert.Null(DeleteSavedTopic);



        }


        [Fact]
        public async Task RepositoryFindTopicsByNameAsyncTest()
        {
            Topic testTopic1 = new Topic("Test1", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await _topicrepository.AddAsync(testTopic1);
            Topic testTopic2 = new Topic("Test2", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await _topicrepository.AddAsync(testTopic2);
            Topic testTopic3 = new Topic("Test3", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await _topicrepository.AddAsync(testTopic3);
            Topic testTopic4 = new Topic("GTest4", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await _topicrepository.AddAsync(testTopic4);
            Topic testTopic5 = new Topic("GTest5G", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await _topicrepository.AddAsync(testTopic5);
            Topic testTopic6 = new Topic("GGTest6G", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await _topicrepository.AddAsync(testTopic6);
            Topic testTopic7 = new Topic("GGTest7GG", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await _topicrepository.AddAsync(testTopic7);

            Topic testTopic8 = new Topic("GGTфівessdft7GG", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await _topicrepository.AddAsync(testTopic8);


            await _db.SaveChangesAsync();

            List<Topic> topics = await _topicrepository.FindTopicsByNameAsync("Test");

            Assert.Equal(7, topics.Count);

            
        }

        [Fact]
        public async Task RepositoryGetLats100TopicTest()
        {
            Topic testTopic1 = new Topic("Test1", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await _topicrepository.AddAsync(testTopic1);
            Topic testTopic2 = new Topic("Test2", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await _topicrepository.AddAsync(testTopic2);
            Topic testTopic3 = new Topic("Test3", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await _topicrepository.AddAsync(testTopic3);
            Topic testTopic4 = new Topic("GTest4", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await _topicrepository.AddAsync(testTopic4);
            Topic testTopic5 = new Topic("GTest5G", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await _topicrepository.AddAsync(testTopic5);
            Topic testTopic6 = new Topic("GGTest6G", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await _topicrepository.AddAsync(testTopic6);
            Topic testTopic7 = new Topic("GGTest7GG", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await _topicrepository.AddAsync(testTopic7);

            Topic testTopic8 = new Topic("GGTфівessdft7GG", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await _topicrepository.AddAsync(testTopic8);


            await _db.SaveChangesAsync();
            List<Topic> topics = await _topicrepository.GetLats100Topic();

            Assert.Equal(8, topics.Count);


        }
    }
}
