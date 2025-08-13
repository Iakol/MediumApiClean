namespace ReadingListDomain.Application.DTO
{
    public class SaveStoryPropsDTO
    {
        public string storyId { get; set; }
        public string ReadinglistId { get; set; }
        public string? Note { get; set; }
    }
}
