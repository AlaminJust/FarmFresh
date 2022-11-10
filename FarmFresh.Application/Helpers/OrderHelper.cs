using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Application.Helpers
{
    public static class OrderHelper
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortBy, string sortOrder)
        {
            var type = typeof(T);
            var property = type.GetProperty(sortBy);
            
            if (property is null)
            {
                return source;
            }
            
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var resultExp = Expression.Call(typeof(Queryable), sortOrder == "asc" ? "OrderBy" : "OrderByDescending", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}
