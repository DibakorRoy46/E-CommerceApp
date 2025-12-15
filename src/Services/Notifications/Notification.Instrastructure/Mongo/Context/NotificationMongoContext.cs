

using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Notification.Instrastructure.Mongo.Collections;

namespace Notification.Instrastructure.Mongo;

public class NotificationMongoContext
{
    private readonly IMongoDatabase _database;

    public NotificationMongoContext(IConfiguration configuration)
    {
        var client = new MongoClient(
            configuration.GetConnectionString("MongoDb"));

        _database = client.GetDatabase("NotificationDb");
    }

    public IMongoCollection<NotificationDocument> Notifications =>
        _database.GetCollection<NotificationDocument>("Notifications");

    public IMongoCollection<NotificationTemplateDocument> Templates =>
        _database.GetCollection<NotificationTemplateDocument>("NotificationTemplates");
}

