using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Infrastructure.DataBase.Model;

namespace UserRealetionShipDomain.Application.Interfaces
{
    public interface IUserFollowRepository
    {
        public Task AddAsync(UserFollow entity);
        public Task DeleteAsync(string Followid, string subcsriptionid);
        public Task<UserFollow?> GetEntityAsync(string Followid, string subcsriptionid);

        public Task<List<UserFollow>> GetEntityListByFollowIdAsync(string Followid );

    }
}
