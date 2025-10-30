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
        public ClapsToResponceOfUsersIterfaces(AppDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task AddClapsToResponceEntity(ClapsToResponceOfUsers clapsToResponceOfUsers)
        {
            ClapsToResponceOfUsersModel newClaps = _mapper.Map<ClapsToResponceOfUsersModel>(clapsToResponceOfUsers);
            await _db.Claps.AddAsync(newClaps);
        }

        public async Task DeleteClapsToResponceEntityByClapsList(IEnumerable<ClapsToResponceOfUsers> claps)
        {
            IEnumerable<ClapsToResponceOfUsersModel> clapsModel = _mapper.Map<IEnumerable<ClapsToResponceOfUsersModel>>(claps);

            _db.Claps.RemoveRange(clapsModel);
        }


        public async Task<Dictionary<int, int>> getClapsCountToResponceOfUsersByRespocnceList(List<int> responceIds)
        {
            Dictionary<int, int> ClapsIdToSum = await _db.Claps.Where(c => responceIds.Contains(c.ResponceId))
                .GroupBy(c => c.ResponceId)
                .Select(g => new { g.Key, Count = g.Sum(x => x.ClapsCount) })
                .ToDictionaryAsync(x => x.Key, x => x.Count);


            return ClapsIdToSum;

        }

        public async Task<ClapsToResponceOfUsers> getClapsToResponceOfUsers(int responceId, string userId)
        {
            ClapsToResponceOfUsersModel clapsModel = await _db.Claps.FirstOrDefaultAsync(c => (c.ResponceId == responceId && c.UserId.Equals(userId)));
            return _mapper.Map<ClapsToResponceOfUsers>(clapsModel);
        }

        public async Task<Dictionary<int, List<ClapsToResponceOfUsers>>> getClapsToResponceOfUsersByRespocnceList(List<int> responceIds)
        {
            List<ClapsToResponceOfUsersModel> AllClapsList = await _db.Claps.Where(c => responceIds.Contains(c.ResponceId)).ToListAsync();
            List<ClapsToResponceOfUsers> Domains = _mapper.Map<List<ClapsToResponceOfUsers>>(AllClapsList);

            return Domains.GroupBy(x => x.ResponceId).ToDictionary(x => x.Key, x => x.ToList());


        }

        public async Task UpdateClapsToResponce(ClapsToResponceOfUsers clapsToResponceOfUsers)
        {
            ClapsToResponceOfUsersModel clapstoUpdate = await _db.Claps.FindAsync(clapsToResponceOfUsers.UserId, clapsToResponceOfUsers.ResponceId);
            _mapper.Map(clapsToResponceOfUsers, clapstoUpdate);
        }
    }
}
