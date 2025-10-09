using ResponceDomain.Application.Interfaces;
using ResponceDomain.Application.UnitsOfWorks;
using ResponceDomain.Domain;
using ResponceDomain.Infrastructure.DataBase.DBContex;
using ResponceDomain.Infrastructure.DataBase.Repositories;

namespace ResponceDomain.Infrastructure.DataBase.UnitsOfWork
{
    public class UpdateResponceUnit : IUpdateResponceUnit
    {
        IResponceRepository _responceRepository;
        AppDBContext _db;
        public UpdateResponceUnit(IResponceRepository responceRepository, AppDBContext db) 
        {
            _responceRepository = responceRepository;
            _db = db;
        }

        public async Task UpdateResponce(Responce responce)
        {
            await _responceRepository.UpdateTextOfResponce(responce);
            _db.SaveChangesAsync();
        }
    }
}
