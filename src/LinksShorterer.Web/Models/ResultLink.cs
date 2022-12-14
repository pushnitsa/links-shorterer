namespace LinksShorterer.Models;

public class ResultLink
{
    public string? ShortLink { get; set; }
    public ICollection<string> Errors { get; set; } = new List<string>();
}
