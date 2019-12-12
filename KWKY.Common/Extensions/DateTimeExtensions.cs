using KWKY.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace KWKY.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToStringByFormatAttr<T> (this DateTime dateTime, Expression<Func<T,DateTime>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            if ( memberExpression.Member.IsDefined(typeof(FormatAttribute)) )
            {
                var format = ( memberExpression.Member.GetCustomAttribute(typeof(FormatAttribute)) as FormatAttribute).Format;
                return dateTime.ToString(format);
            }
            return dateTime.ToString(); 
        }

        public static string ToStringByFormatAttr<T> (this DateTime? dateTime, Expression<Func<T, DateTime?>> expression)
        {
            if ( !dateTime.HasValue ) 
                return null;

            MemberExpression memberExpression = expression.Body as MemberExpression;
            if ( memberExpression.Member.IsDefined(typeof(FormatAttribute)) )
            {
                var format = ( memberExpression.Member.GetCustomAttribute(typeof(FormatAttribute)) as FormatAttribute).Format;
                return dateTime.Value.ToString(format);
            }
            return dateTime.ToString();
        }

    }
    
}
