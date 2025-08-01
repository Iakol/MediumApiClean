using UserRealetionShipDomain.Application.DTO;

namespace UserRealetionShipDomain.Presentation.UseCases.UserBlockCases
{
    public interface IUnBlockUserCase
    {
        public Task<Result> Handle(string UserId, string UserToBlock);

    }
}
