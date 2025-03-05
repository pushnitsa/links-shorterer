namespace LinksShorterer.Models;

public class Link
{
    public Guid Id { get; set; }

    public required string FullUrl { get; init; }

    public string? ShortName { get; set; }

    public bool IsPermanent { get; init; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public int? Hits { get; set; }
}
