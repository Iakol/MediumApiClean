using UserRealetionShipDomain.Domain;

namespace UserRealetionShipDomain.Application.Interfaces
{
    public interface IUserMuteRepository
    {
        public Task AddAsync(UserMute entity);
        public Task DeleteAsync(string Followid, string subcsriptionid);
        public Task<UserMute?> GetEntityAsync(string Followid, string subcsriptionid);

        public Task<List<UserMute>> GetEntityListByFollowIdAsync(string Followid);
    }
}
