
using MediatR;
using Notification.Domain.Enums;
using System.Threading.Channels;

namespace Notification.Application.Commands;

public record CreateNotificationCommand(
                string TemplateId,
                string UserId,
                string? Email,
                string? PhoneNumber,
                string? PushToken,
                NotificationChannelEnum Channel,
                IDictionary<string, object>? Payload
                ) : IRequest<string>; // returns Notification Id