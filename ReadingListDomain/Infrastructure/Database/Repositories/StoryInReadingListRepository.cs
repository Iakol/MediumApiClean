using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Domain;
using ReadingListDomain.Infrastructure.Database.DBContext;
using ReadingListDomain.Infrastructure.Database.Models;

namespace ReadingListDomain.Infrastructure.Database.Repositories
{
    public class StoryInReadingListRepository : CommonDbIteraction<StoryInReadingListModel, StoryInReadingList>, IStoryInReadingListRepository
    {

        public StoryInReadingListRepository(IMapper mapper, AppDBContext db) : base(mapper, db)
        {

        }

        public async Task DeleteAllStoryByReadingList(string ReadingListId)
        {
            List < StoryInReadingListModel > stories = await Models.Where(s => s.ReadingListId.Equals(ReadingListId)).ToListAsync();
            Models.RemoveRange(stories);
        
        }

        public async Task<List<StoryInReadingList>> GetSaveStoryListByReadingList(string ReadingListId)
        {
            List<StoryInReadingListModel> list = await Models.Where(s => s.ReadingListId.Equals(ReadingListId)).ToListAsync();

            return _mapper.Map<List<StoryInReadingList>>(list);
        }

        public async Task<bool> isSaveStoryInReadingList(string ReadingListId, string storyId)
        {
            return await Models.Where(s => s.ReadingListId.Equals(ReadingListId)).AnyAsync(s => s.StoryId.Equals(storyId));
        }

        public async Task UpdateNote(string Note, string storyInListId)
        {
            StoryInReadingListModel entity = await Models.FirstAsync(x => x.Id.Equals(storyInListId));

            entity.Note = Note;
        }


    }
}
