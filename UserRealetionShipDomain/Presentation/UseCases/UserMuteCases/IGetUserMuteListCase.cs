using UserRealetionShipDomain.Application.DTO;
using UserRealetionShipDomain.Domain;

namespace UserRealetionShipDomain.Presentation.UseCases.UserMuteCases
{
    public interface IGetUserMuteListCase
    {
        public Task<Result<List<UserMute>>> Handle(string UserId);


    }
}
