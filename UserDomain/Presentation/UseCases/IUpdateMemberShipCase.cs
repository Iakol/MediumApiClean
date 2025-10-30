using UserDomain.Application.DTO;

namespace UserDomain.Presentation.UseCases
{
    public interface IUpdateMemberShipCase
    {
        public Task<Result> Handle();
    }
}
