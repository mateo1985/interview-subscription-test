using System.Threading.Tasks;

namespace MailSubscriptionsApi.Services
{
    public interface IMailingService
    {
        Task SendEmailAsync(string destination, string htmlMessage, string subject);
    }
}