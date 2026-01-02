
namespace Notification.Domain.ValueObjects;

public class TemplateData
{
    private readonly Dictionary<string, string> _data;

    public IReadOnlyDictionary<string, string> Data => _data;

    public TemplateData(Dictionary<string, string> data)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public string GetValue(string key)
    {
        return _data.TryGetValue(key, out var value)
            ? value
            : string.Empty;
    }
}

