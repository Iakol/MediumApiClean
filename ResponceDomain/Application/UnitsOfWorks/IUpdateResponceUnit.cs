using ResponceDomain.Domain;

namespace ResponceDomain.Application.UnitsOfWorks
{
    public interface IUpdateResponceUnit
    {
        public Task UpdateResponce(Responce responce);
    }
}
