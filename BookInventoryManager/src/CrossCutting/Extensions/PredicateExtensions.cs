using System.Linq.Expressions;

namespace CrossCutting.Extensions;

public static class PredicateExtensions
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> predicate1, Expression<Func<T, bool>> predicate2)
    {
        var parameter = Expression.Parameter(typeof(T));

        var leftVisitor = new ReplaceExpressionVisitor(predicate1.Parameters[0], parameter);
        var left = leftVisitor.Visit(predicate1.Body);

        var rightVisitor = new ReplaceExpressionVisitor(predicate2.Parameters[0], parameter);
        var right = rightVisitor.Visit(predicate2.Body);

        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
    }

    private class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldExpression;
        private readonly Expression _newExpression;

        public ReplaceExpressionVisitor(Expression oldExpression, Expression newExpression)
        {
            _oldExpression = oldExpression;
            _newExpression = newExpression;
        }

        public override Expression Visit(Expression node)
        {
            if (node == _oldExpression)
            {
                return _newExpression;
            }
            return base.Visit(node);
        }
    }
}