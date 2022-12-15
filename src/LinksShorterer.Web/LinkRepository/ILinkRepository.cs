using LinksShorterer.Models;

namespace LinksShorterer.LinkRepository;

public interface ILinkRepository
{
    Task CreateLinkAsync(SourceLink sourceLink);
    Task<SourceLink?> GetAsync(string shortLinkName);
    Task<bool> IsLinkExistsAsync(string shortLinkName);
}
