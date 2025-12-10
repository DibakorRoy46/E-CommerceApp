
using MongoDB.Driver;
using Notification.Instrastructure.Settings;

namespace Notification.Instrastructure.Persistence;

public static class MongoClientFactory
{
    public static IMongoClient Create(MongoSettings settings)
    {
        var url = new MongoUrl(settings.ConnectionString);
        var clientSettings = MongoClientSettings.FromUrl(url);
        clientSettings.RetryWrites = true;
        // tune timeouts if needed
        return new MongoClient(clientSettings);
    }
}
