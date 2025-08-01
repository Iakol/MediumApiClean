using AutoMapper;
using UserDomain.Application.Interfaces.Repository;
using UserDomain.Domain;
using UserDomain.Infrastructure.Database.DBContext;
using UserDomain.Infrastructure.Database.Models;

namespace UserDomain.Infrastructure.Database.Repositories.UserRepositories
{
    public class UserRepository : CommonDbIteraction<UserModel, User, string>, IUserRepository
    {
        public UserRepository(AppDbContext _db, IMapper _mappper) : base(_db, _mappper)
        {

        }


    }
}
