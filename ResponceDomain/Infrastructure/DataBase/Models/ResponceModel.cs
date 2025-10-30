using ResponceDomain.Application.Enum;
using ResponceDomain.Domain;

namespace ResponceDomain.Infrastructure.DataBase.Models
{
    public class ResponceModel
    {
        public int ResponceId { get; set; }
        public string UserId { get; set; }
        public string ReadId { get; set; }

        public string TextOfReply { get; set; }

        public ReaderTypeEnum ReaderTypeEnum { get; set; }
        public DateTime CreateAt { get; set; }


        public List<ClapsToResponceOfUsersModel> ClapsToResponceOfUsersModels { get; set; }
        public List<ResponceModel> ChildResponses { get; set; }

        public int? BaseResponseId { get; set; }
        public ResponceModel? BaseResponce { get;set ;}
    }
}
