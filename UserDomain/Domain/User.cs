using Microsoft.AspNetCore.Identity;

namespace UserDomain.Domain
{
    public class User : IdentityUser
    {
        public User(string UserWrapperId) {
            this.UserWrapperId = UserWrapperId;
        }
        public string UserWrapperId { get; private set; }


    }
}

