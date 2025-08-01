using UserDomain.Enum;

namespace UserDomain.Infrastructure.Database.Models
{
    public class UserMemberShipModel
    {
        public string UserWrapperId { get; set; }
        public UserWrapperModel UserWrapper { get; set; }

        public bool isActive { get; set; } = false;
        public UserMemberShipTypeEnum Type { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
    }
}
