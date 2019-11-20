using System;
using System.Collections.Generic;
using System.Text;

namespace KWKY.Common.Extensions
{
    public static class DictionaryExtension
    {
        public static TValue SafeGetByKey<TKey, TValue> (this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            if ( key == null )
                return default;
            dictionary.TryGetValue(key, out TValue value);
            return value;
        }
    }
}
