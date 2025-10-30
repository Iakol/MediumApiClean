using ResponceDomain.Application.DTO;
using ResponceDomain.Presentation.Comand;

namespace ResponceDomain.Presentation.UseCases
{
    public interface IUpdateResponceCase
    {
        public Task<Result> Handle(UpdateResponceCommandData updateData, string userId);

    }
}
