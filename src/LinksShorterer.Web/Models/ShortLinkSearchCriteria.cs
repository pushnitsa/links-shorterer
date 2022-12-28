namespace LinksShorterer.Models;

public record ShortLinkSearchCriteria
{
    public string? ShortName { get; set; }
    public int Take { get; set; } = 20;
    public int? Skip { get; set; }
}
