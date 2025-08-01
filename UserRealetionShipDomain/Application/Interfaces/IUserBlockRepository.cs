using UserRealetionShipDomain.Domain;

namespace UserRealetionShipDomain.Application.Interfaces
{
    public interface IUserBlockRepository
    {
        public Task AddAsync(UserBlock entity);
        public Task DeleteAsync(string Followid, string subcsriptionid);
        public Task<UserBlock?> GetEntityAsync(string Followid, string subcsriptionid);

        public Task<List<UserBlock>> GetEntityListByFollowIdAsync(string Followid );
    }
}
