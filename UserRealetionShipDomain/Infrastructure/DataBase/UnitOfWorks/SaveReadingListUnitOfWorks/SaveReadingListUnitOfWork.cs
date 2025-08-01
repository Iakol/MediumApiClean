using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Application.UnitOfWorks;
using UserRealetionShipDomain.Application.UnitOfWorks.ISaveReadingListUnitOfWorks;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Infrastructure.DataBase.DBContext;

namespace UserRealetionShipDomain.Infrastructure.DataBase.UnitOfWorks.SaveReadingListUnitOfWorks
{
    public class SaveReadingListUnitOfWork(ISaveReadingListRepository _saveReadingListRepository, AppDbContext _db) : ISaveReadingListUnitOfWork
    {
        public async Task SaveReadinglist(string UserId, string readingList)
        {
            SaveReadingList saveReadingList = await _saveReadingListRepository.GetEntityAsync(UserId, readingList);
            if (saveReadingList != null)
            {
                throw new Exception("The Reading List was save");
            }
            else
            {
                SaveReadingList saveReadingListToCreate = new SaveReadingList
                {
                    UserId = UserId,
                    SaveReadingListId = readingList,
                    SaveAt = DateTime.Now,
                };
                await _saveReadingListRepository.AddAsync(saveReadingListToCreate);
                _db.SaveChanges();
            }
        }
    }
}
