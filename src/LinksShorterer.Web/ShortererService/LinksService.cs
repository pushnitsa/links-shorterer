using LinksShorterer.LinkStorage;
using LinksShorterer.Models;

namespace LinksShorterer.ShortererService;

public class LinksService : IShorterer, IRedirector
{
    private readonly ILinkStorage _linkStorage;
    private readonly ILinkExistanceValidator _linkExistanceValidator;

    public LinksService(ILinkStorage linkStorage, ILinkExistanceValidator linkExistanceValidator)
    {
        _linkStorage = linkStorage;
        _linkExistanceValidator = linkExistanceValidator;
    }

    public async Task<string> GetShortLinkAsync(SourceLink link)
    {
        string result;

        if (link.ShortName != null)
        {
            if (await _linkExistanceValidator.IsLinkExistsAsync(link.ShortName))
            {
                throw new InvalidOperationException($"This short link already exists: {link.ShortName}");
            }
            else
            {
                result = await _linkStorage.CreateShortLinkAsync(link.FullUrl, link.ShortName, link.IsPermanent, link.ExpirationDate);
            }
        }
        else
        {
            result = await _linkStorage.CreateShortLinkAsync(link.FullUrl, link.IsPermanent, link.ExpirationDate);
        }

        return result;
    }

    public async Task<string> GetUrlAsync(string shortLinkName)
    {
        var result = await _linkStorage.GetFullUrlAsync(shortLinkName);

        return result;
    }
}
