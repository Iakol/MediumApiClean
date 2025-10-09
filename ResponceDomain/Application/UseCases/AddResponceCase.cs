using ResponceDomain.Application.DTO;
using ResponceDomain.Application.Enum;
using ResponceDomain.Application.Interfaces;
using ResponceDomain.Application.UnitsOfWorks;
using ResponceDomain.Domain;
using ResponceDomain.Presentation.Comand;
using ResponceDomain.Presentation.UseCases;

namespace ResponceDomain.Application.UseCases
{
    public class AddResponceCase : IAddResponce
    {
        private readonly IAddResponceUnit _addResponceUnit;

        public AddResponceCase(IAddResponceUnit addResponceUnit)
        {
            _addResponceUnit = addResponceUnit;
        }

        public async Task<Result> Handle(CreateResponceCommandData NewResponce, string userId)
        {
            if (string.IsNullOrWhiteSpace(NewResponce.text))
            {
                return Result.Failure("Text of Responce can`t be empty");

            }
            if (string.IsNullOrWhiteSpace(NewResponce.itemId))
            {
                return Result.Failure("Item can`t be null");

            }

            try
            {
                await _addResponceUnit.addResponce(new Responce(userId, NewResponce.itemId, NewResponce.text, NewResponce.type));
                return Result.Success();
            }
            catch (Exception ex) 
            {
                return Result.Failure(ex.Message);
            }
        }


    }
}
