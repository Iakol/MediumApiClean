using UserDomain.Presentation.UseCases;

namespace UserDomain.Application.UserCases
{
    public class UpdateAboutUserCase : IUpdateAboutUserCase
    {
        public Task Handle(string UserId, string newAboutUser)
        {
            throw new NotImplementedException();
        }
    }
}
