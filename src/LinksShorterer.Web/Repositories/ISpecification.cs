using System.Linq.Expressions;

namespace LinksShorterer.Repositories;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    int Take { get; set; }
    int? Skip { get; set; }
}
