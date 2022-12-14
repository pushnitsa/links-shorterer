namespace LinksShorterer.LinkStorage;

public interface ILinkExistanceValidator
{
    Task<bool> IsLinkExistsAsync(string shortLinkName);
}
