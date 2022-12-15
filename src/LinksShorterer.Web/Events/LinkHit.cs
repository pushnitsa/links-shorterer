namespace LinksShorterer.Events;

public class LinkHit : IEvent
{
    public string ShortLinkName { get; set; }
}
