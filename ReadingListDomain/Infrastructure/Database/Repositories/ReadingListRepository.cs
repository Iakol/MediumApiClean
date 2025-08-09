using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Domain;
using ReadingListDomain.Infrastructure.Database.DBContext;
using ReadingListDomain.Infrastructure.Database.Models;
using System.Linq;

namespace ReadingListDomain.Infrastructure.Database.Repositories
{
    public class ReadingListRepository : CommonDbIteraction<ReadingListModel, ReadingList>, IReadingListRepository
    {
        public ReadingListRepository(IMapper mapper, AppDBContext db) : base(mapper, db)
        {
        }

        public async Task<List<ReadingList>> GetListOfReadingList(List<string> Ids)
        {
            List<ReadingListModel> list = await Models.Where(r => Ids.Contains(r.Id)).ToListAsync();
            return _mapper.Map<List<ReadingList>>(list);
        }

        public async Task<List<ReadingList>> GetListOfReadingList(string UserId)
        {
            List<ReadingListModel> list = await Models.Include(r =>r.SaveStories).Where(r => r.ReadingListCreator.Equals(UserId)).ToListAsync();
            return _mapper.Map<List<ReadingList>>(list);
        }

        public async Task UpdateReadingLisOpenResponces(bool isOpenResponces, string readingListID)
        {
            ReadingListModel entity = await Models.Include(r => r.SaveStories).FirstAsync(r => r.Id.Equals(readingListID));
                entity.IsOpenResponces = isOpenResponces;
        }

        public async Task UpdateReadingListPrivate(bool isPrivate, string readingListID)
        {
            ReadingListModel entity = await Models.FirstAsync(r => r.Id.Equals(readingListID));
            entity.IsPrivate = isPrivate;
        }
    }
}
