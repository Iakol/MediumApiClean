using UserRealetionShipDomain.Application.DTO;

namespace UserRealetionShipDomain.Presentation.UseCases.UserMuteCases
{
    public interface IMuteUserCase
    {
        public Task<Result> Handle(string UserId, string MuteId);

    }
}
