using LinksShorterer.LinkManager;
using LinksShorterer.LinkRepository;
using LinksShorterer.Models;

namespace LinksShorterer.ShortererService;

public class LinksService : IShorterer, IRedirector
{
    private readonly ILinkManager _linkManager;
    private readonly ILinkRepository _linkRepository;

    public LinksService(ILinkManager linkManager, ILinkRepository linkRepository)
    {
        _linkManager = linkManager;
        _linkRepository = linkRepository;
    }

    public async Task<string> GetShortLinkAsync(SourceLink link)
    {
        if (link.ShortName != null && await _linkRepository.IsLinkExistsAsync(link.ShortName))
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
