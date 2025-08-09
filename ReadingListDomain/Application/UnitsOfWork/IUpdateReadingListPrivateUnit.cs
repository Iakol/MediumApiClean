namespace ReadingListDomain.Application.UnitsOfWork
{
    public interface IUpdateReadingListPrivateUnit
    {
        public Task SetPrivate(bool isPrivate);
    }
}
