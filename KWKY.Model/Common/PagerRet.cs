using System;
using System.Collections.Generic;
using System.Text;

namespace KWKY.Model.Common
{
    /// <summary>
    /// 分页查询时 使用的模型
    /// 仅做返回结果使用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagerRet<T> where T : class
    {
        /// <summary>
        /// 数据
        /// </summary>
        public IEnumerable<T> DataCollection { get; set; }
        /// <summary>
        /// 第几页 从1开始
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页数据条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 匹配条件的总数
        /// </summary>
        public int Total { get; set; }
    }
}
