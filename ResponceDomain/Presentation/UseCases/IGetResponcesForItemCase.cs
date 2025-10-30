using ResponceDomain.Application.DTO;

namespace ResponceDomain.Presentation.UseCases
{
    public interface IGetResponcesForItemCase
    {
        public Task<Result<List<ResponceDTO>>> Handle(string itemId);
    }
}
