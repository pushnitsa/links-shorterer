using LinksShorterer.LinkStorage;
using LinksShorterer.Models;

namespace LinksShorterer.ShortererService;

public class LinksService : IShorterer, IRedirector
{
    private readonly ILinkStorage _linkStorage;

    public LinksService(ILinkStorage linkStorage)
    {
        _linkStorage = linkStorage;
    }

    public async Task<string> GetShortLinkAsync(SourceLink link)
    {
        var result = await _linkStorage.CreateShortLinkAsync(link.FullUrl, link.ShortName, link.IsPermanent, link.ExpirationDate);

        return result;
    }

    public async Task<string> GetUrlAsync(string shortLinkName)
    {
        var result = await _linkStorage.GetFullUrlAsync(shortLinkName);

        return result;
    }
}
