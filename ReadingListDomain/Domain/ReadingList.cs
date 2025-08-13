using ReadingListDomain.Application.DTO;

namespace ReadingListDomain.Domain
{
    public class ReadingList
    {

        public ReadingList() { }

        public ReadingList(string ReadingListCreator,string Name,string Description, bool IsPrivate, bool IsOpenResponses, bool Immortal = false)
        {
            ReadingListCreator = ReadingListCreator;
            Name = Name;
            Description = Description;
            Immortal = Immortal;
            IsPrivate = IsPrivate;
            IsOpenResponces = IsOpenResponses;
            SaveStories = new List<StoryInReadingList> ();
        }

        public ReadingList(CreatePropsReadingListDTO readingListDTO, string userId)
        {
            ReadingListCreator = userId;
            Name = readingListDTO.Name;
            Description = readingListDTO.Description;
            IsPrivate = readingListDTO.IsPrivate;
            IsOpenResponces = readingListDTO.IsPrivate;
            SaveStories = new List<StoryInReadingList>();
        }

        public ReadingList(string UserId) 
        {
            ReadingListCreator =UserId;
            Description = string.Empty;
            Name = "Reading list";
            Immortal = true;
            IsPrivate = false;
            IsOpenResponces = false;
            SaveStories = new List<StoryInReadingList>();
        }

        


        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ReadingListCreator { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public bool Immortal { get; set; } = false;

        public bool IsPrivate { get; set; }
        public bool IsOpenResponces { get; set; }

        public List<StoryInReadingList> SaveStories { get; set; }
    }
}
