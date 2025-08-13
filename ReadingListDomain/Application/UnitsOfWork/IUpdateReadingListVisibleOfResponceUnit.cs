namespace ReadingListDomain.Application.UnitsOfWork
{
    public interface IUpdateReadingListVisibleOfResponceUnit
    {
        public Task SetVisibleOfResponce(bool isVisible, string readingListId);
    }
}
