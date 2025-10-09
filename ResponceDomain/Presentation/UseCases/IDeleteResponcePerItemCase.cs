using ResponceDomain.Application.DTO;

namespace ResponceDomain.Presentation.UseCases
{
    public interface IDeleteResponcePerItemCase
    {
        public Task<Result> Handle(string itemId);
    }
}
