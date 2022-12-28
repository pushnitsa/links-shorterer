namespace LinksShorterer.Models;

public record ShortLinkSearchResult
{
    public IReadOnlyCollection<Link> Links { get; set; } = new List<Link>();
    public int TotalCount { get; set; }
}
