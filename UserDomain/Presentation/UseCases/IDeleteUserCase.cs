using UserDomain.Application.DTO;

namespace UserDomain.Presentation.UseCases
{
    public interface IDeleteUserCase
    {
        public Task<Result> Handle(string UserId);
    }
}
