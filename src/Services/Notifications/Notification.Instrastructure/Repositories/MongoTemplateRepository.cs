
using MongoDB.Driver;
using Notification.Application.Interfaces;
using Notification.Domain.Entities;

namespace Notification.Instrastructure.Repositories;

public class MongoTemplateRepository : ITemplateRepository
{
    private readonly IMongoCollection<Template> _col;
    public MongoTemplateRepository(IMongoDatabase db) => _col = db.GetCollection<Template>("templates");

    public async Task<Template?> GetByIdAsync(string id, CancellationToken ct = default)
        => await _col.Find(t => t.Id == id).FirstOrDefaultAsync(ct);

    public async Task InsertAsync(Template template, CancellationToken ct = default)
        => await _col.InsertOneAsync(template, cancellationToken: ct);
}
