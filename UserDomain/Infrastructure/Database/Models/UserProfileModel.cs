using UserDomain.Infrastructure.Database.DBContext;

namespace UserDomain.Infrastructure.Database.Models
{
    public class UserProfileModel : IPrimitiveModelKey
    { 
        public string UserWrapperId { get; set; }
        public UserWrapperModel User { get; set; }

        public string LogoUrl { get; set; }
        public string About { get; set; }
    }
}
