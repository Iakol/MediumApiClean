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
using TopicDomain.Infrastructure.Database.UnitsOfWork;

namespace TopicDomainTest.Infrastructure.UnitOfWork
{
    public class CreateTopiсUnitOfWorkTest
    {

        private IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            },null);

            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
        [Fact]
        public async Task CreateTopicTest() 
        {
            var options = new DbContextOptionsBuilder<AppDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var _db = new AppDBContext(options);
            var repository = new TopicRepository(_db, CreateMapper());

            CreateTopiсUnitOfWork Testouw = new CreateTopiсUnitOfWork(repository, _db);
            Topic TopicTest = new Topic("Test", TopicDomain.Enum.TopicCreatorSourseEnum.User);
            await Testouw.CreateTopicAsync(TopicTest);

            var topic = await repository.GetEntityAsync(TopicTest.Id);

            topic.Should().NotBeNull();
            topic!.Name.Should().Be("Test");    

        }
    }
}
