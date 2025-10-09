namespace ResponceDomain.Application.DTO
{
    public class ResponceDTO
    {
        public int Id { get; set; }

        public int ClapsCount { get; set; }

        public string UserId { get; set; }

        public string TextOfReply { get; set; }
        public List<ResponceDTO> SubResponces { get; set; }

        public int? BaseResponseId { get; set; }
        public DateTime CreateAt { get; set; }

    }
}
