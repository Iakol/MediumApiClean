using UserDomain.Application.DTO;

namespace UserDomain.Presentation.UseCases
{
    public interface IGetUserHeaderCase
    {
        public Task<Result<UserHeaderDTO>> Handle(string userId);

    }
}
