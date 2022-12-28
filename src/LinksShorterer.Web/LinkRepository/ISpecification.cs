using System.Linq.Expressions;

namespace LinksShorterer.LinkRepository;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    int Take { get; set; }
    int? Skip { get; set; }
}
