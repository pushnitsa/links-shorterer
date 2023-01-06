namespace LinksShorterer.Models;

public class Link
{
    public Guid Id { get; set; }

    public string FullUrl { get; set; }

    public string? ShortName { get; set; }

    public bool IsPermanent { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public int? Hits { get; set; }
}
