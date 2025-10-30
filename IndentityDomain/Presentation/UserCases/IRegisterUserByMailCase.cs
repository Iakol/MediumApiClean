using IndentityDomain.Application.DTO;

namespace IndentityDomain.Presentation.UserCases
{
    public interface IRegisterUserByMailCase
    {
        public Task<Result> Handle(string Mail, string code);

    }
}
