using UserDomain.Application.DTO;
using UserDomain.Domain;

namespace UserDomain.Presentation.UseCases
{
    public interface IGetIdentityUserCase
    {
        public Task<Result<User>> Handle(string id);
    }
}
