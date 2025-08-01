namespace UserDomain.Domain
{
    public class UserProfile
    {

        public UserProfile(string UserWrapperId, string LogoUrl, string About)
        {
            this.UserWrapperId = UserWrapperId;
            this.LogoUrl = LogoUrl;
            this.About = About;
        }

        public string UserWrapperId { get; private set; }

        public string LogoUrl { get; private set; }
        public string About { get; private set; }


         
    }
}
