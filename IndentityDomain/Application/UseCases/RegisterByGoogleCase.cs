using Google.Apis.Auth;
using IndentityDomain.Application.DTO;
using IndentityDomain.Presentation.UserCases;
using Microsoft.AspNetCore.Identity;

namespace IndentityDomain.Application.UseCases
{
    public class RegisterByGoogleCase : IRegisterByGoogleCase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public async Task<Result> Handle(string idToken)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
            IdentityUser user = await _userManager.FindByEmailAsync(payload.Email);

            if (user == null)
            {
                _userManager.CreateAsync(new IdentityUser() { Email = payload.Email });
                return Result.Success();
            }
            else
            {
                return Result.Failure("UserAlreadyRegisterd");
            }


        }

    }
}
