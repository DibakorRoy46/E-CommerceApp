
using MediatR;

namespace Notification.Application.Commands;

public record ActivatedTemplateCommand(string TemplateCode) : IRequest<bool>;
