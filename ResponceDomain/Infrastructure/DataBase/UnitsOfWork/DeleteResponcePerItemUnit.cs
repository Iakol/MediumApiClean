using ResponceDomain.Application.Interfaces;
using ResponceDomain.Application.UnitsOfWorks;
using ResponceDomain.Domain;
using ResponceDomain.Infrastructure.DataBase.DBContex;

namespace ResponceDomain.Infrastructure.DataBase.UnitsOfWork
{
    public class DeleteResponcePerItemUnit : IDeleteResponcePerItemUnit
    {
        IResponceRepository _responceRepository;
        IClapsToResponceOfUsersIterfaces _responceOfUsersIterfaces;
        AppDBContext _db;

        public DeleteResponcePerItemUnit(IResponceRepository responceRepository, IClapsToResponceOfUsersIterfaces responceOfUsersIterfaces, AppDBContext db)
        {
            _responceRepository = responceRepository;
            _responceOfUsersIterfaces = responceOfUsersIterfaces;
            _db = db;
        }

        public async Task DeleteResponce(string ItemId)
        {
            IEnumerable<Responce> responces = await _responceRepository.GetAllResponcesByItem(ItemId);
            IEnumerable<ClapsToResponceOfUsers> clapsToResponceOfUsers = (await _responceOfUsersIterfaces
                .getClapsToResponceOfUsersByRespocnceList(responces.Select(r => r.ResponceId).ToList())).SelectMany(c => c.Value);

            await _responceOfUsersIterfaces.DeleteClapsToResponceEntityByClapsList(clapsToResponceOfUsers);
            await _responceRepository.DeleteResponceList(responces.ToList());
            await _db.SaveChangesAsync();
        }

    }
}
