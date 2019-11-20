using KWKY.Common.Attributes;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace KWKY.Common.Extensions
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// 根据 属性名 字符串 设置linq 排序表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="sortProperty"></param>
        /// <param name="Asc"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderByStrProp<T> (this IQueryable<T> source, string sortProperty, bool Asc = false)
        {
            var type = typeof(T);
            PropertyInfo property = null; ;
            if ( !string.IsNullOrEmpty(sortProperty) )
            {
                property = type.GetProperty(sortProperty);

            }
            if ( property == null )
            {
                property = type.GetProperties().FirstOrDefault(p => p.IsDefined(typeof(DefSortAttribute), false));
            }
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var typeArguments = new[] { type, property.PropertyType };
            var methodName = Asc ? "OrderBy" : "OrderByDescending";
            var resultExp = Expression.Call(typeof(Queryable), methodName, typeArguments, source.Expression, Expression.Quote(orderByExp));

            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}
