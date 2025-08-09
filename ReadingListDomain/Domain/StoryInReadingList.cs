namespace ReadingListDomain.Domain
{
    public class StoryInReadingList
    {

        public StoryInReadingList(string storyId, string readingListId)
        {
            StoryId = storyId;
            ReadingListId = readingListId;
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string StoryId {  get; set; }
        public string ReadingListId { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}
