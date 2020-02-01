using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace UrgentCareData.Queries
{
    public static partial class QueryableExtensions
    {
        public static Expression<Func<T, bool>> DynamicWhere<T>(this IQueryable<T> source, string pname, object value, string expression) 
        {
            var param = Expression.Parameter(typeof(T), "arg");
            var memberValue = pname.Split('.').Aggregate((Expression)param, Expression.PropertyOrField);
            var memberType = memberValue.Type;
            if (value != null && value.GetType() != memberType)
                value = Convert.ChangeType(value, memberType);

            var constP = Expression.Constant(value, memberType);

            var expressionFunc = Expression.Call(
                memberValue,
                typeof(string).GetMethod(expression, new[] { typeof(string) }),
                constP);

            return Expression.Lambda<Func<T, bool>>(expressionFunc, param);
        }

        public static IQueryable<T> WhereEquals<T>(this IQueryable<T> source, string member, object value)
        {
            var item = Expression.Parameter(typeof(T), "item");
            var memberValue = member.Split('.').Aggregate((Expression)item, Expression.PropertyOrField);
            var memberType = memberValue.Type;
            if (value != null && value.GetType() != memberType)
                value = Convert.ChangeType(value, memberType);
            var condition = Expression.Equal(memberValue, Expression.Constant(value, memberType));
            var predicate = Expression.Lambda<Func<T, bool>>(condition, item);
            return source.Where(predicate);
        }

        
    }
}
