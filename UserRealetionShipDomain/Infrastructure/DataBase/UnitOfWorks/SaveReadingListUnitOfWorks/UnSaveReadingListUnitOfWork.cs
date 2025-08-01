using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Application.UnitOfWorks.ISaveReadingListUnitOfWorks;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Infrastructure.DataBase.DBContext;

namespace UserRealetionShipDomain.Infrastructure.DataBase.UnitOfWorks.SaveReadingListUnitOfWorks
{
    public class UnSaveReadingListUnitOfWork(ISaveReadingListRepository _saveReadingListRepository, AppDbContext _db) : IUnSaveReadingListUnitOfWork
    {
        public async Task UnSaveReadingList(string UserId, string readingList)
        {
            SaveReadingList saveReadingList = await _saveReadingListRepository.GetEntityAsync(UserId, readingList);
            if (saveReadingList == null)
            {
                throw new Exception("The Reading List was not saved");
            }
            else
            {
                await _saveReadingListRepository.DeleteAsync(UserId, readingList);
                _db.SaveChanges();
            }

        }
    }
}
