using System.Linq.Expressions;

namespace TaskAGSR.Infrastructure.Extensions;

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> CombineFilter<T>(this IEnumerable<Expression<Func<T, bool>>> filters, bool defaultIsEmpty)
    {
        var input = Expression.Parameter(typeof(T), "input");

        int index = 0;

        Expression? resultExpression = null;

        foreach (Expression<Func<T, bool>> filter in filters)
        {
            resultExpression = index switch
            {
                0 => filter,
                1 => Expression.AndAlso(Expression.Invoke(resultExpression!, input), Expression.Invoke(filter, input)),
                _ => Expression.AndAlso(resultExpression!, Expression.Invoke(filter, input)),
            };
            index++;
        }

        if (resultExpression is null)
            return v => defaultIsEmpty;

        if (index == 1)
            return (Expression<Func<T, bool>>)resultExpression;

        return Expression.Lambda<Func<T, bool>>(resultExpression, input);
    }
    public static Expression<Func<TInput, TResult>> Map<TInput, TMapped, TResult>(this Expression<Func<TMapped, TResult>> func, Expression<Func<TInput, TMapped>> map)
    {
        var input = Expression.Parameter(typeof(TInput), "input");
        var resultExpression = Expression.Invoke(func, Expression.Invoke(map, input));
        return Expression.Lambda<Func<TInput, TResult>>(resultExpression, input);
    }
}
