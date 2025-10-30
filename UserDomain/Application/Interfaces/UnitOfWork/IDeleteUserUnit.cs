using UserDomain.Infrastructure.Database.Models;

namespace UserDomain.Application.Interfaces.UnitOfWork
{
    public interface IDeleteUserUnit
    {
        public Task DeleteUser(UserModel user);
    }
}
