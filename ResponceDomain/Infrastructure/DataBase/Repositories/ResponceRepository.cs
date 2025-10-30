using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ResponceDomain.Application.Interfaces;
using ResponceDomain.Domain;
using ResponceDomain.Infrastructure.DataBase.DBContex;
using ResponceDomain.Infrastructure.DataBase.Models;
using System.Linq;

namespace ResponceDomain.Infrastructure.DataBase.Repositories
{
    public class ResponceRepository : IResponceRepository
    {
        private readonly AppDBContext _db;
        private readonly IMapper _mapper;

        public ResponceRepository(AppDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task AddResponce(Responce responce)
        {
            ResponceModel responceToAdd = _mapper.Map<ResponceModel>(responce);
            await _db.AddAsync(responceToAdd);
        }

        public async Task DeleteResponceList(List<int> responcesList)
        {
            await _db.Responces.Where(r => responcesList.Contains(r.ResponceId)).ExecuteDeleteAsync();
        }

        public async Task DeleteResponceList(List<Responce> responcesList)
        {
            await _db.Responces.Where(r => responcesList.Select(rd => rd.ResponceId).Contains(r.ResponceId)).ExecuteDeleteAsync();

        }


        public async Task<IEnumerable<Responce>> GetAllResponcesByItem(string itemId)
        {
            IEnumerable<ResponceModel> responcesModels = await _db.Responces.Where(r => r.ReadId.Equals(itemId)).ToListAsync();
            return _mapper.Map<IEnumerable<Responce>>(responcesModels);
        }

        public async Task<Responce?> GetResponceById(int id)
        {
            ResponceModel? responce = await _db.Responces.FirstOrDefaultAsync(r => r.ResponceId == id);
            return _mapper.Map<Responce>(responce);

        }

        public async Task<IEnumerable<int>> GetTreeFlatListOfResponceIDsByParent(int Parentid)
        {
            IEnumerable<int> flatTree = await _db.Database
                                .SqlQueryRaw<int>(@"
                                WITH RecursiveCTE AS (
                                    SELECT * 
                                    FROM Responces 
                                    WHERE ResponceId = {0}
                                    UNION ALL
                                    SELECT r.*
                                    FROM Responces r
                                    INNER JOIN RecursiveCTE rc ON r.BaseResponseId = rc.ResponceId
                                )
                                SELECT * FROM RecursiveCTE
                                ", Parentid).ToListAsync();


            return flatTree;
        }

        public async Task UpdateTextOfResponce(Responce UpdatedDomainResponce)
        {
            _mapper.Map(UpdatedDomainResponce, await _db.Responces.FindAsync(UpdatedDomainResponce.ResponceId));
        }
    }
}
