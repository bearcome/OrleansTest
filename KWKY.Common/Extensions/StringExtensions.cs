using System;
using System.Collections.Generic;
using System.Text;

namespace KWKY.Common.Extensions
{
    public static class StringExtensions
    {
        public static string SubStringWithLength (this string str, int maxLength)
        {
            return (string.IsNullOrEmpty(str)||str.Length <= maxLength) ? str : new string(str.AsSpan(0, maxLength));
        }
    }
}
