
using MassTransit;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Notification.Application.Interfaces;
using Notification.Domain.Entities;
using Notification.Domain.Enums;
using Notification.Instrastructure.DTOs;
using Notification.Instrastructure.Interfaces;
using Notification.Instrastructure.Providers;
using System.Text.Json;

namespace Notification.Instrastructure.Consumers;

public class NotificationCreatedConsumer : IConsumer<NotificationCreatedDto>
{
    private readonly IMongoCollection<Notifier> _writeCol;
    private readonly IOutboxRepository _outbox;
    private readonly IProviderResolver _resolver;
    private readonly IDeliveryEventRepository _deliveryRepo;
    private readonly ILogger<NotificationCreatedConsumer> _logger;

    public NotificationCreatedConsumer(IMongoDatabase db, IOutboxRepository outbox, IProviderResolver resolver, IDeliveryEventRepository deliveryRepo, ILogger<NotificationCreatedConsumer> logger)
    {
        _writeCol = db.GetCollection<Notifier>("notifications_write");
        _outbox = outbox; _resolver = resolver; _deliveryRepo = deliveryRepo; _logger = logger;
    }

    public async Task Consume(ConsumeContext<NotificationCreatedDto> context)
    {
        var msg = context.Message;
        _logger.LogInformation("Consuming NotificationCreated {Id}", msg.NotificationId);

        // read notification (should already exist in write store) or create minimal doc if needed
        var n = await _writeCol.Find(x => x.Id == msg.NotificationId).FirstOrDefaultAsync();
        if (n == null)
        {
            // create lightweight notification doc if not present
            var recipient = new Notification.Domain.ValueObjects.Recipient(msg.Recipient.UserId, msg.Recipient.Email, msg.Recipient.PhoneNumber, msg.Recipient.PushToken);
            n = new Notifier(msg.NotificationId, msg.TemplateId, Enum.Parse<NotificationChannelEnum>(msg.Channel, true), recipient, null);
            await _writeCol.InsertOneAsync(n);
        }

        // pick providers
        var providers = _resolver.GetProvidersForChannel(n.Channel).ToList();
        var sent = false;
        foreach (var p in providers)
        {
            ProviderResult result = p switch
            {
                IEmailProvider ep when n.Channel == NotificationChannelEnum.Email => await ep.SendAsync(n.Recipient.Email!, "no-subject", RenderHtml(msg.Payload), context.CancellationToken),
                ISmsProvider sp when n.Channel == NotificationChannelEnum.Sms => await sp.SendAsync(n.Recipient.PhoneNumber!, RenderText(msg.Payload), context.CancellationToken),
                IPushProvider pp when n.Channel == NotificationChannelEnum.Push => await pp.SendAsync(n.Recipient.PushToken!, "title", RenderText(msg.Payload), context.CancellationToken),
                _ => new ProviderResult(false, "Unsupported", "Provider not applicable")
            };

            // record attempt
            await _deliveryRepo.AddAttemptAsync(n.Id, new Notification.Domain.Entities.DeliveryAttempt { AttemptedAt = DateTime.UtcNow, Provider = result.ProviderName ?? "unknown", Succeeded = result.Success, Response = result.Response }, context.CancellationToken);

            if (result.Success)
            {
                // update notification status
                await _writeCol.UpdateOneAsync(x => x.Id == n.Id, Builders<Notifier>.Update.Set(x => x.Status, NotificationStatusEnum.Sent).Set(x => x.UpdatedAt, DateTime.UtcNow));
                // add outbox event
                await _outbox.AddAsync(new OutboxEvent { AggregateId = n.Id, EventType = "NotificationSent", Payload = new { NotificationId = n.Id, Provider = result.ProviderName, SentAt = DateTime.UtcNow } }, context.CancellationToken);
                sent = true;
                break;
            }
        }

        if (!sent)
        {
            await _writeCol.UpdateOneAsync(x => x.Id == n.Id, Builders<Notifier>.Update.Set(x => x.Status, NotificationStatusEnum.Failed).Set(x => x.UpdatedAt, DateTime.UtcNow));
            await _outbox.AddAsync(new OutboxEvent { AggregateId = n.Id, EventType = "NotificationFailed", Payload = new { NotificationId = n.Id, Reason = "AllProvidersFailed", At = DateTime.UtcNow } }, context.CancellationToken);
        }
    }

    private string RenderHtml(JsonElement payload)
        => payload.TryGetProperty("html", out var h) ? h.GetString() ?? "" : payload.ToString();

    private string RenderText(JsonElement payload)
        => payload.TryGetProperty("text", out var t) ? t.GetString() ?? "" : payload.ToString();
}