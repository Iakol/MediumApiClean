using ResponceDomain.Application.DTO;
using ResponceDomain.Application.Enum;
using ResponceDomain.Presentation.Comand;

namespace ResponceDomain.Presentation.UseCases
{
    public interface IAddResponceCase
    {
        public Task<Result> Handle(CreateResponceCommandData NewResponce, string userId);

    }
}
