using UserRealetionShipDomain.Application.DTO;

namespace UserRealetionShipDomain.Presentation.UseCases.UserBlockCases
{
    public interface IBlockUserCase
    {
        public Task<Result> Handle(string UserId, string UserToBlock);
    }
}
