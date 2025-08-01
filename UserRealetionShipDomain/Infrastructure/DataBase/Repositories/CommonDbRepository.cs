using AutoMapper;
using UserRealetionShipDomain.Infrastructure.DataBase.DBContext;
using UserRealetionShipDomain.Application.Interfaces;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace UserRealetionShipDomain.Infrastructure.DataBase.Repositories
{
    public class CommonDbRepository<Model, Domain>(AppDbContext _db, IMapper _mappper) : IGenericDBRepository<Model, Domain> where Model : class where Domain : class
    {
        private Expression<Func<Model, bool>> WhereIdEqualsPrimaryKeyExpression(string Followid, string subcsriptionid)
        {
            var ModelType = _db.Model.FindEntityType(typeof(Model));
            var primaryKey = ModelType.FindPrimaryKey();

            var KeyParametrCompositeFirst = primaryKey.Properties[0];
            var KeyParametrCompositeSecond = primaryKey.Properties[1];

            var KeyNameFitst = KeyParametrCompositeFirst.Name;
            var KeyNameSecond = KeyParametrCompositeSecond.Name;

            var expresionPatament = Expression.Parameter(typeof(Model), "e");

            var propertyOne = Expression.Property(expresionPatament, KeyNameFitst);
            var propertySecond = Expression.Property(expresionPatament, KeyNameSecond);

            var FitstComposite = Expression.Constant(Followid);
            var SecondComposite = Expression.Constant(subcsriptionid);


            var equalsFirst = Expression.Equal(propertyOne, FitstComposite);
            var equalsSecond = Expression.Equal(propertySecond, SecondComposite);
            var ex = Expression.AndAlso(equalsFirst, equalsSecond);

            var lambda = Expression.Lambda<Func<Model, bool>>(ex, expresionPatament);
            return lambda;
        }

        private Expression<Func<Model, bool>> WhereIdEqualsPrimaryKeyExpression(string id)
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
            await _db.Set<Model>().AddAsync(_mappper.Map<Model>(entity));
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(string Followid, string subcsriptionid)
        {
            Model entity = await _db.Set<Model>().FirstOrDefaultAsync(WhereIdEqualsPrimaryKeyExpression(Followid, subcsriptionid));
            _db.Set<Model>().Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<Domain?> GetEntityAsync(string Followid, string subcsriptionid)
        {
            var entity = await _db.Set<Model>().FirstOrDefaultAsync(WhereIdEqualsPrimaryKeyExpression(Followid, subcsriptionid));
            return _mappper.Map<Domain>(entity);
        }

        public async Task<List<Domain>> GetEntityListByFollowIdAsync(string Followid)
        {
            return await _db.Set<Model>().Where(WhereIdEqualsPrimaryKeyExpression(Followid)).Select(m => _mappper.Map<Domain>(m)).ToListAsync();
        }

   
    }
}
