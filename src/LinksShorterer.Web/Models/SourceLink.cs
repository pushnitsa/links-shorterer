namespace LinksShorterer.Models;

public class SourceLink
{
    public string FullUrl { get; set; }

    public string? ShortName { get; set; }

    public bool IsPermanent { get; set; }

    public DateTime? ExpirationDate { get; set; }
}
