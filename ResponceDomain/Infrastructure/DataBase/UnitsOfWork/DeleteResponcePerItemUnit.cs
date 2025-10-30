using Microsoft.EntityFrameworkCore;
using ResponceDomain.Application.Interfaces;
using ResponceDomain.Application.UnitsOfWorks;
using ResponceDomain.Domain;
using ResponceDomain.Infrastructure.DataBase.DBContex;
using System.Data;

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
            await using var tx = await _db.Database.BeginTransactionAsync(IsolationLevel.Serializable);

            try
            {
                IEnumerable<Responce> responces = await _responceRepository.GetAllResponcesByItem(ItemId);
                IEnumerable<ClapsToResponceOfUsers> clapsToResponceOfUsers = (await _responceOfUsersIterfaces
                    .getClapsToResponceOfUsersByRespocnceList(responces.Select(r => r.ResponceId).ToList())).SelectMany(c => c.Value);

                await _responceOfUsersIterfaces.DeleteClapsToResponceEntityByClapsList(clapsToResponceOfUsers);
                await _responceRepository.DeleteResponceList(responces.ToList());
                await _db.SaveChangesAsync();
                tx.Commit();
            }
            catch (Exception ex) 
            {
                tx.Rollback();
                throw;
            }
        }

    }
}
