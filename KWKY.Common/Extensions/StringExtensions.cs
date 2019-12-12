using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace KWKY.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// 截取指定长度的字符串 0开始 ，原字符串长度不够时 返回原字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string SubStringWithLength (this string str, int maxLength)
        {
            return (string.IsNullOrEmpty(str)||str.Length <= maxLength) ? str : new string(str.AsSpan(0, maxLength));
        }

        /// <summary>
        /// 判断字符串是否  是指定长度的数字组成的
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool IsNumber (this string str, int length)
        {
            return string.IsNullOrEmpty(str) ? false : Regex.IsMatch(str, string.Format(ConstData.NumberRegex, length));
        }

    }
}
