using KWKY.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace KWKY.Common.Extensions
{
    public static class DateFormatAttrExtensions
    {
        public static string Fromat (this DateTime? datetime, PropertyInfo propertyInfo)
        {
            if ( !datetime.HasValue )
            {
                return null;
            }
            if ( propertyInfo.IsDefined(typeof(FormatAttribute)) )
            {
                var format = (propertyInfo.GetCustomAttribute(typeof(FormatAttribute)) as FormatAttribute).Format;
                return datetime.Value.ToString(format);
            }
            return datetime.Value.ToString();
        }

        public static string Fromat (this DateTime datetime, PropertyInfo propertyInfo)
        {
            if ( propertyInfo.IsDefined(typeof(FormatAttribute)) )
            {
                var format = (propertyInfo.GetCustomAttribute(typeof(FormatAttribute)) as FormatAttribute).Format;
                return datetime.ToString(format);
            }
            return datetime.ToString();
        }



        public static string DateFromatToString<T> (this DateTime dateTime, Expression<Func<T,DateTime>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            if ( memberExpression.Member.IsDefined(typeof(FormatAttribute)) )
            {
                var format = ( memberExpression.Member.GetCustomAttribute(typeof(FormatAttribute)) as FormatAttribute).Format;
                return dateTime.ToString(format);
            }
            return dateTime.ToString(); 
        }

    }
    
}
