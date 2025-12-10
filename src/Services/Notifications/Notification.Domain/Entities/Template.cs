
namespace Notification.Domain.Entities;

public sealed class Template
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public IDictionary<string, string> LocalizedBodies { get; private set; } = new Dictionary<string, string>();
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public Template(string id, string name, string subject, string body)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Subject = subject ?? string.Empty;
        Body = body ?? string.Empty;
    }

    public void AddLocale(string locale, string body) => LocalizedBodies[locale] = body;
}
