using ResponceDomain.Domain;
using ResponceDomain.Presentation.UseCases;

namespace ResponceDomain.Application.Interfaces
{
    public interface IClapsToResponceOfUsersIterfaces
    {

        public Task<ClapsToResponceOfUsers> getClapsToResponceOfUsers(int responceId, string userId);
        public Task<Dictionary<int, List<ClapsToResponceOfUsers>>> getClapsToResponceOfUsersByRespocnceList(List<int> responceIds );
        public Task<Dictionary<int, int>> getClapsCountToResponceOfUsersByRespocnceList(List<int> responceIds);

        public Task UpdateClapsToResponce(ClapsToResponceOfUsers clapsToResponceOfUsers);

        public Task AddClapsToResponceEntity(ClapsToResponceOfUsers clapsToResponceOfUsers);

        public Task DeleteClapsToResponceEntityByClapsList(IEnumerable<ClapsToResponceOfUsers> claps);

    }
}
