using UserDomain.Application.DTO;

namespace UserDomain.Presentation.UseCases
{
    public interface ICreateRegisterUserCase
    {
        public Task<Result> Handle(string Email,string UserId);
        


    }
}
