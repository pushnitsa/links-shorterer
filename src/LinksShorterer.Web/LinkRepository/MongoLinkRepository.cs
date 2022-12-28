using LinksShorterer.Models;
using LinksShorterer.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace LinksShorterer.LinkRepository;

public class MongoLinkRepository : ILinkRepository
{
    private readonly IMongoClient _mongoClient;
    private readonly IMongoDatabase _mongoDatabase;
    private readonly IMongoCollection<LinkEntity> _mongoLinkCollection;

    private readonly FilterDefinitionBuilder<LinkEntity> _filterDefinitionBuilder = Builders<LinkEntity>.Filter;

    public MongoLinkRepository(IConfiguration configuration, IOptions<MongoOptions> option)
    {
        _mongoClient = new MongoClient(configuration.GetConnectionString("MongoDB"));
        _mongoDatabase = _mongoClient.GetDatabase(option.Value.DatabaseName);
        _mongoLinkCollection = _mongoDatabase.GetCollection<LinkEntity>("links");
    }

    public async Task CreateLinkAsync(LinkEntity linkEntity)
    {
        if (string.IsNullOrEmpty(linkEntity.ShortName))
        {
            throw new ArgumentNullException(nameof(linkEntity.ShortName), "Short link name must be filled");
        }

        await _mongoLinkCollection.InsertOneAsync(linkEntity);
    }

    public async Task<LinkEntity?> GetAsync(string shortLinkName)
    {
        var filter = _filterDefinitionBuilder.Eq(x => x.ShortName, shortLinkName);
        var result = await _mongoLinkCollection.Find(filter).ToListAsync();

        return result.FirstOrDefault();
    }

    public async Task<IReadOnlyCollection<LinkEntity>> FindAsync(ISpecification<LinkEntity> specification)
    {
        IFindFluent<LinkEntity, LinkEntity>? query;

        if (specification.Take == 0)
        {
            return new List<LinkEntity>();
        }

        if (specification.Criteria != null)
        {
            query = _mongoLinkCollection.Find(specification.Criteria);
        }
        else
        {
            query = _mongoLinkCollection.Find(new BsonDocument());
        }

        if (specification.Skip != null)
        {
            query = query.Skip(specification.Skip.Value);
        }

        query = query.Limit(specification.Take);

        var result = await query.ToListAsync();

        return result;
    }

    public async Task<int> CountAsync()
    {
        var query = _mongoLinkCollection.Find(new BsonDocument());

        var result = await query.CountDocumentsAsync();

        return Convert.ToInt32(result);
    }

    public async Task<int> CountAsync(ISpecification<LinkEntity> specification)
    {
        IFindFluent<LinkEntity, LinkEntity>? query;

        if (specification.Criteria != null)
        {
            query = _mongoLinkCollection.Find(specification.Criteria);
        }
        else
        {
            query = _mongoLinkCollection.Find(new BsonDocument());
        }

        var result = await query.CountDocumentsAsync();

        return Convert.ToInt32(result);
    }

    public Task IncreaseLinkHitsAsync(string shortLinkName)
    {
        /*var filter = _filterDefinitionBuilder.Eq(x => x.ShortName, shortLinkName);
        var result = await _mongoLinkCollection.Find(filter).ToListAsync();

        var link = result.FirstOrDefault();

        if (link != null)
        {
            link.Hits++;

            filter = _filterDefinitionBuilder.Eq(x => x.Id, link.Id);
            var update = Builders<MongoLink>.Update.Set(x => x.Hits, link.Hits);

            await _mongoLinkCollection.UpdateOneAsync(filter, update);
        }*/

        return Task.CompletedTask;
    }

    public async Task<bool> IsLinkExistsAsync(string shortLinkName)
    {
        var filter = _filterDefinitionBuilder.Eq(x => x.ShortName, shortLinkName);
        var result = await _mongoLinkCollection.Find(filter).AnyAsync();

        return result;
    }

    public void Dispose()
    {
        // Nothing is to do here
    }

}
