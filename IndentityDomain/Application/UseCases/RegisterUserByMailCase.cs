using IndentityDomain.Application.Enums;
using IndentityDomain.Application.Interfaces;
using IndentityDomain.Presentation.UserCases;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using IndentityDomain.Application.DTO;
using System.Windows.Input;

namespace IndentityDomain.Application.UseCases
{
    public class RegisterUserByMailCase : IRegisterUserByMailCase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDistributedCache _cache;

        public RegisterUserByMailCase(UserManager<IdentityUser> userManager, IDistributedCache cache)
        {
            _userManager = userManager;
            _cache = cache;
        }

        public async Task<Result> Handle(string Mail,string code)
        {
            IdentityUser userToRegister = await _userManager.FindByEmailAsync(Mail);

            if (userToRegister != null)
            {
                string Verificationcode = await _cache.GetStringAsync($"{IdentityCommandEnum.Register.ToString()}_code:{Mail}");

                if (Verificationcode.Equals(code))
                {
                    await _userManager.CreateAsync(new IdentityUser { Email = Mail });
                    await _cache.RemoveAsync($"{IdentityCommandEnum.Register.ToString()}_code:{Mail}");
                    return Result.Success();
                }

                return Result.Failure("Wrong Code");
            }

            return Result.Failure("User Already Register"); //redirect to Login 
        }
    }
}
