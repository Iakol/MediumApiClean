using ResponceDomain.Application.DTO;
using ResponceDomain.Application.Interfaces;
using ResponceDomain.Application.UnitsOfWorks;
using ResponceDomain.Infrastructure.DataBase.DBContex;
using ResponceDomain.Presentation.UseCases;

namespace ResponceDomain.Application.UseCases
{
    public class DeleteResponceCase : IDeleteResponce
    {
        private readonly IResponceRepository _responceRepository;
        private readonly IDeleteResponceUnit _deleteResponceUnit;
        public DeleteResponceCase(IResponceRepository responceRepository, IDeleteResponceUnit deleteResponceUnit)
        {
            _responceRepository = responceRepository;
            _deleteResponceUnit = deleteResponceUnit;


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
