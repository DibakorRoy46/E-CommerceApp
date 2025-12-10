
namespace Notification.Domain.ValueObjects;

public sealed class Recipient
{
    public string UserId { get; }
    public string? Email { get; }
    public string? PhoneNumber { get; }
    public string? PushToken { get; }


    public Recipient(string userId, string? email = null, string? phoneNumber = null, string? pushToken = null)
    {
        UserId = string.IsNullOrWhiteSpace(userId) ? throw new ArgumentException("userId is required", nameof(userId)) : userId;
        Email = string.IsNullOrWhiteSpace(email) ? null : email;
        PhoneNumber = string.IsNullOrWhiteSpace(phoneNumber) ? null : phoneNumber;
        PushToken = string.IsNullOrWhiteSpace(pushToken) ? null : pushToken;


        if (Email is null && PhoneNumber is null && PushToken is null)
        {
            throw new ArgumentException("At least one contact method (email, phone, push token) must be provided");
        }
    }
}