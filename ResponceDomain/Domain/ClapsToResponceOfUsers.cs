namespace ResponceDomain.Domain
{
    public class ClapsToResponceOfUsers
    {
        public int ResponceId { get; set; }

        public string UserId { get; set; }
        public int ClapsCount { get; set; }

        public ClapsToResponceOfUsers(int responceId, string userId, int clapsCount)
        {
            ResponceId = responceId;
            UserId = userId;
            ClapsCount = clapsCount;
        }
    }
}
