using ResponceDomain.Application.DTO;
using ResponceDomain.Application.UnitsOfWorks;
using ResponceDomain.Presentation.UseCases;

namespace ResponceDomain.Application.UseCases
{
    public class DeleteResponcePerItemCase : IDeleteResponcePerItemCase
    {
        IDeleteResponcePerItemUnit _deleteResponcePerItemUnit;

        public DeleteResponcePerItemCase(IDeleteResponcePerItemUnit deleteResponcePerItemUnit)
        {
            _deleteResponcePerItemUnit = deleteResponcePerItemUnit;
        }

        public async Task<Result> Handle(string itemId)
        {
            if (string.IsNullOrWhiteSpace(itemId))
            {
                return Result.Failure("Item id is null");
            }

            try
            {
                await _deleteResponcePerItemUnit.DeleteResponce(itemId);
                return Result.Success();
            }
            catch (Exception ex) 
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
