namespace ReadingListDomain.Application.UnitsOfWork
{
    public interface IDeleteStoryInReadingListUnit
    {
        public Task DeleteSaveStoryInReadingList(string SaveStory);

    }
}
