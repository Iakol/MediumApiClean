using UserDomain.Domain;

namespace UserDomain.Application.Interfaces.Repository
{
    public interface IUserMemberShipRepository
    {
        public Task AddAsync(UserMemberShip entity);

        public Task UpdateAsync(UserMemberShip entity);

        public Task DeleteAsync(string id);

        public Task<UserMemberShip?> GetEntityAsync(string id);
    }
}
