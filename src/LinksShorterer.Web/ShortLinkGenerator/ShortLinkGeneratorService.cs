using LinksShorterer.LinkRepository;

namespace LinksShorterer.ShortLinkGenerator;

public class ShortLinkGeneratorService : IShortLinkGenerator
{
    private const string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private const int _shortUrlLength = 7;

    private readonly Random _random = new();
    private readonly ILinkRepository _linkRepository;

    public ShortLinkGeneratorService(ILinkRepository linkRepository)
    {
        _linkRepository = linkRepository;
    }

    public async Task<string> GenerateShortLinkAsync()
    {
        var charArray = new char[_shortUrlLength];
        string result;

        do
        {
            for (var i = 0; i < _shortUrlLength; i++)
            {
                var randomIndex = _random.Next(_alphabet.Length - 1);
                charArray[i] = _alphabet[randomIndex];
            }
            result = new string(charArray);
        } while (await _linkRepository.IsLinkExistsAsync(result));

        return result;
    }
}
