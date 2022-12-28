using LinksShorterer.Models;
using LinksShorterer.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace LinksShorterer.Repositories;

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

    public async Task<LinkEntity?> GetAsync(string id)
    {
        var filter = _filterDefinitionBuilder.Eq(x => x.Id, new Guid(id));
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

    public async Task SaveAsync(LinkEntity entity)
    {
        var existFilter = _filterDefinitionBuilder.Eq(x => x.Id, entity.Id);
        var isEntityExist = await _mongoLinkCollection.Find(existFilter).AnyAsync();

        if (isEntityExist)
        {
            await _mongoLinkCollection.ReplaceOneAsync(existFilter, entity);
        }
        else
        {
            await _mongoLinkCollection.InsertOneAsync(entity);
        }
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
