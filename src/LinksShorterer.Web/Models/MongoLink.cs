namespace LinksShorterer.Models;

public class MongoLink
{
    public Guid Id { get; set; }

    public string ShortLinkName { get; set; }

    public string FullUrl { get; set; }

    public int Hits { get; set; }
}
