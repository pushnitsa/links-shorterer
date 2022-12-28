using LinksShorterer.Repositories;

namespace LinksShorterer.Models;

public class LinkEntity : IEntity
{
    public LinkEntity(string shortName, string fullUrl)
    {
        ShortName = shortName;
        FullUrl = fullUrl;
    }

    public Guid Id { get; set; }

    public string ShortName { get; set; }

    public string FullUrl { get; set; }

    public int? Hits { get; set; }
}
