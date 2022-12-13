namespace LinksShorterer.ShortererService;

public class LinksService : IShorterer, IRedirector
{
    public Task<string> GetShortLinkAsync(string fullUrl)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetUrlAsync(string shortUrl)
    {
        throw new NotImplementedException();
    }
}
