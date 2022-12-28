using LinksShorterer.LinkManager;
using LinksShorterer.Models;
using LinksShorterer.Repositories;

namespace LinksShorterer.ShortererService;

public class LinksService : IShorterer, IRedirector
{
    private readonly ILinkManager _linkManager;
    private readonly Func<ILinkRepository> _linkRepositoryFactory;

    public LinksService(ILinkManager linkManager, Func<ILinkRepository> linkRepositoryFactory)
    {
        _linkManager = linkManager;
        _linkRepositoryFactory = linkRepositoryFactory;
    }

    public async Task<string> GetShortLinkAsync(Link link)
    {
        using var linkRepository = _linkRepositoryFactory();

        if (link.ShortName != null && await linkRepository.IsLinkExistsAsync(link.ShortName))
        {
            throw new InvalidOperationException($"This short link already exists: {link.ShortName}");
        }

        var result = await _linkManager.CreateShortLinkAsync(link);

        return result;
    }

    public async Task<string> GetUrlAsync(string shortLinkName)
    {
        var result = await _linkManager.GetFullUrlAsync(shortLinkName);

        return result;
    }
}
