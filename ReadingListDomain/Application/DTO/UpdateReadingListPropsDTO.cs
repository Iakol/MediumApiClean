namespace ReadingListDomain.Application.DTO
{
    public class UpdateReadingListPropsDTO
    {
        public string ReadingListId { get; set; }
        public bool? isPrivate { get; set; }
        public string? Note { get; set; }
        public bool? IsVisible { get; set; }
    }
}
