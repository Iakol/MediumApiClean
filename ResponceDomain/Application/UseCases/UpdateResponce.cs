using ResponceDomain.Application.DTO;
using ResponceDomain.Application.Interfaces;
using ResponceDomain.Application.UnitsOfWorks;
using ResponceDomain.Domain;
using ResponceDomain.Presentation.Comand;
using ResponceDomain.Presentation.UseCases;

namespace ResponceDomain.Application.UseCases
{
    public class UpdateResponce : IUpdateResponce
    {
        private readonly IUpdateResponceUnit _updateResponceUnit;
        private readonly IResponceRepository _responceRepository;

        public UpdateResponce(IUpdateResponceUnit updateResponceUnit, IResponceRepository responceRepository)
        {
            _updateResponceUnit = updateResponceUnit;
            _responceRepository = responceRepository;
        }

        public async Task<Result> Handle(UpdateResponceCommandData updateData, string userId)
        {
            if (string.IsNullOrWhiteSpace(updateData.newText))
            {
                return Result.Failure("New text can`t be empty");
            }
            try
            {
                Responce responceToUpdate = await _responceRepository.GetResponceById(updateData.responceId);
                if (responceToUpdate != null)
                {
                    if (responceToUpdate.UserId.Equals(userId))
                    {
                        responceToUpdate.TextOfReply = updateData.newText;
                        await _updateResponceUnit.UpdateResponce(responceToUpdate);
                        return Result.Success();
                    }
                    return Result.Failure("User is not owner of responce");
                }
                return Result.Failure("Responce is not Exist");
            }
            catch (Exception ex) 
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
