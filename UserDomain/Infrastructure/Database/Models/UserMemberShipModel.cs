using UserDomain.Enum;
using UserDomain.Infrastructure.Database.DBContext;

namespace UserDomain.Infrastructure.Database.Models
{
    public class UserMemberShipModel : IPrimitiveModelKey
    {
        public string UserWrapperId { get; set; }
        public UserWrapperModel UserWrapper { get; set; }

        public bool isActive { get; set; } = false;
        public UserMemberShipTypeEnum Type { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
    }
}
