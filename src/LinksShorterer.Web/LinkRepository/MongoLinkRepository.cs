using LinksShorterer.Models;
using LinksShorterer.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LinksShorterer.LinkRepository;

public class MongoLinkRepository : ILinkRepository
{
    private readonly IMongoClient _mongoClient;
    private readonly IMongoDatabase _mongoDatabase;
    private readonly IMongoCollection<MongoLink> _mongoLinkCollection;
    public MongoLinkRepository(IConfiguration configuration, IOptions<MongoOptions> option)
    {
        _mongoClient = new MongoClient(configuration.GetConnectionString("MongoDB"));
        _mongoDatabase = _mongoClient.GetDatabase(option.Value.DatabaseName);
        _mongoLinkCollection = _mongoDatabase.GetCollection<MongoLink>("links");
    }

    public async Task CreateLinkAsync(SourceLink sourceLink)
    {
        if (string.IsNullOrEmpty(sourceLink.ShortName))
        {
            throw new ArgumentNullException(nameof(sourceLink.ShortName), "Short link name must be filled");
        }

        var mongoLink = new MongoLink
        {
            FullUrl = sourceLink.FullUrl,
            ShortLinkName = sourceLink.ShortName,
        };

        await _mongoLinkCollection.InsertOneAsync(mongoLink);
    }

    public async Task<SourceLink?> GetAsync(string shortLinkName)
    {
        var filter = Builders<MongoLink>.Filter.Eq(nameof(MongoLink.ShortLinkName), shortLinkName);
        var result = await _mongoLinkCollection.Find(filter).ToListAsync();

        var link = result.FirstOrDefault();

        var sourceLink = default(SourceLink);

        if (link != null)
        {
            sourceLink = new SourceLink
            {
                ShortName = link.ShortLinkName,
                FullUrl = link.FullUrl,
            };
        }

        return sourceLink;
    }

    public async Task IncreaseLinkHitsAsync(string shortLinkName)
    {
        var filter = Builders<MongoLink>.Filter.Eq(nameof(MongoLink.ShortLinkName), shortLinkName);
        var result = await _mongoLinkCollection.Find(filter).ToListAsync();

        var link = result.FirstOrDefault();

        if (link != null)
        {
            link.Hits++;

            filter = Builders<MongoLink>.Filter.Eq("_id", link.Id);
            var update = Builders<MongoLink>.Update.Set(nameof(MongoLink.Hits), link.Hits);

            await _mongoLinkCollection.UpdateOneAsync(filter, update);
        }
    }

    public async Task<bool> IsLinkExistsAsync(string shortLinkName)
    {
        var filter = Builders<MongoLink>.Filter.Eq(nameof(MongoLink.ShortLinkName), shortLinkName);
        var result = await _mongoLinkCollection.Find(filter).AnyAsync();

        return result;
    }

    public void Dispose()
    {
        // Nothing is to do here
    }
}
