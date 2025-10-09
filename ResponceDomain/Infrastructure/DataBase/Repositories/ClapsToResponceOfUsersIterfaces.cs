using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ResponceDomain.Application.Interfaces;
using ResponceDomain.Domain;
using ResponceDomain.Infrastructure.DataBase.DBContex;
using ResponceDomain.Infrastructure.DataBase.Models;
using System.Collections.Concurrent;

namespace ResponceDomain.Infrastructure.DataBase.Repositories
{
    public class ClapsToResponceOfUsersIterfaces : IClapsToResponceOfUsersIterfaces
    {
        private readonly AppDBContext _db;
        private readonly IMapper _mapper;
        public Task AddClapsToResponceEntity(ClapsToResponceOfUsers clapsToResponceOfUsers)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteClapsToResponceEntityByClapsList(IEnumerable<ClapsToResponceOfUsers> claps)
        {
            IEnumerable<ClapsToResponceOfUsersModel> clapsModel = _mapper.Map<IEnumerable<ClapsToResponceOfUsersModel>>(claps);

            _db.Claps.RemoveRange(clapsModel);
        }

        public Task DeleteClapsToResponceEntityByResponce(int responceId)
        {
            throw new NotImplementedException();
        }

        public Task<ClapsToResponceOfUsers> getAllClapsToResponceOfUsers(int responceId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetClapsCount(int responceId)
        {
            throw new NotImplementedException();
        }

        public async Task<Dictionary<int, int>> getClapsCountToResponceOfUsersByRespocnceList(List<int> responceIds)
        {
            Dictionary<int, int> ClapsIdToSum = await _db.Claps.Where(c => responceIds.Contains(c.ResponceId))
                .GroupBy(c => c.ResponceId)
                .Select(g => new { g.Key, Count = g.Sum(x => x.ClapsCount) })
                .ToDictionaryAsync(x => x.Key, x => x.Count);


            return ClapsIdToSum;

        }

        public Task<ClapsToResponceOfUsers> getClapsToResponceOfUsers(int responceId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<int, List<ClapsToResponceOfUsers>>> getClapsToResponceOfUsersByRespocnceList(List<int> responceIds)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateClapsToResponce(ClapsToResponceOfUsers clapsToResponceOfUsers)
        {
            await _mapper.Map(clapsToResponceOfUsers, _db.Responces.FindAsync(clapsToResponceOfUsers.ResponceId));
        }
    }
}
