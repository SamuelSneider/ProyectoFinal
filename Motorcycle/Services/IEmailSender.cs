using System.Threading.Tasks;

namespace Motorcycle.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string message, string? attachmentPath = null);
    }

}
