using AutoMapper;
using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Infrastructure.DataBase.DBContext;
using UserRealetionShipDomain.Infrastructure.DataBase.Model;

namespace UserRealetionShipDomain.Infrastructure.DataBase.Repositories.RelationShip
{
    public class UserBlockModelRepository : CommonDbRepository<UserBlockModel, UserBlock>, IUserBlockRepository
    {
        public UserBlockModelRepository(AppDbContext _db, IMapper _mappper) : base(_db, _mappper)
        {
        }
    }
}
