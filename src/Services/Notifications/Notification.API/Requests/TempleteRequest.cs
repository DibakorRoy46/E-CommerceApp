using Notification.API.Enums;

namespace Notification.API.Requests;

public record TempleteRequest(int? Type, int? channel,bool? IsActive );
