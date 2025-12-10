
namespace Notification.Application.Abstractions;

public interface IPushSender 
{ 
    Task<bool> SendAsync(string token, string title, string message); 
}
