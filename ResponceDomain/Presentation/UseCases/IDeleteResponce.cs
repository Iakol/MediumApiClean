using ResponceDomain.Application.DTO;

namespace ResponceDomain.Presentation.UseCases
{
    public interface IDeleteResponce
    {
        public Task<Result> Handle(int ResponceId, string userId);
    }
}
