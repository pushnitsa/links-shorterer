namespace LinksShorterer.Models;

public class FullLink
{
    public string? FullUrl { get; set; }

    public bool IsPermanent { get; set; }

    public DateTime? ExpirationDate { get; set; }
}
