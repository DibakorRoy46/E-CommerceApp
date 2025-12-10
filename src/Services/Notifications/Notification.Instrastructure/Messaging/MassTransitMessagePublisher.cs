
using MassTransit;
using Notification.Instrastructure.Interfaces;

namespace Notification.Instrastructure.Messaging;

public class MassTransitMessagePublisher : IMessagePublisher
{
    private readonly IBus _bus;
    public MassTransitMessagePublisher(IBus bus) => _bus = bus;
    public async Task PublishAsync<T>(T message, CancellationToken ct = default) where T : class
                        => await _bus.Publish(message, ct);
}
