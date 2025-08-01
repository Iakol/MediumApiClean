using UserRealetionShipDomain.Application.DTO;

namespace UserRealetionShipDomain.Presentation.UseCases.ReadingListCases
{
    public interface IUnSaveReadingListCase
    {
        public Task<Result> Handle(string UserId, string readingList);

    }
}
