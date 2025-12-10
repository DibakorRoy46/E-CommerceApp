

namespace Notification.Application.Abstractions;

public interface IEmailSender 
{
    Task<bool> SendAsync(string email, string subject, string bodyHtml); 
}
