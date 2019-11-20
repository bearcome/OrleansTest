using System;

namespace KWKY.Common.Attributes
{
    /// <summary>
    /// 标识Model种默认的排序字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class DefSortAttribute : Attribute
    {
    }
}
