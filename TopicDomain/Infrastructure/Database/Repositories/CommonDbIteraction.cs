using AutoMapper;

using System.Linq.Expressions;
using TopicDomain.Application.Interfaces;
using TopicDomain.Infrastructure.Database.DBContext;


namespace TopicDomain.Infrastructure.Database.Repositories
{
    public class CommonDbIteraction<Model, Domain> : IGenericDBRepository<Model, Domain> where Model : class where Domain : class
    {
        protected readonly IMapper _mapper;
        protected readonly AppDBContext _db;

        public CommonDbIteraction( IMapper mapper, AppDBContext db)
        {
            _mapper = mapper;
            _db = db;
        }

        public Expression<Func<Model,bool>> WhereIdEqualsPrimaryKeyExpression(string id) 
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
            var model = _mapper.Map<Model>(entity);
            await _db.Set<Model>().AddAsync(model);
        }

        public async Task DeleteAsync(string id)
        {
            

            Model EntityToDelete = _db.Set<Model>().FirstOrDefault(WhereIdEqualsPrimaryKeyExpression(id));

            if (EntityToDelete != null) 
            {
                _db.Set<Model>().Remove(EntityToDelete);
            }
        }

        public async Task<Domain?> GetEntityAsync(string id)
        {
            Model getModel =   _db.Set<Model>().FirstOrDefault(WhereIdEqualsPrimaryKeyExpression(id));     
            return _mapper.Map<Domain>(getModel);         
        }

        public async Task UpdateAsync(Domain entity)
        {
            var Model = _mapper.Map<Model>(entity);
            _db.Set<Model>().Update(Model);
        }


    }
}
