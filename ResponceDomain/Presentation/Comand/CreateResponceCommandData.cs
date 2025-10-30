using ResponceDomain.Application.Enum;

namespace ResponceDomain.Presentation.Comand
{
    public class CreateResponceCommandData
    {
        public string itemId { get; set; }

        public ReaderTypeEnum type { get; set; }

        public string text { get; set; }

        public int? BaseResponce {get;  set; }
    }
}
