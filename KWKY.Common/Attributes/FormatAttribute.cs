using System;
using System.Collections.Generic;
using System.Text;

namespace KWKY.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormatAttribute:Attribute
    {
        public FormatAttribute (string format)
        {
            Format = format;
        }
        public string Format { get; set; }
    }
}
