
using Notification.Domain.Entities;

namespace Notification.Application.Interfaces;

public interface ITemplateRepository
{
    Task<Template?> GetByIdAsync(string id, CancellationToken ct = default);
    Task InsertAsync(Template template, CancellationToken ct = default);
}
