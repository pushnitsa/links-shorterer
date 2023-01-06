using LinksShorterer.Models;
using System.Linq.Expressions;

namespace LinksShorterer.Repositories.Specifications;

public class LinkEntityGetByShortNameSpecification : ISpecification<LinkEntity>
{
    public LinkEntityGetByShortNameSpecification(string shortName)
    {
        Criteria = x => x.ShortName == shortName;
    }

    public Expression<Func<LinkEntity, bool>>? Criteria { get; }

    public int Take { get; set; } = 1;
    public int? Skip { get; set; } = 0;
}
