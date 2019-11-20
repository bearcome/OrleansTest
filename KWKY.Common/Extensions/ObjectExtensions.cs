using KWKY.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace KWKY.Common.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// 获取 类型 中 某DateTime属性的 FormatAttribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetFromat<T> (this T instance, Expression<Func<T, DateTime>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            return memberExpression.Member.IsDefined(typeof(FormatAttribute))
                ? ( memberExpression.Member.GetCustomAttribute(typeof(FormatAttribute)) as FormatAttribute).Format
                : null;
        }

        /// <summary>
        /// 获取 类型 中 某DateTime?属性的 FormatAttribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetFromat<T> (this T instance, Expression<Func<T, DateTime?>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            return memberExpression.Member.IsDefined(typeof(FormatAttribute))
                ? ( memberExpression.Member.GetCustomAttribute(typeof(FormatAttribute)) as FormatAttribute ).Format
                : null;
        }

        /// <summary>
        /// 获取类型T的属性M 的 DescriptionAttribute 描述
        /// </summary>
        /// <typeparam name="T">T类型</typeparam>
        /// <typeparam name="M">T 的泛型属性</typeparam>
        /// <param name="instance"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetDescription<T,M> (this T instance, Expression<Func<T, M>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            return memberExpression.Member.IsDefined(typeof(DescriptionAttribute))
                ? ( memberExpression.Member.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute ).Description
                : null;
        }
    }
}
