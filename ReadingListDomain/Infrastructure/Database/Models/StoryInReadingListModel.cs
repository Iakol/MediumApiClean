using ReadingListDomain.Domain;

namespace ReadingListDomain.Infrastructure.Database.Models
{
    public class StoryInReadingListModel
    {
        public string Id { get; set; }
        public string StoryId { get; set; }
        public string ReadingListId { get; set; }
        public string Note { get; set; }
        public ReadingListModel ReadingList {  get; set; } 
    }
}
