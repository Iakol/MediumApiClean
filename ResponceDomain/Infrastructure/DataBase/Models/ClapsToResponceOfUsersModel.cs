namespace ResponceDomain.Infrastructure.DataBase.Models
{
    public class ClapsToResponceOfUsersModel
    {
        public int ResponceId { get; set; }

        public string UserId { get; set; }
        public int ClapsCount { get; set; }
        public ResponceModel Responce { get; set; }
    }
}
