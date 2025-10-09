using Microsoft.AspNetCore.Identity;
using UserDomain.Infrastructure.Database.Models;

namespace UserDomain.Application.UserCases.IdentityUserCases
{
    public class GetIdentityUserCase
    {
        private UserManager<UserModel> UserManager;

        public GetIdentityUserCase(UserManager<UserModel> userManager)
        {
            UserManager = userManager;
        }

        public async Task<IdentityUser?> Handle(string Id) 
        {
            return (IdentityUser) ( await UserManager.FindByIdAsync(Id));
        }
    }
}
