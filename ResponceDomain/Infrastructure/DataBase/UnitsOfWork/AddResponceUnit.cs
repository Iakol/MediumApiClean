using ResponceDomain.Application.Interfaces;
using ResponceDomain.Application.UnitsOfWorks;
using ResponceDomain.Domain;
using ResponceDomain.Infrastructure.DataBase.DBContex;

namespace ResponceDomain.Infrastructure.DataBase.UnitsOfWork
{
    public class AddResponceUnit : IAddResponceUnit
    {
        private readonly IResponceRepository _responceRepository;
        private readonly AppDBContext _db;

        public AddResponceUnit(IResponceRepository responceRepository, AppDBContext appDBContext)
        {
            _responceRepository = responceRepository;
            _db = appDBContext;
        }

        public async Task addResponce(Responce responce)
        {
            await _responceRepository.AddResponce(responce);
            await _db.SaveChangesAsync();  
        }
    }
}
