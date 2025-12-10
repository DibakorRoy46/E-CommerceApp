
namespace Notification.Instrastructure.Settings;

public sealed class MongoSettings
{
    public string ConnectionString { get; init; } = "mongodb://localhost:27017";
    public string Database { get; init; } = "notificationdb";
}
