using LinksShorterer.LinkRepository;
using LinksShorterer.Models;
using LinksShorterer.ShortLinkGenerator;

namespace LinksShorterer.LinkManager;

public class LinkManagerService : ILinkManager
{
    private readonly IShortLinkGenerator _shortLinkGenerator;
    private readonly ILinkRepository _linkRepository;

    public LinkManagerService(IShortLinkGenerator shortLinkGenerator, ILinkRepository linkRepository)
    {
        _shortLinkGenerator = shortLinkGenerator;
        _linkRepository = linkRepository;
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

        //TODO: Dispatch event for at least hits tracking

        return sourceLink.FullUrl;
    }
}
