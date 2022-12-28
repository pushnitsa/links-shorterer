using LinksShorterer.Repositories;

namespace LinksShorterer.Models;

public class LinkEntity : IEntity
{
    public LinkEntity(string shortLinkName, string fullUrl)
    {
        ShortName = shortLinkName;
        FullUrl = fullUrl;
    }

    public Guid Id { get; set; }

    public string ShortName { get; set; }

    public string FullUrl { get; set; }
}
