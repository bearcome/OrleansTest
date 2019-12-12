using KWKY.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitCommonTest
{
    public class StringExtensionTest
    {
        [Theory]
        [InlineData("12345678",8)]
        [InlineData("123456789",9)]
        [InlineData("1234567",7)]
        public void NumberRegexTest (string str,int length)
        {
            bool isNum = str.IsNumber(length);
            Assert.True(isNum);
        }
    }
}
