using ResponceDomain.Application.Interfaces;
using ResponceDomain.Application.UnitsOfWorks;
using ResponceDomain.Domain;
using ResponceDomain.Infrastructure.DataBase.DBContex;
using ResponceDomain.Infrastructure.DataBase.Repositories;

namespace ResponceDomain.Infrastructure.DataBase.UnitsOfWork
{
    public class DeleteResponceUnit : IDeleteResponceUnit
    {
        IResponceRepository _responceRepository;
        IClapsToResponceOfUsersIterfaces _clapsToResponceOfUsersIterfaces;
        AppDBContext _db;

        public DeleteResponceUnit(IResponceRepository responceRepository, IClapsToResponceOfUsersIterfaces responceOfUsersIterfaces, AppDBContext db) 
        {
            _responceRepository = responceRepository;
            _clapsToResponceOfUsersIterfaces = responceOfUsersIterfaces;
            _db = db;
        }
        public async Task DeleteResponce(int ParentId)
        {
            IEnumerable<int> ids = await _responceRepository.GetTreeFlatListOfResponceIDsByParent(ParentId);

            IEnumerable<ClapsToResponceOfUsers> claps = (await _clapsToResponceOfUsersIterfaces.getClapsToResponceOfUsersByRespocnceList(ids.ToList())).SelectMany(r => r.Value);

            await _clapsToResponceOfUsersIterfaces.DeleteClapsToResponceEntityByClapsList(claps);
            await _responceRepository.DeleteResponceList(ids.ToList());

            await _db.SaveChangesAsync();

        }
    }
}
