
namespace Notification.Instrastructure.DTOs;

public record RecipientDto(
    string UserId, 
    string? Email,
    string? PhoneNumber, 
    string? PushToken
    );