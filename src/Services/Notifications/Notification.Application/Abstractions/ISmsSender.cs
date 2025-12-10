
namespace Notification.Application.Abstractions;

public interface ISmsSender
{
    Task<bool> SendAsync(string phoneNumber, string message);
}
