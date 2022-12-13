namespace LinksShorterer.ShortererService;

public interface IRedirector
{
    Task<string> GetUrlAsync(string shortUrl);
}
