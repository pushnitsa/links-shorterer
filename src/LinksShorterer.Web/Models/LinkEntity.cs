using LinksShorterer.Repositories;

namespace LinksShorterer.Models;

public class LinkEntity : IEntity
{
    public LinkEntity(string shortName, string fullUrl, bool isPermanent, DateTime createdAt)
    {
        ShortName = shortName;
        FullUrl = fullUrl;
        IsPermanent = isPermanent;
        CreatedAt = createdAt;
    }

    public Guid Id { get; set; }

    public string ShortName { get; set; }

    public string FullUrl { get; set; }

    public bool IsPermanent { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? Hits { get; set; }
}
