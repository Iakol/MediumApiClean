using UserRealetionShipDomain.Application.DTO;
using UserRealetionShipDomain.Application.Interfaces;
using UserRealetionShipDomain.Domain;

namespace UserRealetionShipDomain.Presentation.UseCases.ReadingListCases
{
    public interface IGetUserSaveReadingListCase
    {
        public Task<Result<List<SaveReadingList>>> Handle(string userId);
    }
}
