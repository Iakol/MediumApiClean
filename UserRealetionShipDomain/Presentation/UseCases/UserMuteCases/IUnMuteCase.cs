using UserRealetionShipDomain.Application.DTO;

namespace UserRealetionShipDomain.Presentation.UseCases.UserMuteCases
{
    public interface IUnMuteCase
    {
        public Task<Result> Handle(string UserId, string MuteId);

    }
}
