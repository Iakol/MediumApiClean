using ResponceDomain.Application.DTO;
using ResponceDomain.Presentation.Comand;

namespace ResponceDomain.Presentation.UseCases
{
    public interface IAddClapsToResponceCase
    {
        public Task<Result<int>> Handle(AddClapsToResponceCommandData addClapsData, string userId);

    }
}
