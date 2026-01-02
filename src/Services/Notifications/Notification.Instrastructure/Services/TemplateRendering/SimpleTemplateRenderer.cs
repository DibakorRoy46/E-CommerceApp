
using Notification.Application.Interfaces.Services;
using Notification.Domain.ValueObjects;

namespace Notification.Instrastructure.Services.TemplateRendering;

public class SimpleTemplateRenderer : ITemplateRenderer
{
    public string Render(string template, TemplateData data)
    {
        var result = template;

        foreach (var item in data.Data)
        {
            result = result.Replace(
                $"{{{{{item.Key}}}}}",
                item.Value);
        }

        return result;
    }
}

