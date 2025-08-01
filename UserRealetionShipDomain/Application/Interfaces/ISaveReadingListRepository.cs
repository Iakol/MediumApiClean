using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Infrastructure.DataBase.Model;

namespace UserRealetionShipDomain.Application.Interfaces
{
    public interface ISaveReadingListRepository
    {
        public Task AddAsync(SaveReadingList entity);
        public Task DeleteAsync(string Followid, string subcsriptionid);
        public Task<SaveReadingList?> GetEntityAsync(string Followid, string subcsriptionid);

        public Task<List<SaveReadingList>> GetEntityListByFollowIdAsync(string Followid);
    }
}
