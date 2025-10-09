using ResponceDomain.Domain;

namespace ResponceDomain.Application.UnitsOfWorks
{
    public interface IAddResponceUnit
    {
        public Task addResponce(Responce responce);
    }
}
