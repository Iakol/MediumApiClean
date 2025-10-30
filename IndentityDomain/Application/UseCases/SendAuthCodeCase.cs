using IndentityDomain.Application.Enums;
using IndentityDomain.Application.Interfaces;
using IndentityDomain.Presentation.UserCases;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.OpenApi.Extensions;
using IndentityDomain.Application.DTO;

namespace IndentityDomain.Application.UseCases
{
    public class SendAuthCodeCase : ISendAuthCodeCase
    {
        private readonly IEmailSendler _emailSendler;
        private readonly IDistributedCache _cache;
        private readonly UserManager<IdentityUser> _userManager;

        public SendAuthCodeCase(IEmailSendler emailSendler, IDistributedCache cache, UserManager<IdentityUser>  userManager ) 
        {
            _emailSendler = emailSendler;
            _cache = cache;
            _userManager = userManager;
        }
        public async Task<Result> Handle(string mail, IdentityCommandEnum commnd)
        {
            IdentityUser userToAuth = await _userManager.FindByEmailAsync(mail);


            if (commnd == IdentityCommandEnum.Login) 
            {
                if (userToAuth == null) 
                {
                    return Result.Failure("User didn`t register");
                }
            }
            if (commnd == IdentityCommandEnum.Register)
            {
                if (userToAuth != null)
                {
                    return Result.Failure("User registered");
                }
            }

            string code = new Random().Next(100000, 999999).ToString();
            await _emailSendler.SendEmail(mail, commnd.ToString(), code);


            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };
            await _cache.SetStringAsync($"{commnd.ToString()}_code:{mail}", code, options);

            return Result.Success();
        }
    }
}

