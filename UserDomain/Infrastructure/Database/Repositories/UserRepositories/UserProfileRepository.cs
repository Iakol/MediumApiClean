using AutoMapper;
using UserDomain.Application.Interfaces.Repository;
using UserDomain.Domain;
using UserDomain.Infrastructure.Database.DBContext;
using UserDomain.Infrastructure.Database.Models;

namespace UserDomain.Infrastructure.Database.Repositories.UserRepositories
{
    public class UserProfileRepository : CommonDbIteraction<UserProfileModel, UserProfile, string>, IUserProfileRepository
    {
        public UserProfileRepository(AppDbContext _db, IMapper _mappper) : base(_db, _mappper)
        {

        }
    }
}
