namespace UserRealetionShipDomain.Application.UnitOfWorks.ISaveReadingListUnitOfWorks
{
    public interface IUnSaveReadingListUnitOfWork
    {
        public Task UnSaveReadingList(string UserId, string readingList);
    }
}
