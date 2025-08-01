namespace UserDomain.Infrastructure.Database.Models
{
    public class UserProfileModel
    {
        public string UserWrapperId { get; set; }
        public UserWrapperModel User { get; set; }

        public string LogoUrl { get; set; }
        public string About { get; set; }
    }
}
