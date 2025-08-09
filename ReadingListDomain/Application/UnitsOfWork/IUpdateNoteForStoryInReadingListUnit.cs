namespace ReadingListDomain.Application.UnitsOfWork
{
    public interface IUpdateNoteForStoryInReadingListUnit
    {
        public Task UpdateNote(string SaveStoryId, string NewNote);
    }
}
