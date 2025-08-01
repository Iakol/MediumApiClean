using UserDomain.Domain;

namespace UserDomain.Application.Interfaces.Repository
{
    public interface IUserRepository
    {

        public Task AddAsync(User entity);

        public Task UpdateAsync(User entity);

        public Task DeleteAsync(string id);

        public Task<User?> GetEntityAsync(string id);


    }
}
