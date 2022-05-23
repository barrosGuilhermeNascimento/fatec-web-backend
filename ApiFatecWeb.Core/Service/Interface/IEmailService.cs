using ApiFatecWeb.Core.Model;

namespace ApiFatecWeb.Core.Service.Interface
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string receiverEmail, EmailModel emailBody);
    }
}
