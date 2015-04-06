using System;
using System.Linq.Expressions;

namespace Core.Builders.Grid
{
    public static class ExpressionExtensions
    {
        public static string GetName<TModel,TValue>(this Expression<Func<TModel, TValue>> expression)
        {
            return ((MemberExpression)expression.Body).Member.Name;
        }
    }
}