namespace LinksShorterer.ShortererService;

public interface IShorterer
{
    Task<string> GetShortLinkAsync(string fullUrl);
}
