using AutoMapper;
using UserDomain.Application.Interfaces.Repository;
using UserDomain.Domain;
using UserDomain.Infrastructure.Database.DBContext;
using UserDomain.Infrastructure.Database.Models;

namespace UserDomain.Infrastructure.Database.Repositories.UserRepositories
{
    public class UserMemberShipRepository : CommonDbIteraction<UserMemberShipModel, UserMemberShip, string>, IUserMemberShipRepository
    {


        public UserMemberShipRepository(AppDbContext _db, IMapper _mappper) : base(_db, _mappper)
        {

        }


    }
}
