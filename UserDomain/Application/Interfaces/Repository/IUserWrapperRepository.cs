using UserDomain.Domain;

namespace UserDomain.Application.Interfaces.Repository
{
    public interface IUserWrapperRepository
    {
        public Task AddAsync(UserWrapper entity);

        public Task UpdateAsync(UserWrapper entity);

        public Task DeleteAsync(string id);

        public Task<UserWrapper?> GetEntityAsync(string id);
    }
}
