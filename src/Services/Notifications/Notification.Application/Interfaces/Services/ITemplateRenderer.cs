
using Notification.Domain.ValueObjects;

namespace Notification.Application.Interfaces.Services;

public interface ITemplateRenderer
{
    string Render(string template, TemplateData data);
}
