using IndentityDomain.Application.Enums;
using IndentityDomain.Application.DTO;

namespace IndentityDomain.Presentation.UserCases
{
    public interface ISendAuthCodeCase
    {
        public Task<Result> Handle(string mail, IdentityCommandEnum commnd);
    }
}
