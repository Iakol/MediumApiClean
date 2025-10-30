using Microsoft.AspNetCore.Identity;
using UserDomain.Infrastructure.Database.DBContext;

namespace UserDomain.Infrastructure.Database.Models
{
    public class UserModel : IdentityUser , IPrimitiveModelKey
    {
        public string UserWrapperId { get; set; }
        public UserWrapperModel UserWrapper { get; set; }


    }
}
