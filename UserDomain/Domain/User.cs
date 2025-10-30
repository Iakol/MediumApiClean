using Microsoft.AspNetCore.Identity;

namespace UserDomain.Domain
{
    public class User : IdentityUser
    {
        public User() { }
        public User(string UserWrapperId) {
            this.UserWrapperId = UserWrapperId;
        }
        private string _UserWrapperId { get; set; }

        public string UserWrapperId {
            get => _UserWrapperId;
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _UserWrapperId = value;
            }
                
        }
    }
}


