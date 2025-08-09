namespace ReadingListDomain.Application.Interfaces
{
    public interface IGenericDBRepository<Model, Domain> where Model : class where Domain : class
    {
        public Task AddAsync(Domain entity);
        public Task UpdateAsync(Domain entity);
        public Task DeleteAsync(string id);
        public Task<Domain?> GetEntityAsync(string id);
    }
}
