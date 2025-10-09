using ResponceDomain.Domain;

namespace ResponceDomain.Application.Interfaces
{
    public interface IResponceRepository
    {
        public Task<IEnumerable<Responce>> GetAllResponcesByItem(string itemId);

        public Task<Responce?> GetResponceById(int id);

        public Task<IEnumerable<int>> GetTreeFlatListOfResponceIDsByParent(int Parentid);
        public Task DeleteResponceList(List<int> responcesList);

        public Task DeleteResponceList(List<Responce> responcesList);



        public Task UpdateTextOfResponce(Responce UpdatedDomainResponce);

        public Task AddResponce(Responce responce);
    }
}
