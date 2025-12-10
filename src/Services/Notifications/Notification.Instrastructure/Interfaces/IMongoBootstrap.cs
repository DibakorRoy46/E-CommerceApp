
namespace Notification.Instrastructure.Interfaces;

public interface IMongoBootstrap 
{
    Task EnsureIndexesAsync(CancellationToken ct = default);
}

