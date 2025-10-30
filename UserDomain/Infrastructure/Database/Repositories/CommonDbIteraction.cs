using AutoMapper;
using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using UserDomain.Application.Interfaces.Repository;
using UserDomain.Infrastructure.Database.DBContext;

namespace UserDomain.Infrastructure.Database.Repositories
{
    public class CommonDbIteraction<Model, Domain, PrimaryKeyType>(AppDbContext _db, IMapper _mappper) : IGenericDBRepository<Model, Domain, PrimaryKeyType> where Model : class , IPrimitiveModelKey where Domain : class
    {
        public Expression<Func<Model,bool>> WhereIdEqualsPrimaryKeyExpression(PrimaryKeyType id) 
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
        public virtual async Task AddAsync(Domain entity) 
        {
            var model = _mappper.Map<Model>(entity);
            await _db.Set<Model>().AddAsync(model);
        }

        public virtual async Task DeleteAsync(PrimaryKeyType id)
        {
            

            Model EntityToDelete = _db.Set<Model>().FirstOrDefault(m => m.UserWrapperId.Equals(id));

            if (EntityToDelete != null) 
            {
                _db.Set<Model>().Remove(EntityToDelete);
            }
        }

        public virtual async Task<Domain?> GetEntityAsync(PrimaryKeyType id)
        {
            Model getModel =   _db.Set<Model>().FirstOrDefault(m => m.UserWrapperId.Equals(id));     
            return _mappper.Map<Domain>(getModel);         
        }

        public virtual async Task UpdateAsync(Domain entity)
        {
            var Model = _mappper.Map<Model>(entity);
            _db.Set<Model>().Update(Model);
        }

        public virtual async Task<IEnumerable<Domain>> GetEntityListByListIdsAsync(IEnumerable<PrimaryKeyType> id)
        {
            if (typeof(PrimaryKeyType) == typeof(string))
            {
                IEnumerable<string> ids = id.Cast<string>();
                IEnumerable<Model> listOfModels = await _db.Set<Model>().Where(m => ids.Contains(m.UserWrapperId)).ToListAsync();
                return _mappper.Map<IEnumerable<Domain>>(listOfModels);
            }
            return null;
            
        }
    }
}
