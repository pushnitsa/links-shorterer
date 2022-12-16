using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LinksShorterer.Models;

public class MongoLink
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement]
    public string ShortLinkName { get; set; }

    [BsonElement]
    public string FullUrl { get; set; }

    [BsonElement]
    public int Hits { get; set; }
}
