using ApiFatecWeb.Code.Helpers;
using ApiFatecWeb.Core.Model;
using ApiFatecWeb.Core.Service.Interface;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace ApiFatecWeb.Core.Service
{
    public class EmailService : IEmailService
    {
        private readonly string _sender = "email";
        private readonly string _user = "email";
        private readonly string _password = "password";

        private readonly ILogHandler _logger;

        public EmailService(ILogHandler logger)
        {
            _logger = logger;
        }

        public async Task<bool> SendEmail(string receiverEmail, EmailModel emailBody)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_sender));
            email.To.Add(MailboxAddress.Parse(receiverEmail));
            email.Subject = emailBody.Title;
            email.Body = new TextPart(TextFormat.Html) { 
                Text = @$"
                    <h1>{emailBody.Title}</h1>
                    <p>
                        {emailBody.Body}
                    </p>
                " + (emailBody.Image?.Length == 0 ? "" : $"<img src=\"{emailBody.Image}\">")
            };

            try
            {
                using var smtp = new SmtpClient();

                await smtp.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_user, _password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                return true;
            }
            catch (Exception ex)
            {
                _logger.SaveLog("SendEmail",ex.Message, 0);
                return false;
            }
        }
    }
}
