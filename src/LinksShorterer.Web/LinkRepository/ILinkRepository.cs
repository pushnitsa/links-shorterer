using LinksShorterer.Models;

namespace LinksShorterer.LinkRepository;

public interface ILinkRepository : IDisposable
{
    Task CreateLinkAsync(SourceLink sourceLink);
    Task<SourceLink?> GetAsync(string shortLinkName);
    Task<bool> IsLinkExistsAsync(string shortLinkName);
    Task IncreaseLinkHitsAsync(string shortLinkName);
}
