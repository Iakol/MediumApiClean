using ResponceDomain.Application.DTO;
using ResponceDomain.Application.Enum;
using ResponceDomain.Presentation.Comand;

namespace ResponceDomain.Presentation.UseCases
{
    public interface IAddResponce
    {
        public Task<Result> Handle(CreateResponceCommandData NewResponce, string userId);

    }
}
