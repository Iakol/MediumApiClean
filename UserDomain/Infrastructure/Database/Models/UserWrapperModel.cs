namespace UserDomain.Infrastructure.Database.Models
{
    public class UserWrapperModel
    {
        public string UserId { get; set; }
        public UserModel User { get; set; }
        public UserProfileModel Profile { get; set; }

        //public string? AboutContentId { get; set; }

        public UserMemberShipModel UserMemberShip { get; set; }
        public string Tag { get; set; } // nedd migrate

    }
}
