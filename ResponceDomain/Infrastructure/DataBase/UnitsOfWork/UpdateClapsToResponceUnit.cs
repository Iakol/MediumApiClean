using ResponceDomain.Application.Interfaces;
using ResponceDomain.Application.UnitsOfWorks;
using ResponceDomain.Domain;
using ResponceDomain.Infrastructure.DataBase.DBContex;
using ResponceDomain.Infrastructure.DataBase.Repositories;

namespace ResponceDomain.Infrastructure.DataBase.UnitsOfWork
{
    public class UpdateClapsToResponceUnit : IUpdateClapsToResponceUnit
    {
        IClapsToResponceOfUsersIterfaces _clapsToResponceOfUsersIterfaces;
        AppDBContext _db;

        public UpdateClapsToResponceUnit(IClapsToResponceOfUsersIterfaces clapsToResponceOfUsersIterfaces, AppDBContext db)
        {
            _clapsToResponceOfUsersIterfaces = clapsToResponceOfUsersIterfaces;
            _db = db;
        }

        public async Task UpdateClaps(ClapsToResponceOfUsers clapsToUpdate)
        {
            await _clapsToResponceOfUsersIterfaces.UpdateClapsToResponce(clapsToUpdate);
            await _db.SaveChangesAsync();
        }
    }
}
