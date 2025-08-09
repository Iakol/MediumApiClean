using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Application.UnitsOfWork;
using ReadingListDomain.Domain;
using ReadingListDomain.Infrastructure.Database.DBContext;
using ReadingListDomain.Infrastructure.Database.Repositories;

namespace ReadingListDomain.Infrastructure.Database.UnitOfWorks
{
    public class CreateStoryInReadingListUnit(IStoryInReadingListRepository _storyInReadingListRepository , AppDBContext _db) : ICreateStoryInReadingListUnit
    {
        public async Task CreateStoryInReadingList(StoryInReadingList Savestory)
        {
            await _storyInReadingListRepository.AddAsync(Savestory);
            await _db.SaveChangesAsync();            

        }
    }
}
