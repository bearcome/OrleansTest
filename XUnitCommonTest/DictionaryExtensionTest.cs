using KWKY.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitCommonTest
{
    public class DictionaryExtensionTest
    {
        [Fact]
        public void SafeGetTest ()
        {
            Dictionary<string,object> dictionary = new Dictionary<string, object>();
            var obj =new object();
            dictionary.Add("key",obj);

            var a = dictionary.SafeGetByKey("key");
            var b = dictionary.SafeGetByKey("key1");
            Assert.Equal(a, obj);
            Assert.Null(b);
        }
        [Fact]
        public void SafeGetTest2 ()
        {
            Dictionary<string,int> dictionary = new Dictionary<string, int>();
            dictionary.Add("key", 10);

            var a = dictionary.SafeGetByKey("key");
            var b = dictionary.SafeGetByKey("key1");
            Assert.Equal(10, a);
            Assert.Equal(0,b);
        }

    }
}
