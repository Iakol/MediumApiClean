using ResponceDomain.Application.Interfaces;
using ResponceDomain.Application.UnitsOfWorks;
using ResponceDomain.Domain;
using ResponceDomain.Infrastructure.DataBase.DBContex;

namespace ResponceDomain.Infrastructure.DataBase.UnitsOfWork
{
    public class AddClapsToResponceUnit : IAddClapsToResponceUnit
    {
        IClapsToResponceOfUsersIterfaces _clapsToResponceOfUsersIterfaces;
        AppDBContext _db;

        public AddClapsToResponceUnit(IClapsToResponceOfUsersIterfaces clapsToResponceOfUsersIterfaces, AppDBContext db)
        {
            _clapsToResponceOfUsersIterfaces = clapsToResponceOfUsersIterfaces;
            _db = db;
        }

        public async Task AddClaps(ClapsToResponceOfUsers clapsToResponceOfUsers)
        {
            await _clapsToResponceOfUsersIterfaces.AddClapsToResponceEntity(clapsToResponceOfUsers);
            await _db.SaveChangesAsync();
        }
    }
}
