using Microsoft.AspNetCore.Identity;

namespace UserDomain.Infrastructure.Database.Models
{
    public class UserModel : IdentityUser
    {
        public string UserWrapperId { get; set; }
        public UserWrapperModel UserWrapper { get; set; }


    }
}
