using UserRealetionShipDomain.Application.DTO;
using UserRealetionShipDomain.Domain;

namespace UserRealetionShipDomain.Presentation.UseCases.UserBlockCases
{
    public interface IGetUserBlockListCase
    {
        public Task<Result<List<UserBlock>>> Handle(string UserId);


    }
}
