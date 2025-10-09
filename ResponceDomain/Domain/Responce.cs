using ResponceDomain.Application.Enum;

namespace ResponceDomain.Domain
{
    public class Responce
    {
        public int ResponceId { get; set; }
        public string UserId { get; set; }
        public string ReadId { get; set; }

        public string TextOfReply { get; set; }

        public DateTime CreateAt { get; set; }

        public ReaderTypeEnum ReaderTypeEnum { get; set; }
        public int? BaseResponseId { get; set; }
        public List<Responce> ChildeResponses { get; set; }


        public Responce()
        {

        }

        public Responce(string userId, string readId, string textOfReply, ReaderTypeEnum readerTypeEnum)
        {
            UserId = userId;
            ReadId = readId;
            TextOfReply = textOfReply;
            ReaderTypeEnum = readerTypeEnum;
            CreateAt = DateTime.Now;    
        }
    }


}


