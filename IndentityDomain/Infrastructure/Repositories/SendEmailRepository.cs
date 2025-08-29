using IndentityDomain.Application.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace IndentityDomain.Infrastructure.Repositories
{
    public class SendEmailRepository : IEmailSendler,IDisposable
    {
        private readonly ISmtpClient _smtpClient;
        private const string MailName = "MyMedium.ua";
        private const string EmailAddress = "website@Medium.ua";

        public SendEmailRepository(IOptions<SMTPCred> options) 
        {
            _smtpClient = new SmtpClient();
            _smtpClient.Connect(options.Value.Host,options.Value.Port, MailKit.Security.SecureSocketOptions.StartTls);
            _smtpClient.Authenticate(options.Value.UserName,options.Value.Password);
        }

        public async Task SendEmail( string toAddress, string subject, string message, string messageType = "plain")
        {
            MimeMessage email = new MimeMessage();
            email.From.Add(new MailboxAddress(MailName, EmailAddress));
            email.To.Add(MailboxAddress.Parse(toAddress));
            email.Subject = subject;
            email.Body = new TextPart(messageType) { Text = message };

            await _smtpClient.SendAsync(email);
        }

        public void Dispose()
        {
            _smtpClient.DisconnectAsync(true);
        }
    }
}
