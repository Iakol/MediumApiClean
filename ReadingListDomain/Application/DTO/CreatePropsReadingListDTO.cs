namespace ReadingListDomain.Application.DTO
{
    public class CreatePropsReadingListDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsPrivate { get; set; }
    }
}
