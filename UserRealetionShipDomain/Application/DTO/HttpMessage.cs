namespace UserRealetionShipDomain.Application.DTO
{
    public class HttpMessage<T>
    {
        public bool sucsses { get; set; }
        public string message { get; set; }
        public T? data { get; set; }
    }

    public class HttpMessage
    {
        public bool sucsses { get; set; }
        public string message { get; set; }
    }
}
