using LinksShorterer.EventManager;
using LinksShorterer.Events;
using LinksShorterer.LinkRepository;
using LinksShorterer.Models;
using LinksShorterer.ShortLinkGenerator;

namespace LinksShorterer.LinkManager;

public class LinkManagerService : ILinkManager
{
    private readonly IShortLinkGenerator _shortLinkGenerator;
    private readonly ILinkRepository _linkRepository;
    private readonly IEventDispatcher _eventDispatcher;

    public LinkManagerService(IShortLinkGenerator shortLinkGenerator, ILinkRepository linkRepository, IEventDispatcher eventDispatcher)
    {
        _shortLinkGenerator = shortLinkGenerator;
        _linkRepository = linkRepository;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<string> CreateShortLinkAsync(SourceLink sourceLink)
    {
        if (string.IsNullOrEmpty(sourceLink.ShortName))
        {
            var shortLink = await _shortLinkGenerator.GenerateShortLinkAsync();

            sourceLink.ShortName = shortLink;
        }

        await _linkRepository.CreateLinkAsync(sourceLink);

        return sourceLink.ShortName;
    }

    public async Task<string> GetFullUrlAsync(string shortLinkName)
    {
        var sourceLink = await _linkRepository.GetAsync(shortLinkName);

        if (sourceLink == null)
        {
            throw new InvalidOperationException($"Short link was not found: {shortLinkName}");
        }

        var @event = new LinkHit
        {
            ShortLinkName = shortLinkName,
        };

        await _eventDispatcher.DispatchAsync(@event);

        return sourceLink.FullUrl;
    }
}
