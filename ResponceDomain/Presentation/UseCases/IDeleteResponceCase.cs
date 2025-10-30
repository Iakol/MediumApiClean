using ResponceDomain.Application.DTO;

namespace ResponceDomain.Presentation.UseCases
{
    public interface IDeleteResponceCase
    {
        public Task<Result> Handle(int ResponceId, string userId);
    }
}
