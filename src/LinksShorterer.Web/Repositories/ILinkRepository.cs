using LinksShorterer.Models;

namespace LinksShorterer.Repositories;

public interface ILinkRepository : IRepository<LinkEntity>, IDisposable
{
    Task<bool> IsLinkExistsAsync(string shortLinkName);
}
