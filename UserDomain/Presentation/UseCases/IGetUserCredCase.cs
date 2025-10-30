using System.Reflection.Metadata;
using UserDomain.Application.DTO;

namespace UserDomain.Presentation.UseCases
{
    public interface IGetUserCredCase
    {
        public Task<Result<UserCredDTO>> Handle(string userId);
    }
}
