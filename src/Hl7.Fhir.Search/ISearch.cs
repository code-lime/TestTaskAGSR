using System.Linq.Expressions;

namespace Hl7.Fhir.Search;

public interface ISearch<T>
{
    Expression<Func<T, bool>> SearchExpression { get; }
}
