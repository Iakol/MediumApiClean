using ReadingListDomain.Domain;

namespace ReadingListDomain.Infrastructure.Database.Models
{
    public class ReadingListModel
    {
        public string Id { get; set; }
        public string ReadingListCreator { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Immortal { get; set; }

        public bool IsPrivate { get; set; }
        public bool IsOpenResponces { get; set; }

        public List<StoryInReadingListModel> SaveStories { get; set; }
    }
}
