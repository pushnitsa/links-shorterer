using LinksShorterer.ShortLinkGenerator;
using System.Diagnostics;

namespace LinksShorterer.Tests;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        var shortLinkGenerator = new ShortLinkGeneratorService();

        var timer = new Stopwatch();

        timer.Start();
        for (var i = 0; i < 1000000000; i++)
        {
            await shortLinkGenerator.GenerateShortLinkAsync();
        }
        timer.Stop();

        Assert.Equal(0, timer.ElapsedMilliseconds);
    }
}
