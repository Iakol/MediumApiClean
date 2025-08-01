namespace UserRealetionShipDomain.Application.UnitOfWorks.ISaveReadingListUnitOfWorks
{
    public interface ISaveReadingListUnitOfWork
    {
        public Task SaveReadinglist(string UserId, string readingList);


    }
}
