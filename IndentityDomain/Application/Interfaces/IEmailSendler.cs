namespace IndentityDomain.Application.Interfaces
{
    public interface IEmailSendler
    {
        public Task SendEmail(string toAddress, string subject, string message,string messageType = "plain");
    }
}
