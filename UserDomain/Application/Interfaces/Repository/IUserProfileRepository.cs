using UserDomain.Domain;

namespace UserDomain.Application.Interfaces.Repository
{
    public interface IUserProfileRepository
    {
        public Task AddAsync(UserProfile entity);

        public Task UpdateAsync(UserProfile entity);

        public Task DeleteAsync(string id);

        public Task<UserProfile?> GetEntityAsync(string id);
    }
}
