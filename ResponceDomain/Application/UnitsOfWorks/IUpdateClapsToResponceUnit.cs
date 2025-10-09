using ResponceDomain.Domain;

namespace ResponceDomain.Application.UnitsOfWorks
{
    public interface IUpdateClapsToResponceUnit
    {
        public Task UpdateClaps(ClapsToResponceOfUsers clapsToUpdate);
    }
}
