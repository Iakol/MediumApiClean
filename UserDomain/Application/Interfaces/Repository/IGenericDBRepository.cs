namespace UserDomain.Application.Interfaces.Repository
{
    public interface IGenericDBRepository<Model,Domain,PrimaryKeyType> where Model : class where Domain : class
    {
        public Task AddAsync(Domain entity);
        public Task UpdateAsync(Domain entity);
        public Task DeleteAsync(PrimaryKeyType id);
        public Task<Domain?> GetEntityAsync(PrimaryKeyType id);



    }
}
