namespace UserRealetionShipDomain.Application.Interfaces
{
    public interface IGenericDBRepository<Model, Domain> where Model : class where Domain : class
    {
        public Task AddAsync(Domain entity);
        public Task DeleteAsync(string Followid, string subcsriptionid);
        public Task<Domain?> GetEntityAsync(string Followid, string subcsriptionid);

        public Task<List<Domain>> GetEntityListByFollowIdAsync(string Followid);




    }
}
