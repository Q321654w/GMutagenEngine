using System.Linq.Expressions;

namespace Utils.Clr;

public static class ExpressionExtensions
{
    public static string GetMemberName<T, TProp>(this Expression<Func<T, TProp>> expr)
    {
        if (expr.Body is MemberExpression member)
            return member.Member.Name;

        if (expr.Body is UnaryExpression unary && unary.Operand is MemberExpression m)
            return m.Member.Name;

        throw new ArgumentException("Expression must be a member access", nameof(expr));
    }
}