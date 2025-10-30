using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ResponceDomain.Application.Interfaces;
using ResponceDomain.Application.UnitsOfWorks;
using ResponceDomain.Domain;
using ResponceDomain.Infrastructure.DataBase.DBContex;


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
            await using  IDbContextTransaction tx = await _db.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);

            try
            {

                IEnumerable<int> ids = await _responceRepository.GetTreeFlatListOfResponceIDsByParent(ParentId);

                IEnumerable<ClapsToResponceOfUsers> claps = (await _clapsToResponceOfUsersIterfaces.getClapsToResponceOfUsersByRespocnceList(ids.ToList())).SelectMany(r => r.Value);

                await _clapsToResponceOfUsersIterfaces.DeleteClapsToResponceEntityByClapsList(claps);
                await _responceRepository.DeleteResponceList(ids.ToList());

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
