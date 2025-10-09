using Azure;
using ResponceDomain.Application.DTO;
using ResponceDomain.Domain;

namespace ResponceDomain.Application.Services
{
    public class CreateResponceTreeByResponceTreeBuilder
    {
        public IEnumerable<ResponceDTO> BuildTree(IEnumerable<Responce> allResponces, IReadOnlyDictionary<int, int> ClapsCount)
        {
            IEnumerable<ResponceDTO> BaseResponcesByItem = allResponces.Where(r => r.BaseResponseId == null)
                    .Select(r => new ResponceDTO
                    {
                        Id = r.ResponceId,
                        ClapsCount = ClapsCount.TryGetValue(r.ResponceId, out var claps) ? claps : 0,
                        UserId = r.UserId,
                        TextOfReply = r.TextOfReply,
                        SubResponces = BuildSubTree(r.ResponceId, allResponces, ClapsCount).ToList(),
                        CreateAt = r.CreateAt,
                        BaseResponseId = r.BaseResponseId
                    });

            return BaseResponcesByItem;
        }



        private IEnumerable<ResponceDTO> BuildSubTree(int BaseresponceId, IEnumerable<Responce> allResponces, IReadOnlyDictionary<int, int> ClapsCount) 
        {
            HashSet<Responce> Childresponces = allResponces.Where(r => r.BaseResponseId == BaseresponceId).ToHashSet<Responce>();

            IEnumerable<ResponceDTO> childrensDTO = Childresponces.Select(r => new ResponceDTO
            {
                Id = r.ResponceId,
                ClapsCount = ClapsCount.TryGetValue(r.ResponceId, out var claps) ? claps : 0,
                UserId = r.UserId,
                TextOfReply = r.TextOfReply,
                SubResponces = BuildSubTree(r.ResponceId, allResponces, ClapsCount).ToList(),
                CreateAt = r.CreateAt,
                BaseResponseId = r.BaseResponseId,

            });
            return childrensDTO;
        }
    }
}
