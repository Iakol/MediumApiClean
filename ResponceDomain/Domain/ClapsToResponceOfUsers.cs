namespace ResponceDomain.Domain
{
    public class ClapsToResponceOfUsers
    {
        public int ResponceId { get; set; }

        public string UserId { get; set; }

        private int _ClapsCount { get; set; }
        public int ClapsCount
        {
            get => _ClapsCount; set
            {
                if (value > 50)
                {
                    _ClapsCount = 50;
                }
                else if (value < _ClapsCount) 
                {
                    
                }
                else
                {
                    _ClapsCount = value;

                }
            }
        }

        public ClapsToResponceOfUsers(int responceId, string userId, int clapsCount)
        {
            ResponceId = responceId;
            UserId = userId;
            ClapsCount = clapsCount;
        }
    }
}
