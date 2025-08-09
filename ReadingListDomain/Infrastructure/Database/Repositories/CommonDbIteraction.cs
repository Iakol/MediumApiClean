using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadingListDomain.Application.Interfaces;
using ReadingListDomain.Infrastructure.Database.DBContext;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ReadingListDomain.Infrastructure.Database.Repositories
{
    public class CommonDbIteraction<Model, Domain> : IGenericDBRepository<Model, Domain> where Model : class where Domain : class
    {
        protected readonly IMapper _mapper;
        protected readonly AppDBContext _db;
        protected readonly DbSet<Model> Models;

        public CommonDbIteraction(IMapper mapper, AppDBContext db)
        {
            _mapper = mapper;
            _db = db;
            Models =  _db.Set<Model>();

        }

        public Expression<Func<Model, bool>> WhereIdEqualsPrimaryKeyExpression(string id)
        {
            var ModelType = _db.Model.FindEntityType(typeof(Model));
            var primaryKey = ModelType.FindPrimaryKey();

            var KeyParametr = primaryKey.Properties[0];
            var KeyName = KeyParametr.Name;

            var expresionPatament = Expression.Parameter(typeof(Model), "e");

            var property = Expression.Property(expresionPatament, KeyName);
            var idValue = Expression.Constant(id);

            var equals = Expression.Equal(property, idValue);

            var lambda = Expression.Lambda<Func<Model, bool>>(equals, expresionPatament);
            return lambda;
        }

        public async Task AddAsync(Domain entity)
        {
            Model model = _mapper.Map<Model>(entity);
            await Models.AddAsync(model);
        }

        public async Task DeleteAsync(string id)
        {
            Model entity = await Models.FirstOrDefaultAsync(WhereIdEqualsPrimaryKeyExpression(id));

            if (entity != null) 
            {
                Models.Remove(entity);
            }
        }

        public async Task<Domain?> GetEntityAsync(string id)
        {
            return _mapper.Map<Domain>( await Models.FirstOrDefaultAsync(WhereIdEqualsPrimaryKeyExpression(id)));
        }

        public async Task UpdateAsync(Domain entity)
        {
            Model model =_mapper.Map<Model>(entity);
            Models.Update(model);

        }
    }
}
