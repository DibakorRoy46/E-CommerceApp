
using MediatR;

namespace Notification.Application.Commands;

public record DeActivatedTemplateCommand(string TemplateCode) : IRequest<bool>;
