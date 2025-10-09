using ResponceDomain.Application.DTO;

namespace ResponceDomain.Presentation.UseCases
{
    public interface IGetResponcesForItem
    {
        public Task<Result<List<ResponceDTO>>> Handle(string itemId);
    }
}
