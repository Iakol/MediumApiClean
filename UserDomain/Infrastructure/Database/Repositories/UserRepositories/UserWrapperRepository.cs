using AutoMapper;
using UserDomain.Application.Interfaces.Repository;
using UserDomain.Domain;
using UserDomain.Infrastructure.Database.DBContext;
using UserDomain.Infrastructure.Database.Models;

namespace UserDomain.Infrastructure.Database.Repositories.UserRepositories
{
    public class UserWrapperRepository : CommonDbIteraction<UserWrapperModel, UserWrapper, string>, IUserWrapperRepository
    {
        public UserWrapperRepository(AppDbContext _db, IMapper _mappper) : base(_db, _mappper)
        {

        }
    }
}
