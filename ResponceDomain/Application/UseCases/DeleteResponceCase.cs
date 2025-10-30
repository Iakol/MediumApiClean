
using ResponceDomain.Application.DTO;
using ResponceDomain.Application.Interfaces;
using ResponceDomain.Application.UnitsOfWorks;
using ResponceDomain.Infrastructure.DataBase.DBContex;
using ResponceDomain.Presentation.UseCases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
namespace ResponceDomain.Application.UseCases
{
    public class DeleteResponceCase : IDeleteResponceCase
    {
        private readonly IResponceRepository _responceRepository;
        private readonly IDeleteResponceUnit _deleteResponceUnit;
        private readonly AppDBContext _db;
        public DeleteResponceCase(IResponceRepository responceRepository, IDeleteResponceUnit deleteResponceUnit, AppDBContext appDBContext)
        {
            _responceRepository = responceRepository;
            _deleteResponceUnit = deleteResponceUnit;
            _db = appDBContext;

        }

        public async Task<Result> Handle(int ResponceId, string userId)
        {

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Result.Failure("User isn`t authorize");
            }

            try
            {

                if (_responceRepository.GetResponceById(ResponceId) == null) 
                {
                    return Result.Failure("Responce is not exist");

                }
                await _deleteResponceUnit.DeleteResponce(ResponceId);
                return Result.Success();
            }
            catch (Exception ex) 
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
