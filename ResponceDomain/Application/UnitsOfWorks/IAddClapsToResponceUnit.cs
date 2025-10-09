using ResponceDomain.Domain;

namespace ResponceDomain.Application.UnitsOfWorks
{
    public interface IAddClapsToResponceUnit
    {
        public Task AddClaps(ClapsToResponceOfUsers clapsToResponceOfUsers);
    }
}
