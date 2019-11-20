using System.ComponentModel.DataAnnotations;

namespace KWKY.Model.Common
{

    /// <summary>
    /// 分页使用的参数
    /// </summary>
    public class Pager
    {
        public Pager ()
        {
            PageIndex = 1;
            PageSize = 20;
            Asc = false;
        }
        /// <summary>
        /// 第几页 从1开始
        /// </summary>
        [Range(1,int.MaxValue, ErrorMessage="页数必须是大于0的整数")]
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页数据条数
        /// </summary>
        [Range(1, 3000, ErrorMessage="每页数据条数必须在1-3000之间")]
        public int PageSize { get; set; }
        /// <summary>
        /// 排序字段名
        /// </summary>
        public string SortPropName { get; set; }
        /// <summary>
        /// 是否正序
        /// </summary>
        public bool Asc { get; set; }
    }
}
