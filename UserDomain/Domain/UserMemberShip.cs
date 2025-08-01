using UserDomain.Enum;

namespace UserDomain.Domain
{
    public class UserMemberShip
    {

        public UserMemberShip(string userWrapperId, bool isActive, UserMemberShipTypeEnum type, DateTime? startAt, DateTime? endAt)
        {
            UserWrapperId = userWrapperId;
            this.isActive = isActive;
            Type = type;
            StartAt = startAt;
            EndAt = endAt;
        }

        public string UserWrapperId { get; private set; }

        public bool isActive { get; private set; } = false;
        public UserMemberShipTypeEnum Type { get; private set; }
        public DateTime? StartAt { get; private set; }
        public DateTime? EndAt { get; private set; }

        public void SetMontlyMemberShip() 
        {
            Type = UserMemberShipTypeEnum.Premium;
            StartAt = DateTime.Now;
            EndAt = DateTime.Now.AddMonths(1);
        }
        public void SetYearlyMemberShip()
        {
            Type = UserMemberShipTypeEnum.Premium;
            StartAt = DateTime.Now;
            EndAt = DateTime.Now.AddYears(1);
        }


    }
}
